using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nav : MonoBehaviour {

    public Transform navCircles;
    public Image leftArrow;
    public Image rightArrow;

    public Sprite filledArrow;
    public Sprite hollowArrow;

    public Sprite filledCircle;
    public Sprite hollowCircle;

    public int currentNav;
    public int navCount;

    public Transform objects;


    private void Start()
    {
        RefreshSprites();
    }

    public void NavLeft()
    {
        if (currentNav != 1)
        {
            currentNav = currentNav - 1;
            float x = transform.position.x + 24f;
            transform.position = new Vector2(x, 0);
        }

        RefreshSprites();
    }

    public void NavRight()
    {
        if (currentNav != navCount)
        {
            currentNav = currentNav + 1;
            float x = transform.position.x - 24f;
            transform.position = new Vector2(x, 0);
        }

        RefreshSprites();
    }

    void RefreshSprites()
    {
        if (currentNav == 1)
        {
            leftArrow.sprite = hollowArrow;
        }
        else
        {
            leftArrow.sprite = filledArrow;
        }

        if (currentNav == navCount)
        {
            rightArrow.sprite = hollowArrow;
        }
        else
        {
            rightArrow.sprite = filledArrow;
        }

        for (int i = 0; i < navCircles.childCount; i++)
        {
            if (i + 1 == currentNav)
            {
                navCircles.GetChild(i).GetComponent<SpriteRenderer>().sprite = filledCircle;
            }
            else
            {
                navCircles.GetChild(i).GetComponent<SpriteRenderer>().sprite = hollowCircle;
            }
        }
    }
}
