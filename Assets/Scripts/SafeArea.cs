using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeArea : MonoBehaviour
{
    public int scaleType = 1;

    RectTransform Panel;
    Rect LastSafeArea = new Rect(0, 0, 0, 0);
    public CanvasScaler canvasScaler;

    void Awake()
    {
        Panel = GetComponent<RectTransform>();
        canvasScaler = transform.parent.GetComponent<CanvasScaler>();

        Refresh();
    }

    void Refresh()
    {
        if (GetSafeArea().height != Screen.height)
        {
            Panel.offsetMax = new Vector2(0, (-(Screen.height - GetSafeArea().height) / 2f) - 25f);
            Panel.offsetMin = new Vector2(0, ((Screen.height - GetSafeArea().height) / 2f));
        }

        if (scaleType == 1)
        {
            //does nothing
        }

        else if (scaleType == 2)
        {
            SetSafeArea(0.35f, 0.5f, 0.45f);
        }

        else if (scaleType == 3)
        {
            SetSafeArea(1f, 0.5f, 0.0f);
        }
    }

    void Update()
    {
        //Refresh();
    }

    private void SetSafeArea(float min, float mid, float max)
    {
        if (((float)Screen.height / (float)Screen.width) <= (16f / 9f))
        {
            float frac1 = ((float)Screen.height / (float)Screen.width) - (4f / 3f);
            float frac2 = (16f / 9f) - (4f / 3f);
            float percent = frac1 / frac2;

            Debug.Log(frac1 + " " + frac2 + " " + percent);

            float val = min + (percent * (mid - min));

            if (val < min)
            {
                val = min;
            }
      
            canvasScaler.matchWidthOrHeight = val;
        }

        else
        {
            float frac1 = ((float)Screen.height / (float)Screen.width) - (16f / 9f);
            float frac2 = 19.5f / 9f - (16f / 9f);
            float percent = frac1 / frac2;

            float val = mid - (percent * (mid - max));

            if (val < max)
            {
                val = max;
            }

            canvasScaler.matchWidthOrHeight = val;
        }
    }
     
    Rect GetSafeArea()
    {
        return Screen.safeArea;
    }

    void ApplySafeArea(Rect r)
    {
        LastSafeArea = r;

        // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        Vector2 anchorMin = r.position;
        Vector2 anchorMax = r.position + r.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        Panel.anchorMin = anchorMin;
        Panel.anchorMax = anchorMax;
    }
}