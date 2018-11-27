using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nav : MonoBehaviour {
    public Transform navParent;
    public Transform container;
    public RectTransform rect;

    public List<Transform> objectsInNav = new List<Transform>();

    public void PosChildren() {
        rect.sizeDelta = new Vector2(objectsInNav.Count + 0.5f, 2f);
        rect.localPosition = new Vector2(0f, -4f);

        for (int i = 0; i < objectsInNav.Count; i++)
        {
            float x = i - (objectsInNav.Count - 1f) / 2f;
            objectsInNav[i].localPosition = new Vector2(x, -4f);

            if (objectsInNav[i].name != "Background Scroll")
            {
                objectsInNav[i].GetComponent<ShapeScript>().PosInNav();
            }
        }

        if (objectsInNav.Count > Mathf.FloorToInt(Camera.main.orthographicSize * 2 * Screen.width / Screen.height))
        {
            navParent.GetComponent<ScrollRect>().enabled = true;
            RectTransform containerRect = container.GetComponent<RectTransform>();

            float totalOffset = -((Camera.main.orthographicSize * 2 * Screen.width / Screen.height) + rect.offsetMin.x - rect.offsetMax.x);

            /*
            navParent.GetComponent<ScrollRect>().enabled = true;
            RectTransform containerRect = container.GetComponent<RectTransform>();

            float totalOffset = -((Camera.main.orthographicSize * 2 * Screen.width / Screen.height) + rect.offsetMin.x - rect.offsetMax.x);
            float prevTotalOffset = Mathf.Abs(containerRect.offsetMin.x) + Mathf.Abs(containerRect.offsetMax.x);

            if (totalOffset.Equals(0f))
            {
                containerRect.offsetMin = new Vector2(0, 0);
                containerRect.offsetMax = new Vector2(totalOffset, 0);
            }

            else 
            {
                Debug.Log("totalOffset: " + totalOffset + ", prevLeftOffset: " + containerRect.offsetMin.x + ", prevRightOffset: " + containerRect.offsetMax.x + ", prevTotalOffset: " + prevTotalOffset);

                containerRect.offsetMin = new Vector2(-totalOffset * containerRect.offsetMin.x / prevTotalOffset, 0);
                containerRect.offsetMax = new Vector2(totalOffset * containerRect.offsetMax.x / prevTotalOffset, 0);
            }
            */
        }
        else
        {
            navParent.GetComponent<ScrollRect>().enabled = false;
            container.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            container.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
    }
}