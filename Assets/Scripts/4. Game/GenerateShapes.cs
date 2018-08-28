using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShapes : MonoBehaviour {

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
    public GameObject objects;

    public List<int> sizes = new List<int>();

    public List<Transform> squares = new List<Transform>();

	public List<Transform> shapes = new List<Transform>();

    public List<int> grid = new List<int>();

    public SortOrder sortOrder;

    public Transform gridParent;

	public Transform navParent;

    public List<Transform> navs = new List<Transform>();

    public List<Color32> colors = new List<Color32>();

    private void Start()
    {
        /*
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

        switch (Utilities.currentPack)
        {   
            case "Beginner":
                game = levelLoader.beginner[Utilities.currentLevel]; 
                break;
            case "Easy":
                game = levelLoader.easy[Utilities.currentLevel];
                break;
            case "Normal":
                game = levelLoader.normal[Utilities.currentLevel];
                break;
            case "Hard":
                game = levelLoader.hard[Utilities.currentLevel];
                break;
            case "Expert 1":
                game = levelLoader.expert1[Utilities.currentLevel];
                break;
            case "Expert 2":
                game = levelLoader.expert2[Utilities.currentLevel];
                break;
            case "Expert 3":
                game = levelLoader.expert3[Utilities.currentLevel];
                break;
            case "Expert 4":
                game = levelLoader.expert4[Utilities.currentLevel];
                break;
        }

        LoadGameString();
        */
    }

    public void GenerateMultipleLevels() 
    {
        for (int i = 0; i < 150; i++)
        {
            width = Random.Range(4, 7);
            height = Random.Range(4, 7);

            float shapeCountFloat = Mathf.Sqrt((float)(width * height));

            shapeCount = (int)Mathf.Round(shapeCountFloat) + Random.Range(0, 2);

            if (shapeCount > 11){
                shapeCount = 11;
            }


            if (width<height) {
                minSize = Mathf.FloorToInt(width)/2 + Random.Range(-1, 1);



                maxSize = 2 * height + Random.Range(-2, 2);


            }
            else {
                minSize = Mathf.FloorToInt(height)/2 + Random.Range(-1, 1);
                maxSize = 2 * width + Random.Range(-2, 0);
            }

//for small games
            if (minSize < 2)
            {
                minSize = 2;
            }

            scaleValue = 1.75f;

            amount = width * height;

            GenerateSizes();
            Generate();
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

            default:
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

        Nav();    
    }

    public void CreateGameString()
    {
        game = "" + width + "," + height + "," + scaleValue;

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
        scaleValue = float.Parse(gameStringList[2]);

        grid.Clear();

        transform.localScale = new Vector2(1, 1);

        for (int i = 3; i < gameStringList.Length; i++)
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

        objects.transform.localScale = new Vector2(scaleValue, scaleValue);

        sortOrder.CustomStart();

        CreateGameString();

        Nav();
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
        }

        gridParent.transform.localScale = new Vector2(scaleValue, scaleValue);
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
        for (int i = 0; i < colors.Count; i++)
        {
            Color32 temp = colors[i];
            int randomIndex = Random.Range(i, colors.Count);
            colors[i] = colors[randomIndex];
            colors[randomIndex] = temp;
        }

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

            squares[i].SetParent(ScrollRectBackground.GetChild(grid[i]));
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