using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;   

public class GenerateShapes : MonoBehaviour {

    public int importMinSize;
    public int importMaxSize;
    public float importScaleValue;
    public int importMinCount;
    public int importMaxCount;

    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]
    [Space]


    public List<string> levels = new List<string>();

    [Space]

    public string game;

    public float scaleValue;

    public int shapeCount;
    public int minSize;
    public int maxSize;

    public int width;
    public int height;
    private int amount;

    private int sum;
    private int sumLeft;

    public LevelLoader levelLoader;

    public GameObject squarePrefab;
    public GameObject shapePrefab;
    public GameObject gridSquarePrefab;

    public Transform ScrollRectBackground;
    public Transform navFollower;
    public GameObject objects;

    public TextMeshProUGUI levelText;

    public List<int> sizes = new List<int>();

    public List<Transform> squares = new List<Transform>();

	public List<Transform> shapes = new List<Transform>();

    public List<int> grid = new List<int>();

    public SortOrder sortOrder;

    public Transform gridParent;

	public Transform navParent;
    public Transform content;

    public List<Transform> navs = new List<Transform>();

    public List<Color32> colors = new List<Color32>();

    private void Start()
    {
        switch (Utilities.currentPack)
        {   
            case "Beginner":
                game = levelLoader.beginner[Utilities.currentLevel - 1]; 
                break;
            case "Easy":
                game = levelLoader.easy[Utilities.currentLevel - 1];
                break;
            case "Normal":
                game = levelLoader.normal[Utilities.currentLevel - 1];
                break;
            case "Hard":
                game = levelLoader.hard[Utilities.currentLevel - 1];
                break;
            case "Advanced":
                game = levelLoader.advanced[Utilities.currentLevel - 1];
                break;
            case "Expert 1":
                game = levelLoader.expert1[Utilities.currentLevel - 1];
                break;
            case "Expert 2":
                game = levelLoader.expert2[Utilities.currentLevel - 1];
                break;
            case "Expert 3":
                game = levelLoader.expert3[Utilities.currentLevel - 1];
                break;
            default:
                Utilities.currentPack = "Beginner";
                Utilities.currentLevel = 1;
                game = levelLoader.beginner[0];
                break;
        }

        levelText.SetText("" + Utilities.currentLevel);

        LoadGameString();
    }

    public void GenerateMultipleLevels() 
    {
        for (int i = 0; i < 150; i++)
        {
            //beginner 4-7 0.6
            //easy 5-8 0.55
            //normal 5-9 0.5
            //hard 6-10 0.4
            //advanced 7-11 0.4
            //expert 8-13 0.35


            width = Random.Range(importMinSize, importMaxSize);
            height = Random.Range(importMinSize, importMaxSize);
            scaleValue = importScaleValue;

            shapeCount = Random.Range(importMinCount, importMaxCount);

            if (shapeCount > 11)
            {
                shapeCount = 11;
            }

            if (width<height) {
                minSize = Mathf.FloorToInt(width)/2 + Random.Range(-1, 1);
                maxSize = 2 * height + Random.Range(-3, 1);
            }
            else {
                minSize = Mathf.FloorToInt(height)/2 + Random.Range(-1, 1);
                maxSize = 2 * width + Random.Range(-3, 1);
            }

//for small games
            if (minSize < 3)
            {
                minSize = 3;
            }

            amount = width * height;

            //Debug.Log("Min: " + minSize + "; Max: " + maxSize + "; ShapeCount: " + shapeCount + "; Amount: " + amount);

            GenerateSizes();
            Generate();

            int highestColor = 0;
            List<int> takenColors = new List<int>();

            for (int j = 0; j < colors.Count; j++)
            {
                takenColors.Add(j);
            }

            for (int j = 0; j < grid.Count; j++)
            {
                if (grid[j] > highestColor) {
                    highestColor = grid[j];
                }
            }

            for (int j = 0; j <= highestColor; j++)
            {
                for (int k = 0; k < grid.Count; k++)
                {
                    if (grid[k] == j)
                    {
                        grid[k] = colors.Count + j;
                    }
                }
            }

            for (int j = 0; j <= highestColor; j++)
            {
                int newColor = takenColors[Random.Range(0, takenColors.Count)];
                takenColors.Remove(newColor);

                for (int k = 0; k < grid.Count; k++)
                {
                    if (grid[k] == j + colors.Count) {
                        grid[k] = newColor;
                    }
                }
            }


            CreateGameString();
            
            levels.Add(game);

        }
    }

    public void SaveToLevelLoader() {
        switch (Utilities.currentPack)
        {
            case "Beginner":
                levelLoader.beginner = levels;
                break;
            case "Easy":
                levelLoader.easy = levels;
                break;
            case "Normal":
                levelLoader.normal = levels;
                break;
            case "Hard":
                levelLoader.hard = levels;
                break;
            case "Advanced":
                levelLoader.advanced = levels;
                break;
            case "Expert 1":
                levelLoader.expert1 = levels;
                break;
            case "Expert 2":
                levelLoader.expert2 = levels;
                break;
            case "Expert 3":
                levelLoader.expert3 = levels;
                break;
        }
    }

    public void GeneratingStart()
    {
        amount = width * height;

        CreateSquares();
        GenerateSizes();
        Generate();

        for (int i = 0; i < shapeCount; i++)
        {
            GameObject instShape;
            instShape = Instantiate(shapePrefab, ScrollRectBackground);
            instShape.name = (i + 1).ToString();
            instShape.GetComponent<ShapeScript>().number = i + 1;

			shapes.Add(instShape.transform);
		}
        
        CreateGrid();

        ApplyColor();

        transform.localScale = new Vector2(scaleValue, scaleValue);
      
        sortOrder.CustomStart();

        CreateGameString();

        Nav2();    
    }

    public void CreateGameString()
    {
        game = "" + width + "," + height + "," + scaleValue + "," + shapeCount;

        string squares = "";

        foreach (int square in grid)
        {
            squares = squares + "," + square;
        }
        game = game + squares;
    }

    public void LoadGameString()
    {
        string[] gameStringList = game.Split(',');

        width = int.Parse(gameStringList[0]);
        height = int.Parse(gameStringList[1]);
        amount = width * height;

        scaleValue = float.Parse(gameStringList[2]);
        shapeCount = int.Parse(gameStringList[3]);
        
        grid.Clear();

        transform.localScale = new Vector2(1, 1);

        for (int i = 4; i < gameStringList.Length; i++)
        {
            grid.Add(int.Parse(gameStringList[i]));
        }

        List<Transform> childrenToDestroy = new List<Transform>();

        foreach (Transform child in objects.transform)
        {
            childrenToDestroy.Add(child);
        }

        foreach (Transform child in gridParent)
        {
            childrenToDestroy.Add(child);
        }

        foreach (Transform child in transform)
        {
            childrenToDestroy.Add(child);
        }

        foreach (Transform child in childrenToDestroy)
        {
            child.transform.parent = null;
            Destroy(child.gameObject);
        }

        squares.Clear();
        shapes.Clear();

        CreateSquares();

        CreateGrid();


        for (int i = 0; i < shapeCount; i++)
        {
            GameObject instShape;
            instShape = Instantiate(shapePrefab, ScrollRectBackground);
            instShape.name = (i + 1).ToString();
            instShape.GetComponent<ShapeScript>().number = i + 1;

            shapes.Add(instShape.transform);
        }


        ApplyColor();

        objects.transform.localScale = new Vector2(scaleValue, scaleValue);
        objects.transform.localPosition = new Vector2(0, 1f);

        sortOrder.CustomStart();

        CreateGameString();

        Nav2();
       //Nav();
    }

    private void CreateSquares() {

        amount = width * height;

        for (int i = 1; i <= amount; i++)
        {
            GameObject instSquare;
            instSquare = Instantiate(squarePrefab, transform);
            instSquare.name = i.ToString();
        }

        squares.Add(null);

        foreach (Transform child in transform)
        {
            squares.Add(child);
        }

        float startValueX = -(0.5f * (width - 1));
        float startValueY = (0.5f * (height - 1));

        for (int i = 1; i < squares.Count; i++)
        {
            int xOrder;

            if (i % width == 0)
            {
                xOrder = width;
            }
            else
            {
                xOrder = i % width;
            }

            int yOrder = Mathf.RoundToInt((Mathf.Floor((i - 0.01f) / width)) + 1);

            squares[i].position = new Vector2(startValueX + (xOrder - 1), startValueY - (yOrder - 1));
        }
    }

    private void CreateGrid() {
        gridParent.transform.localScale = new Vector2(1, 1);

        for (int i = 1; i <= amount; i++)
        {
            GameObject instGridSquare;
            instGridSquare = Instantiate(gridSquarePrefab, gridParent);
            instGridSquare.name = i.ToString();
        }

        float startValueX2 = -(0.5f * (width - 1));
        float startValueY2 = (0.5f * (height - 1));

        for (int i = 1; i <= gridParent.childCount; i++)
        {
            int xOrder;

            if (i % width == 0)
            {
                xOrder = width;
            }
            else
            {
                xOrder = i % width;
            }

            int yOrder = Mathf.RoundToInt((Mathf.Floor((i - 0.01f) / width)) + 1);

            gridParent.GetChild(i - 1).position = new Vector2(startValueX2 + (xOrder - 1), startValueY2 - (yOrder - 1));

            float x = (Mathf.Round((gridParent.GetChild(i - 1).position.x) * 10f) / 10f);
            float y = (Mathf.Round((gridParent.GetChild(i - 1).position.y + 0.6f/scaleValue) * 10f) / 10f);

            sortOrder.finishedLevel.Add(new Vector2(x, y));
        }

        gridParent.transform.localScale = new Vector2(scaleValue, scaleValue);
        gridParent.transform.localPosition = new Vector2(0, 0.6f);
    }

    public void Generate()
    {
        grid.Clear();
        
        for (int i = 0; i <= amount; i++)
        {
            grid.Add(-1);
        }

        for (int i = 0; i < shapeCount; i++)
        {
            int firstFreeSquare = -1;
            int num = 1;

            while (firstFreeSquare == -1)
            {
                if (grid[num] == -1)
                {
                    firstFreeSquare = num;
                }

                num = num + 1;
            }

            grid[firstFreeSquare] = i;

            List<int> currentShape = new List<int>();

            currentShape.Add(firstFreeSquare);

            for (int j = 1; j < sizes[i]; j++)
            {                
                //decide which square
                bool moveAvaliable = false;
                int squareToMove = 0;
                int squareToMoveInGrid = 0;

                bool anyMoveAvaliable = false;

                for (int k = 0; k < currentShape.Count; k++)
                {
                    squareToMoveInGrid = currentShape[k];

                    if (UpAvaliable(squareToMoveInGrid) || RightAvaliable(squareToMoveInGrid) || DownAvaliable(squareToMoveInGrid) || LeftAvaliable(squareToMoveInGrid))
                    {
                        anyMoveAvaliable = true;
                    }
                }

                if (anyMoveAvaliable)
                {
                    while (!moveAvaliable)
                    {
                        squareToMove = Random.Range(0, currentShape.Count);
                        squareToMoveInGrid = currentShape[squareToMove];

                        if (UpAvaliable(squareToMoveInGrid) || RightAvaliable(squareToMoveInGrid) || DownAvaliable(squareToMoveInGrid) || LeftAvaliable(squareToMoveInGrid))
                        {
                            moveAvaliable = true;
                        }

                        else
                        {
                            moveAvaliable = false;
                        }
                    }

                    //decide where to go
                    List<int> possibleMoves = new List<int>();
                    if (UpAvaliable(squareToMoveInGrid))
                    {
                        possibleMoves.Add(1);
                    }
                    if (RightAvaliable(squareToMoveInGrid))
                    {
                        possibleMoves.Add(2);
                    }
                    if (DownAvaliable(squareToMoveInGrid))
                    {
                        possibleMoves.Add(3);
                    }
                    if (LeftAvaliable(squareToMoveInGrid))
                    {
                        possibleMoves.Add(4);
                    }

                    int chosenDirection = possibleMoves[Random.Range(0, possibleMoves.Count)];

                    if (chosenDirection == 1)
                    {
                        grid[SquareAbove(squareToMoveInGrid)] = i;
                        currentShape.Add(SquareAbove(squareToMoveInGrid));
                    }
                    else if (chosenDirection == 2)
                    {
                        grid[SquareRight(squareToMoveInGrid)] = i;
                        currentShape.Add(SquareRight(squareToMoveInGrid));
                    }
                    else if (chosenDirection == 3)
                    {
                        grid[SquareBelow(squareToMoveInGrid)] = i;
                        currentShape.Add(SquareBelow(squareToMoveInGrid));
                    }
                    else if (chosenDirection == 4)
                    {
                        grid[SquareLeft(squareToMoveInGrid)] = i;
                        currentShape.Add(SquareLeft(squareToMoveInGrid));
                    }
                }

                else
                {
                    i = -1;

                    grid.Clear();

                    for (int l = 0; l <= amount; l++)
                    {
                        grid.Add(-1);
                    }
                    break;
                }
            }
        }
    }

    private void GenerateSizes()
    {
        sizes.Clear();

        sumLeft = amount;

        for (int i = 0; i < shapeCount; i++)
        {
            if ((minSize * (shapeCount - sizes.Count) <= sumLeft) && (maxSize * (shapeCount - sizes.Count) >= sumLeft))
            {
                if (i == shapeCount - 1)
                {
                     sizes.Add(sumLeft);
                }
                else
                {
                    sizes.Add(Random.Range(minSize, maxSize + 1));
                }

                CalculateSum();
            }

            else
            {
                sizes.RemoveAt(i - 1);
                CalculateSum();
                i = i - 2;
            }
        }

        CalculateSum();
    }

    private void CalculateSum()
    {
        sum = 0;
        for (int j = 0; j < sizes.Count; j++)
        {
            sum = sum + sizes[j];
        }
        sumLeft = amount - sum;
    }

    private void ApplyColor()
    {
        for (int i = 1; i <= amount; i++)
        {
            if (grid[i] == -1)
            {
                squares[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }

            else
            {
                squares[i].GetComponent<SpriteRenderer>().color = colors[grid[i]];
            }

            foreach (Transform child in ScrollRectBackground)
            {
                if (child.childCount == 0) {
                    squares[i].SetParent(child);
                    child.name = grid[i].ToString();
                    break;
                }
                else if (child.name == grid[i].ToString())
                {
                    squares[i].SetParent(child);
                    break;
                }
            }
        }

        foreach (Transform child in ScrollRectBackground)
        {
            child.GetComponent<ShapeScript>().CustomStart();
        }
    }

    private void Nav() 
	{
		int NavCount = Mathf.CeilToInt(((float)shapeCount) / 3.0f);
       
		for (int i = 1; i <= NavCount; i++)
		{
			new GameObject("" + i).transform.SetParent(navParent);
		}

		for (int i = 0; i < navParent.childCount; i++)
		{
			navs.Add(navParent.GetChild(i));

			float x = 24 * i;

			navs[i].position = new Vector2(x, 0);
		}

        for (int i = 0; i < shapes.Count; i++)
        {
            Transform temp = shapes[i];
            int randomIndex = Random.Range(i, shapes.Count);
            shapes[i] = shapes[randomIndex];
            shapes[randomIndex] = temp;
        }

        for (int i = 0; i < shapes.Count; i++) 
		{
			int order = i + 1;

			int parent = Mathf.CeilToInt(((float)order) / 3.0f);
			shapes[i].SetParent(navs[parent - 1]);

			float x;

			if (order % 3 == 1) 
			{
				x = -5.7f;
			}

			else if (order % 3 == 2)
            {
				x = 0;
            }

			else 
			{
				x = 5.7f;          
			}
            
			shapes[i].localPosition = new Vector2(x, -16f);

			shapes[i].GetComponent<ShapeScript>().PosInNav();

		}
	}

    private void Nav2()
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            Transform temp = shapes[i];
            int randomIndex = Random.Range(i, shapes.Count);
            shapes[i] = shapes[randomIndex];
            shapes[randomIndex] = temp;
        }

        for (int i = 0; i < shapes.Count; i++)
        {
            shapes[i].SetParent(navParent.GetChild(0));
            shapes[i].GetComponent<ShapeScript>().PosInNav();
        }
       
        GameObject backgroundScroll;
        backgroundScroll = new GameObject("Background Scroll");
        backgroundScroll.transform.SetParent(navParent.GetChild(0));
        backgroundScroll.AddComponent<Image>().color = new Color(1, 1, 1, 0);
        backgroundScroll.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        backgroundScroll.GetComponent<RectTransform>().sizeDelta = new Vector2(shapeCount + 0.5f, 2f);
        backgroundScroll.GetComponent<RectTransform>().localPosition = new Vector2(0f, -4f);

        RectTransform rect = backgroundScroll.GetComponent<RectTransform>();

        content.GetComponent<Nav>().rect = rect;
       
        //position
        if (shapes.Count > Mathf.FloorToInt(Camera.main.orthographicSize * 2 * Screen.width / Screen.height)) {
            navParent.GetComponent<ScrollRect>().enabled = true;
            content.GetComponent<RectTransform>().offsetMax = new Vector2(-((Screen.width / 192f) - (-(rect.offsetMin.x - rect.offsetMax.x))), 0);
        }
        else {
            navParent.GetComponent<ScrollRect>().enabled = false;
            content.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
       
        for (int i = 0; i < shapes.Count; i++)
        {
            float x = i - (shapeCount - 1f) / 2f;
            shapes[i].localPosition = new Vector2(x, -4f);
            content.GetComponent<Nav>().objectsInNav.Add(shapes[i]);
        }
    }

    bool UpAvaliable(int square)
    {
        if ((square > width) && grid[(square - width)] == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool RightAvaliable(int square)
    {
        if ((square % width != 0) && grid[(square + 1)] == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool DownAvaliable(int square)
    {
        if ((square <= (amount-width)) && grid[(square + width)] == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool LeftAvaliable(int square)
    {
        if (((square + width) % width != 1) && grid[(square - 1)] == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int SquareAbove(int square)
    {
        return square - width;
    }

    int SquareRight(int square)
    {
        return square + 1;
    }

    int SquareBelow(int square)
    {
        return square + width;
    }

    int SquareLeft(int square)
    {
        return square - 1;
    }
}