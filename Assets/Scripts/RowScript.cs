using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowScript : MonoBehaviour {

    public GameObject mainSprite;
    public List<GameObject> pieces = new List<GameObject>();
    public List<Vector2> correctPositions = new List<Vector2>();

    RectTransform rT;

	// Use this for initialization
	public void CustomStart () {
        GameObject.Destroy(mainSprite);

        rT = GetComponent<RectTransform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.name = (i + 1).ToString();
            child.GetComponent<PieceScript>().number = i + 1;
            child.GetComponent<PieceScript>().SetHalfScale();

            pieces.Add(child);
        }

        rT.sizeDelta = new Vector2((((pieces.Count-1) * 250f) + 300f), 0f);

        foreach (GameObject piece in pieces)
        {
            float xPos = -100 + (piece.GetComponent<PieceScript>().number * 250f);
            piece.transform.localPosition = new Vector2(xPos, 0f);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
