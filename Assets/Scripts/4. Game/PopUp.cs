using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour {

    public GameObject blur;
    public GameObject menu;
    public GameObject levelWon;

    public InputManager inputManager;

    private string startedType;

    public void InitPopUp(string type)
    {
        blur.SetActive(true);
        inputManager.interactable = false;
        startedType = type;

        if (startedType == "Menu")
        {
            menu.SetActive(true);

        }

        else if (startedType == "Level Won")
        {
            levelWon.SetActive(true);
        }
    }

    public void StopPopUp()
    {
        if (startedType != "Level Won")
        {
            inputManager.interactable = true;
            blur.SetActive(false);
            menu.SetActive(false);
            levelWon.SetActive(false);
        }
    }
}