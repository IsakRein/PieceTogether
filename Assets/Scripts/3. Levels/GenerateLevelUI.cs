using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevelUI : MonoBehaviour {

    public GameObject levelPrefab;
    public GameObject canvas;

    private RectTransform rectTransform;
	
    void Start () {
        rectTransform = gameObject.GetComponent<RectTransform>();

        int lastFinishedLevel;

        if (!PlayerPrefs.HasKey(Utilities.currentPack)) 
        {
            PlayerPrefs.SetInt(Utilities.currentPack, 0);
        }

        lastFinishedLevel = PlayerPrefs.GetInt(Utilities.currentPack);

        float width = canvas.GetComponent<RectTransform>().rect.width;

        rectTransform.sizeDelta = new Vector2(4* width, 10);

        gameObject.transform.localPosition = new Vector2((2* width), 0);

        for (int i = 1; i <= 5; i++) {
            GameObject page;
            page = new GameObject("Page " + i);
            page.transform.SetParent(gameObject.transform);
            page.AddComponent<RectTransform>();
            page.transform.localScale = new Vector2(1, 1);
           
            page.transform.localPosition = new Vector2((i - 1) * width, 0);


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
	}
}

