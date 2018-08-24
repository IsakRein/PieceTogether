using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    public int levelNumber;
    public bool levelAvaliable;

    private Image image;
    private Button button;
    private TextMeshProUGUI text;

    public Color black;
    public Color white;

    private void Start()
    {
        image = gameObject.GetComponentInChildren<Image>();
        button = gameObject.GetComponentInChildren<Button>();
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        text.SetText("" + levelNumber);

        if (levelAvaliable) 
        {
            image.color = white;
            text.color = black;
        }
        else
        {
            image.color = black;
            text.color = white;

            button.interactable = false;
        }
    }

    public void Load()
    {
        Utilities.currentLevel = levelNumber;
        Utilities.LoadScene("4. Game");
    }
}
