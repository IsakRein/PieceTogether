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

    public float lastPointedX;
    public float lastPointedY;

    private BoxCollider2D boxCollider2D;
    BoxCollider2D[] BoxColliders;

    Vector3 targetPos;

    private SortOrder sortOrder;

    private float scaleValue;

    public int number;

    public GenerateShapes generateShapes;
    private GameObject pointer;

    bool draggingItem = false;
    private bool xEven;
    private bool yEven;
	public List<Vector2> squarePositions = new List<Vector2>();
	private float scaleInNav;
	private bool dropInNav;
	private Vector2 navPosition;
	private Vector2 lastSquareNavPosition;
    public Transform objects;
    public Transform nav;
    public float lastScale;
    public float height;
    public float navEnd;

    private bool pointerHasPos;
    private float screenWidth;
    public bool isInNav = true;

    public Transform content;

    public bool levelWon;

    private float rotationSpeed;
    private Vector2 direction;

    public void CustomStart()
    {
        isInNav = true;

        content = GameObject.Find("/UI/Nav/Content").transform;

        generateShapes = GameObject.Find("/Generator").GetComponent<GenerateShapes>();
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

        screenWidth = Camera.main.orthographicSize * 2 * Screen.width / Screen.height;

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

        if (AverageX.Equals(Mathf.Round(AverageX)))
        {
            xEven = true;
        }
        else
        {
            xEven = false;
        }

        if (AverageY.Equals(Mathf.Round(AverageY)))
        {
            yEven = true;
        }
        else
        {
            yEven = false;
        }

       // Debug.Log(number + ": " + "highest y: " + HighestY + ", lowest y: " + LowestY + ", yEven: " + yEven);

        transform.position = new Vector2(AverageX, AverageY);

        foreach (Transform square in squares)
        {
            square.position = new Vector2(square.position.x-transform.position.x, square.position.y-transform.position.y);

            gameObject.AddComponent<BoxCollider2D>().offset = square.GetComponent<BoxCollider2D>().offset;
            Destroy(square.GetComponent<BoxCollider2D>());
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

        int rotationDirection = UnityEngine.Random.Range(0, 2);
        rotationSpeed = UnityEngine.Random.Range(5f, 10f);

        if (rotationDirection == 1)
        {
            rotationSpeed = -rotationSpeed;
        }

        direction = UnityEngine.Random.insideUnitCircle;
        direction.Normalize();
    }

    private void Update()
    {
        if (levelWon)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            transform.Translate(direction * 0.1f * Time.deltaTime);

            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.sortingOrder = 19;
            }
        }

        else if (draggingItem)
		{

			float xPos;
			float yPos;

			if (xEven)
			{
				xPos = scaleValue * Mathf.Round(transform.position.x / scaleValue);
			}
			else
			{
				xPos = scaleValue * (Mathf.Floor(transform.position.x / scaleValue) + 0.5f);
			}

			if (yEven)
			{
                yPos = (Mathf.Round((transform.position.y - 0.6f) / scaleValue) * scaleValue) + 0.6f;
            }
			else
			{
                yPos = (Mathf.Round((transform.position.y - (0.6f - (scaleValue / 2))) / scaleValue) * scaleValue) + (0.6f - (scaleValue / 2));
            }

            float tempYPos = yPos;
            
            bool objectsOverlapping = false;
            if (!Mathf.Approximately(lastPointedX, xPos) || !Mathf.Approximately(lastPointedY, yPos))
			{
                for (int i = 0; i < transform.childCount - 1; i++)
				{
                    float x = Mathf.Round((transform.GetChild(i).localPosition.x + (xPos / scaleValue)) * 10f) / 10f;
					float y = Mathf.Round((transform.GetChild(i).localPosition.y + (yPos / scaleValue)) * 10f) / 10f;

                    float lossyX = transform.GetChild(i).position.x;
                    float lossyY = transform.GetChild(i).position.y;

                    //if (y > 7.5f || y < -5.5f)
                    if (lossyY > 3.6f || lossyY < -2.9f)
					{
						objectsOverlapping = true;
					}

                    /*
                    0.6 --> -4.5
                    x/scalevalue = -4.5 
                    */

                    else if (lossyX > screenWidth/2 || lossyX < -screenWidth/2)
					{
						objectsOverlapping = true;
					}

					else
					{
                        for (int j = 0; j < sortOrder.positions.shapesPos.Count; j++)
						{
                            if (sortOrder.positions.shapesPos[j] != null && j != number - 1)
							{
                                foreach (Vector2 vector in sortOrder.positions.shapesPos[j].squaresPos)
								{
                                    if (vector.x.Equals(x) && y.Equals(vector.y))
                                    {
                                        objectsOverlapping = true;
                                    }

                                    else
                                    {
                                       //Debug.Log("vector.x=" + vector.x + " x=" + x + " " + (vector.x.Equals(x)) + "; " + " vector.y=" + vector.y + " y=" + y + " " + vector.y.Equals(y));
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

                    sortOrder.ObjectMoved();
				}
			}

            if (lastPointedX != 0 || lastPointedY != 0)
            {
                pointerHasPos = true;
            }

            pointer.transform.position = new Vector2(lastPointedX, lastPointedY);

			if (transform.localPosition.y < - navEnd)
            {
                float percentage = (((-transform.localPosition.y) - navEnd) / (4f-navEnd));

                float scale;
                if (transform.position.y > -4f)
                {
                    scale = scaleValue - (percentage * (scaleValue - scaleInNav));
                }
                else
                {
                    scale = scaleInNav;
                }

                lastScale = scale;

				transform.localScale = new Vector2(scale, scale);

				dropInNav = true;

				pointer.SetActive(false);

                pointerHasPos = false;

                lastPointedX = 0;
                lastPointedY = 0;

                isInNav = true;
            }

			else {
				transform.localScale = new Vector2(scaleValue, scaleValue);

                if (pointerHasPos)
                {
                    pointer.SetActive(true);
                    dropInNav = false;
                    isInNav = false;
                }
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
        
        if (isInNav) {
			transform.localScale = new Vector2(scaleInNav, scaleInNav);
            transform.SetParent(content.transform);

            for (int i = 0; i < content.childCount; i++)
            {
                Transform child = content.transform.GetChild(i);

                if (child.name != "Background Scroll" && child != transform) {
                    if (child.transform.position.x > transform.position.x)
                    {
                        Debug.Log(i);

                        transform.SetSiblingIndex(i);

                        if (!content.GetComponent<Nav>().objectsInNav.Contains(transform))
                        {
                            content.GetComponent<Nav>().objectsInNav.Insert(i, transform);
                        }
                        break;
                    }
                }

                if (i == content.childCount - 1)
                {
                    if (!content.GetComponent<Nav>().objectsInNav.Contains(transform))
                    {
                        content.GetComponent<Nav>().objectsInNav.Add(transform);
                    }
                }
            }

            sortOrder.positions.shapesPos[number - 1].squaresPos.Clear();
        }

		else {
			transform.position = new Vector2(lastPointedX, lastPointedY);
            pointer.transform.position = new Vector2(0, 0);

            squarePositions.Clear();

            transform.SetParent(objects);

            content.GetComponent<Nav>().objectsInNav.Remove(transform);

            for (int i = 0; i < transform.childCount - 1; i++)
            {
                float x = transform.GetChild(i).localPosition.x + (transform.position.x / scaleValue);
                float y = transform.GetChild(i).localPosition.y + (transform.position.y / scaleValue);

                squarePositions.Add(new Vector2(Mathf.Round(x * 10f) / 10f, Mathf.Round(y * 10f) / 10f));
            }
            
            sortOrder.positions.shapesPos[number - 1].squaresPos = squarePositions;
		}

        sortOrder.UpdatePositions();

        content.GetComponent<Nav>().PosChildren();
    }

    public void PosInNav() {
		float largestDifference;

		navPosition = transform.localPosition;
		lastSquareNavPosition = squares[squares.Count - 1].position;

        navEnd = 3f;

        if (HighestX-LowestX > HighestY-LowestY) {
			largestDifference = HighestX - LowestX;
		}
        
		else {
			largestDifference = HighestY - LowestY;
		}

		scaleInNav = 0.5f / (float)largestDifference;
        
        if (scaleInNav > 0.3f)
        {
            scaleInNav = 0.3f;
        }
        
		transform.localScale = new Vector2(scaleInNav, scaleInNav);    
	}
}
