using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour {

    public List<Transform> squares = new List<Transform>();
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    public float HighestX;
    public float LowestX;
    public float HighestY;
    public float LowestY;

    public float AverageX;
    public float AverageY;

    private BoxCollider2D boxCollider2D;
    BoxCollider2D[] BoxColliders;

    Vector3 targetPos;

    private SortOrder sortOrder;

    private float scaleValue;

    public int number;

    public GenerateShapes generateShapes;
    private GameObject pointer;

    bool draggingItem = false;

    private bool xWhole;
    private bool yWhole;

    public void CustomStart()
    {
        generateShapes = GameObject.Find("/Grid").GetComponent<GenerateShapes>();
        scaleValue = generateShapes.scaleValue;

        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        sortOrder = transform.parent.GetComponent<SortOrder>();

        sortOrder.sortOrder.Add(number);
        sortOrder.shapes.Add(gameObject.GetComponent<ShapeScript>());

        foreach (Transform child in transform)
        {
            squares.Add(child);
            spriteRenderers.Add(child.GetComponent<SpriteRenderer>());
        }

        HighestX = squares[0].position.x;
        LowestX = squares[0].position.x;
        HighestY = squares[0].position.y;
        LowestY = squares[0].position.y;

        foreach (Transform square in squares)
        {
            if (square.position.x > HighestX)
            {
                HighestX = square.position.x;
            }
            else if (square.position.x < LowestX)
            {
                LowestX = square.position.x;
            }

            if (square.position.y > HighestY)
            {
                HighestY = square.position.y;
            }
            else if (square.position.y < LowestY)
            {
                LowestY = square.position.y;
            }
        }

        AverageX = (HighestX + LowestX) / 2;
        AverageY = (HighestY + LowestY) / 2;


        if (AverageX == Mathf.RoundToInt(AverageX))
        {
            xWhole = true;
        }
        else
        {
            xWhole = false;
        }

        if (AverageY == Mathf.RoundToInt(AverageY))
        {
            yWhole = true;
        }
        else
        {
            yWhole = false;
        }

        transform.position = new Vector2(AverageX, AverageY);

        foreach (Transform square in squares)
        {
            square.position = new Vector2(square.position.x-transform.position.x, square.position.y-transform.position.y);

            UnityEditorInternal.ComponentUtility.CopyComponent(square.GetComponent<BoxCollider2D>());
            Destroy(square.GetComponent<BoxCollider2D>());
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(gameObject);
        }

        BoxColliders = GetComponents<BoxCollider2D>();

        for (int i = 0; i < transform.childCount; i++)
        {
            BoxColliders[i].offset = transform.GetChild(i).transform.localPosition;
        }

        int childCount = transform.childCount;
        pointer = new GameObject("Pointer");
        pointer.transform.SetParent(transform);
        pointer.transform.localPosition = new Vector2(0, 0);

        for (int i = 0; i < childCount; i++)
        {
            GameObject instChild;

            instChild = Instantiate(transform.GetChild(i).gameObject, pointer.transform);
            Color color = instChild.GetComponent<SpriteRenderer>().color;
            color.a = 0.5f;
            instChild.GetComponent<SpriteRenderer>().color = color;
        }

        targetPos = transform.position;
    }

    private void Update()
    {
        if (draggingItem)
        {
            float xPos;
            float yPos;

            if (xWhole)
            {
                xPos = scaleValue * Mathf.Round(transform.position.x / scaleValue);
            }
            else
            {
                xPos = scaleValue * (Mathf.Floor(transform.position.x / scaleValue) + 0.5f);
            }

            if (yWhole)
            {
                yPos = scaleValue * Mathf.Round(transform.position.y / scaleValue);
            }
            else
            {
                yPos = scaleValue * (Mathf.Floor(transform.position.y / scaleValue) + 0.5f);
            }

            pointer.transform.position = new Vector2(xPos, yPos); 
        }
    }

    public void SetSort(int sort)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = sort;
        }
    }

    public void DraggingItem()
    {
        pointer.SetActive(true);
        draggingItem = true;

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingOrder = 50;
        }
    }

    public void DropItem()
    {
        pointer.SetActive(false);
        draggingItem = false;

        sortOrder.UpdateSort(number);

        float xPos;
        float yPos;

        if (xWhole)
        {
            xPos = scaleValue * Mathf.Round(transform.position.x / scaleValue);
        }
        else
        {
            xPos = scaleValue * (Mathf.Floor(transform.position.x / scaleValue) + 0.5f);
        }

        if (yWhole)
        {
            yPos = scaleValue * Mathf.Round(transform.position.y / scaleValue);
        }
        else
        {
            yPos = scaleValue * (Mathf.Floor(transform.position.y / scaleValue) + 0.5f);
        }

        transform.position = new Vector2(xPos, yPos);
        pointer.transform.position = new Vector2(0, 0);
    }
}
