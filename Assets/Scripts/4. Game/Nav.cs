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

        int filledScreenCount = Mathf.FloorToInt(Camera.main.orthographicSize * 2 * Screen.width / Screen.height);

        if (objectsInNav.Count > filledScreenCount)
        {
            navParent.GetComponent<ScrollRect>().enabled = true;
            RectTransform containerRect = container.GetComponent<RectTransform>();

            float totalOffset = (objectsInNav.Count - filledScreenCount - 1) + 0.8f;

            if (containerRect.offsetMin.x.Equals(0f) && containerRect.offsetMax.x.Equals(0f))
            {
                containerRect.offsetMin = new Vector2(0, 0);
                containerRect.offsetMax = new Vector2(totalOffset, 0);
            }
            else
            {
                float prevOffset = containerRect.offsetMin.x + (-containerRect.offsetMax.x);
                float leftShare = containerRect.offsetMin.x / prevOffset;
                float rightShare = -containerRect.offsetMax.x / prevOffset;

                Debug.Log(leftShare + "/" + rightShare);

                if (leftShare < rightShare)
                {
                    containerRect.offsetMax = new Vector2(totalOffset + containerRect.offsetMin.x, 0);
                }

                else
                {
                    containerRect.offsetMin = new Vector2(containerRect.offsetMax.x - totalOffset, 0);
                }

                /*
                containerRect.offsetMin = new Vector2(-leftShare * totalOffset, 0);
                containerRect.offsetMax = new Vector2(rightShare * totalOffset, 0);
                */
            }
        }
        else
        {
            navParent.GetComponent<ScrollRect>().enabled = false;
            container.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            container.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }

        rect.localPosition = new Vector2(0f, -4f);
    }
}