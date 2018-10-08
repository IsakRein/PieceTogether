using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;

    private Vector2 firstPosition;
    private bool firstSwipeHasBeenDone = false;
    public bool movingEnabled;

    public ScrollRect scrollRect;

    private Nav nav;

    private void Start()
    {
        nav = GameObject.Find("/UI/Nav/Content").GetComponent<Nav>();
    }

    void Update()
    {
        if (HasInput)
        {
            DragOrPickUp();
        }
        else
        {
            if (draggingItem)
            {
                DropItem();
            }
        }
    }

    Vector2 CurrentTouchPosition
    {
        get
        {
            Vector2 inputPos;
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return inputPos;
        }
    }

    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;

        if (draggingItem)
        {
            if (firstSwipeHasBeenDone == false)
            {
                float xDif = Mathf.Abs(firstPosition.x - inputPosition.x);
                float yDif = Mathf.Abs(firstPosition.y - inputPosition.y);

                if (draggedObject.GetComponent<ShapeScript>().isInNav)
                {
                    if (xDif > 0.1f || yDif > 0.1f)
                    {
                        if (draggedObject.GetComponent<ShapeScript>().isInNav)
                        {
                            if (xDif < 3f * yDif)
                            {
                                movingEnabled = true;
                                draggedObject.GetComponent<ShapeScript>().DraggingItem();
                                scrollRect.enabled = false;
                            }
                            else
                            {
                                movingEnabled = false;
                            }
                        }

                        firstSwipeHasBeenDone = true;
                    }
                }
                else {
                    movingEnabled = true;
                    draggedObject.GetComponent<ShapeScript>().DraggingItem();
                    scrollRect.enabled = false;
                }
            }

            if (movingEnabled)
            {
                draggedObject.transform.position = inputPosition + touchOffset;
            }

        }

        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null)
                {
                    draggingItem = true;
                    draggedObject = hit.transform.gameObject;
                    touchOffset = (Vector2)hit.transform.position - inputPosition;
                    firstPosition = CurrentTouchPosition;
                }
            }
        }
    }

    private bool HasInput
    {
        get
        {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }

    void DropItem()
    {
        draggingItem = false;
        firstSwipeHasBeenDone = false;

        if (nav.objectsInNav.Count > Mathf.FloorToInt(Camera.main.orthographicSize * 2 * Screen.width / Screen.height))
        {
            scrollRect.enabled = true;
        }

        if (movingEnabled) {
            draggedObject.GetComponent<ShapeScript>().DropItem();
        }
    }
}