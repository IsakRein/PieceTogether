using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript: MonoBehaviour {

    public Transform row;
    public Vector2 correctPos;
    public Vector2 currentPos;
    public int number;
    private float faultMargin = 0.3f;
    public float defaultScaleValue;
    public float halfScaleValue;

	public void CustomStart() {
        row = GameObject.Find("Row").transform;
        transform.SetParent(row);

        correctPos = transform.position;
        row.GetComponent<RowScript>().correctPositions.Add(correctPos);
        defaultScaleValue = transform.localScale.x;
        halfScaleValue = defaultScaleValue / 2;
    }

    private void Update()
    {
        currentPos = transform.position;
    }

    public void SetHalfScale() {
        transform.localScale = new Vector2(halfScaleValue, halfScaleValue);;
    }

    public void PositionInRow()
    {
        float xPos = -100 + (number * 250f);
        transform.localPosition = new Vector2(xPos, 0f);
        transform.localScale = new Vector2(halfScaleValue, halfScaleValue);
    }

    public void Drop () {
        if ((Mathf.Abs(currentPos.x - correctPos.x) < faultMargin) && (Mathf.Abs(currentPos.y - correctPos.y) < faultMargin)) 
        {
            transform.position = correctPos; 
        }

    }

	
}
