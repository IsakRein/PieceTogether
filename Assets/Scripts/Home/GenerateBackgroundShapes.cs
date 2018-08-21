using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBackgroundShapes : MonoBehaviour
{
    public int minSize;
    public int maxSize;

    public float timeBetween;
    public float timeLeft;


    RectTransform rectTransform;

    public float spaceBetween;

    private List<Transform> squares = new List<Transform>();

    public List<Color32> colors = new List<Color32>();


    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0) 
        {
            GenerateNewShape();


            timeLeft = timeBetween + Random.Range(-1, 1);
        }
    }

    void GenerateNewShape()
    {
        int size = Random.Range(minSize, maxSize);
        Color32 color = colors[Random.Range(0, colors.Count)];

        GameObject shape;
        shape = new GameObject("Shape");
        shape.transform.SetParent(gameObject.transform);

        shape.transform.localPosition = new Vector2(0, 0);
        shape.transform.position = new Vector2((Random.Range(-4f, 4f)), shape.transform.position.y);
                     
        shape.transform.localScale = new Vector2(1f, 1f);
        shape.AddComponent<BackgroundShapeScript>();
            
        squares.Clear();

        NewSquare(0, 0, color, shape.transform);

        for (int i = 0; i < size - 1; i++)
        {
            Transform expandSquare = squares[Random.Range(0, squares.Count-1)];
            int expandDirection = Random.Range(1, 4);

            if (expandDirection == 1) {
                NewSquare(expandSquare.localPosition.x - spaceBetween, expandSquare.localPosition.y, color, shape.transform);
            }

            else if (expandDirection == 2)
            {
                NewSquare(expandSquare.localPosition.x, expandSquare.localPosition.y - spaceBetween, color, shape.transform);
            }

            else if (expandDirection == 3)
            {
                NewSquare(expandSquare.localPosition.x + spaceBetween, expandSquare.localPosition.y, color, shape.transform);
            }

            else if (expandDirection == 4) {
                NewSquare(expandSquare.localPosition.x, expandSquare.localPosition.y + spaceBetween, color, shape.transform);
            }
        }

        shape.transform.eulerAngles = new Vector3(0, 0, Random.Range(1, 360));
    }

    void NewSquare(float xPos, float yPos, Color32 color, Transform parent) {
        GameObject square;
        square = new GameObject("Square");
        square.transform.SetParent(parent);
        square.transform.localPosition = new Vector2(xPos, yPos);
        square.transform.localScale = new Vector2(0.75f, 0.75f);
        square.AddComponent<Image>().color = color;

        squares.Add(square.transform);
    }
}
