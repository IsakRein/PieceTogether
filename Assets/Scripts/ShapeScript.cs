using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeScript : MonoBehaviour {
	private const string x = "x";
	public List<Transform> squares = new List<Transform>();
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    public float HighestX;
    public float LowestX;
    public float HighestY;
    public float LowestY;

    public float AverageX;
    public float AverageY;

    private float lastPointedX;
    private float lastPointedY;

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

	public List<Vector2> squarePositions = new List<Vector2>();

	private float scaleInNav;

	private bool dropInNav;

	private Vector2 navPosition;

	private Vector2 lastSquareNavPosition;

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

			bool objectsOverlapping = false;

			if (lastPointedX != xPos || lastPointedY != yPos)
			{
				for (int i = 0; i < transform.childCount - 1; i++)
				{
					float x = (transform.GetChild(i).localPosition.x + (xPos / scaleValue));
					float y = (transform.GetChild(i).localPosition.y + (yPos / scaleValue));

					if (y > 4.5f || y < -4.5f)
					{
						objectsOverlapping = true;
					}

					else if (x > 4.5f || x < -4.5f)
					{
						objectsOverlapping = true;
					}

					else
					{
						for (int j = 0; j < sortOrder.positions.Count; j++)
						{
							if (sortOrder.positions[j] != null && j != number - 1)
							{
								foreach (Vector2 vector in sortOrder.positions[j])
								{
									if (vector.x == x && vector.y == y)
									{
										objectsOverlapping = true;
									}
								}
							}
						}
					}
				}

				if (objectsOverlapping == false)
				{
					lastPointedX = xPos;
					lastPointedY = yPos;
				}
			}

			pointer.transform.position = new Vector2(lastPointedX, lastPointedY);

            

			if (squares[squares.Count-1].position.y < -12.5f)
			{
				float percentage = (((squares[squares.Count - 1].position.y * (-1f)) - 12.5f) / 3.5f);

				float scale;
				if (squares[squares.Count - 1].position.y > -16f)
				{
					scale = scaleValue - (percentage * (scaleValue - scaleInNav));
				}

				else
				{
					scale = scaleInNav;
				}

				transform.localScale = new Vector2(scale, scale);

				dropInNav = true;

				pointer.SetActive(false);
			}

			else {
				transform.localScale = new Vector2(scaleValue, scaleValue);

				dropInNav = false;

				pointer.SetActive(true);
                
			}

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
        
		if (dropInNav) {
			transform.localPosition = navPosition;
			transform.localScale = new Vector2(scaleInNav, scaleInNav);
		}

		else {
			transform.position = new Vector2(lastPointedX, lastPointedY);
            pointer.transform.position = new Vector2(0, 0);

            squarePositions.Clear();

            for (int i = 0; i < transform.childCount - 1; i++)
            {
                float x = transform.GetChild(i).localPosition.x + (transform.position.x / scaleValue);
                float y = transform.GetChild(i).localPosition.y + (transform.position.y / scaleValue);

                squarePositions.Add(new Vector2(x, y));
            }

            sortOrder.positions[number - 1] = squarePositions;
		}      
    }

	public void PosInNav() {
		float largestDifference;

		navPosition = transform.localPosition;
		lastSquareNavPosition = squares[squares.Count - 1].position;

		if (HighestX-LowestX > HighestY-LowestY) {
			largestDifference = HighestX - LowestX;
		}
        
		else {
			largestDifference = HighestY - LowestY;
		}

		scaleInNav = 3.2f / (float)largestDifference;

		transform.localScale = new Vector2(scaleInNav, scaleInNav);    
	}
}
