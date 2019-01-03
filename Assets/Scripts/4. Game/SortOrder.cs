using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Positions
{
    public List<Vector2> squaresPos = new List<Vector2>();
}

[System.Serializable]
public class PositionsList
{
    public List<Positions> shapesPos = new List<Positions>();
}

public class SortOrder : MonoBehaviour {

    public List<int> solution = new List<int>();
    private int highestValue;
    public int width;
    public int height;

    public GameObject squarePrefab;
    public GameObject hintPrefab;
    public Transform hints;

    public List<int> sortOrder = new List<int>();
    public List<ShapeScript> shapes = new List<ShapeScript>();
    public GenerateShapes generateShapes;
    public PositionsList positions = new PositionsList();
    public List<Vector2> positionsInOne = new List<Vector2>();
    public List<Vector2> finishedLevel = new List<Vector2>();
    public PopUp popUp;
    public AudioSource audioSource;
    public AudioClip click;
    public AudioClip dropClick;
    public AudioClip start;
    public AudioClip levelWon;

    public List<int> notHintedList = new List<int>();

    public TextMeshProUGUI hintText;
    public TextMeshProUGUI skipText;

    public LevelWon levelWonScript;

    private int clickId;
    private int fileID;


#if UNITY_ANDROID && !UNITY_EDITOR
    private bool isAndroid = true;
#else
    private bool isAndroid = false;
#endif

    private void Start()
    {
        if (isAndroid)
        {
            AndroidNativeAudio.makePool(1);
            fileID = AndroidNativeAudio.load("Android Native Audio/Click.wav");
        }
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        hintText.SetText("" + Utilities.hintCount);
        skipText.SetText("" + Utilities.skipCount);
    }

    public void CustomStart()
    {
        for (int i = 0; i <= generateShapes.shapeCount; i++)
        {
            positions.shapesPos.Add(new Positions());
        }

        transform.localScale = new Vector2(generateShapes.scaleValue, generateShapes.scaleValue);

        foreach (int value in solution)
        {
            if (!notHintedList.Contains(value) && value != -1)
            {
                notHintedList.Add(value);
            }
        }
    }

    public void GetHint()
    {
        Utilities.AddHints(-1);
        hintText.SetText("" + Utilities.hintCount);
        GenerateSolution();
    }

    public void Skip()
    {
        Utilities.AddSkips(-1);
        skipText.SetText("" + Utilities.skipCount);
        levelWonScript.NextLevel();
    }

    public void GenerateSolution()
    {
        int shape = notHintedList[Random.Range(0, notHintedList.Count)];
        notHintedList.Remove(shape);

        GenerateShapeHint(shape);
    }

    void GenerateShapeHint(int num)
    {
        GameObject instShape = new GameObject();
        instShape.name = "instShape";
        instShape.transform.SetParent(hints);

        for (int i = 0; i < solution.Count; i++)
        {
            if (solution[i] == num)
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

                GameObject instSquare;
                instSquare = Instantiate(squarePrefab, instShape.transform);
                instSquare.transform.position = new Vector2(-(0.5f * (width - 1)) + (xOrder - 1), ((0.5f * (height - 1)) - (yOrder - 1)) + (0.6f / generateShapes.scaleValue));



                Color color = generateShapes.colors[num];
                Color newColor = new Color(color.r, color.g, color.b, 0.5f);
                instSquare.GetComponent<SpriteRenderer>().color = newColor;
            }
        }

        instShape.transform.localScale = new Vector2(generateShapes.scaleValue, generateShapes.scaleValue);

        for (int i = instShape.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = instShape.transform.GetChild(i);
            child.SetParent(hints);
        }

        GameObject.Destroy(instShape);

        List<SpriteRenderer> sprites = hints.GetComponent<Hint>().sprites;

        foreach (Transform child in hints.transform)
        {
            if (child.name != "instShape")
            {
                sprites.Add(child.GetComponent<SpriteRenderer>());
            }
        }

        hints.GetComponent<Hint>().hasStarted = true;
    }

    public void UpdateSort(int number)
    {
        sortOrder.Remove(number);
        sortOrder.Insert(0, number);

        foreach (ShapeScript shape in shapes)
        {
            int indexNumber = sortOrder.Count - sortOrder.IndexOf(shape.number);
           
            shape.SetSort(indexNumber);
        }
    }

    public void UpdatePositions()
    {
        positionsInOne.Clear();

        for (int i = 0; i < positions.shapesPos.Count; i++)
        {
            for (int j = 0; j < positions.shapesPos[i].squaresPos.Count; j++)
            {
                positionsInOne.Add(positions.shapesPos[i].squaresPos[j]);
            }
        }

        CheckIfWon();
    }

    private void CheckIfWon()
    {
        int amountOfSim = 0;

        for (int i = 0; i < positionsInOne.Count; i++)
        {
            if (finishedLevel.Contains(positionsInOne[i])) {
                amountOfSim += 1;
            }
        }

        //level won
        if (amountOfSim == finishedLevel.Count) {
            //trigger animations etc
            foreach (ShapeScript shape in shapes)
            {
                shape.levelWon = true;
            }

            popUp.InitPopUp("Level Won");
        }
    }

    public void ObjectMoved()
    {
        if (Utilities.VibrationOn)
        {
            Utilities.Vibrate();
        }
        if (Utilities.SoundOn)
        {
            if (isAndroid)
            {
                int streamID = AndroidNativeAudio.play(fileID);
            }
            else
            {
                audioSource.PlayOneShot(click);
            }
        }
    }
}
