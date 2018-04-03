using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;

    private float spaceToFinger;

    public ScrollRect scrollRect;

    public float screenHeight;
    public float rowHeight;

    public Transform Fillobject;

    private float halfScaleValue;

	private void Start()
	{
        screenHeight = Screen.height;
        rowHeight = screenHeight / 6;
        spaceToFinger = 0.5f;

        halfScaleValue = transform.GetChild(0).GetComponent<PieceScript>().halfScaleValue;
	}

	void Update ()
	{
	    if (HasInput)
	    {
	        DragOrPickUp();

            if (draggingItem)
            {
                scrollRect.enabled = false;
            }
	    }
	    else
	    {
            if (draggingItem) {
                DropItem();

                scrollRect.enabled = true;
            }
	    }
	}

    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;
        if (draggingItem)
        {
            draggedObject.transform.position = inputPosition + touchOffset;

            draggedObject.transform.position += new Vector3(0f, 0.75f, 0f);

            if (draggedObject.transform.localPosition.y < 0)
            {
                draggedObject.transform.localScale = new Vector2(halfScaleValue, halfScaleValue);
            }

            else if (draggedObject.transform.localPosition.y < rowHeight)
            {
                float value = ((draggedObject.transform.localPosition.y / rowHeight) * halfScaleValue) + halfScaleValue;
                draggedObject.transform.localScale = new Vector2(value, value);
            }

            else 
            {
                draggedObject.transform.localScale = new Vector2(halfScaleValue * 2, halfScaleValue * 2);
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
                }
            }
        }
    }

    void DropItem()
    {
        if (draggedObject.transform.localPosition.y > screenHeight/6) {
            draggedObject.transform.SetParent(Fillobject);
            draggedObject.GetComponent<PieceScript>().Drop();
        }

        else {
            draggedObject.transform.SetParent(transform);
            draggedObject.GetComponent<PieceScript>().PositionInRow();
        }

        draggingItem = false;
    }

    //get info
    Vector2 CurrentTouchPosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private bool HasInput
    {
        get
        {
            return Input.GetMouseButton(0);
        }
    }
}
