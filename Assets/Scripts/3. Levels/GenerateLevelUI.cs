using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevelUI : MonoBehaviour {

    public GameObject levelPrefab;
    public GameObject canvas;

    private RectTransform rectTransform;

    public GameObject scrollRectBack;

    public int currentPage;
    public int lastPage;

    private float width;

    public Sprite pressableButton;
    public Sprite notPressableButton;

    public Sprite selectedCircle;
    public Sprite notSelectedCircle;

    public Button leftButton;
    public Button rightButton;

    public List<Image> circles = new List<Image>();

    void Start () {
        rectTransform = gameObject.GetComponent<RectTransform>();

        int lastFinishedLevel;

        if (!PlayerPrefs.HasKey(Utilities.currentPack)) 
        {
            PlayerPrefs.SetInt(Utilities.currentPack, 0);
        }

        lastFinishedLevel = PlayerPrefs.GetInt(Utilities.currentPack);

        width = canvas.GetComponent<RectTransform>().rect.width;

        rectTransform.sizeDelta = new Vector2(5* width, 10);

        gameObject.transform.localPosition = new Vector2((2* width), 0);

        scrollRectBack.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 0);


        GameObject backgroundScroll;
        backgroundScroll = new GameObject("Background Scroll");
        backgroundScroll.transform.SetParent(gameObject.transform);
        backgroundScroll.AddComponent<Image>().color = new Color(1,1,1);
        backgroundScroll.GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
        backgroundScroll.GetComponent<RectTransform>().localPosition = new Vector2(0, -30f);
        backgroundScroll.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 6f, 730f);

        for (int i = 1; i <= 5; i++) {
            GameObject page;
            page = new GameObject("Page " + i);
            page.transform.SetParent(gameObject.transform);
            page.AddComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            page.transform.localScale = new Vector2(1, 1);
           
            page.transform.localPosition = new Vector2((i - 3.5f) * width, 0);


            for (int j= 1; j <= 30; j++)
            {
                GameObject level = Instantiate(levelPrefab, page.transform);

                int number = j + ((i - 1) * 30);

                level.name = "" + number;
                LoadLevel loadLevel = level.GetComponent<LoadLevel>();
                loadLevel.levelNumber = number;

                loadLevel.levelAvaliable = true;

                if (number > lastFinishedLevel + 1) {
                    loadLevel.levelAvaliable = false;
                }

                level.transform.localPosition = new Vector2((-200 + (100 * ((j-1) % 5))), (270 - ((Mathf.FloorToInt((float)((j - 0.01)/ 5))) * 120)));
            }
        }

        scrollRectBack.GetComponent<RectTransform>().localPosition = new Vector2(width / 2, 0);
    }

    private void Update()
    {
        float currentPos = (-(rectTransform.localPosition.x - (2 * width))/width)+0.5f;
        currentPage = Mathf.FloorToInt(currentPos);

        if (lastPage != currentPage)
        {
            lastPage = currentPage;

            for (int i = 0; i < circles.Count; i++)
            {
                if (i == currentPage)
                {
                    circles[i].sprite = selectedCircle;
                }
                else
                {
                    circles[i].sprite = notSelectedCircle;
                }
            }

            if (currentPage == 0)
            {
                leftButton.interactable = false;
                leftButton.GetComponent<Image>().sprite = notPressableButton;        
            }
            else
            {
                leftButton.interactable = true;
                leftButton.GetComponent<Image>().sprite = pressableButton;
            }

            if (currentPage == 4)
            {
                rightButton.interactable = false;
                rightButton.GetComponent<Image>().sprite = notPressableButton;
            }
            else
            {
                rightButton.interactable = true;
                rightButton.GetComponent<Image>().sprite = pressableButton;
            }
        }
    }
}

