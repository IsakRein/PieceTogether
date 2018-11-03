using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour {

    public GameObject blur;
    public GameObject menu;
    public GameObject levelWon;

    public InputManager inputManager;

    public Animator blurAnimator;
    public Animator menuAnimator;
    public Animator levelWonAnimator;

    private string startedType;

    public void InitPopUp(string type)
    {
        startedType = type;

        inputManager.interactable = false;
        blur.SetActive(true);

        if (startedType == "Menu")
        {  
            blurAnimator.SetTrigger("Start");
            menu.SetActive(true);
            menuAnimator.SetTrigger("Start");
        }

        else if (startedType == "Level Won")
        {
            blurAnimator.SetTrigger("StartDelay");
            levelWon.SetActive(true);
            levelWonAnimator.SetTrigger("StartDelay");
        }
    }

    public void StopPopUp()
    {
        if (startedType == "Menu")
        {
            blurAnimator.SetTrigger("Stop");
            menuAnimator.SetTrigger("Stop");
            inputManager.interactable = true;
        }
    }
}