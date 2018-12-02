using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour {

    public GameObject blur;
    public GameObject menu;
    public GameObject levelWon;
    public GameObject hint;
    public GameObject skip;
    public GameObject cheaperHint;

    public InputManager inputManager;

    public Animator blurAnimator;
    public Animator menuAnimator;
    public Animator levelWonAnimator;
    public Animator hintAnimator;
    public Animator skipAnimator;
    public Animator cheaperHintAnimator;

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

        else if (startedType == "Hint")
        {
            blurAnimator.SetTrigger("Start");
            hint.SetActive(true);
            hintAnimator.SetTrigger("Start");
        }

        else if (startedType == "Skip")
        {
            blurAnimator.SetTrigger("Start");
            skip.SetActive(true);
            skipAnimator.SetTrigger("Start");
        }

        else if (startedType == "CheaperHint")
        {
            blurAnimator.SetTrigger("Start");
            cheaperHint.SetActive(true);
            cheaperHintAnimator.SetTrigger("Start");
        }
    }

    public void StopPopUp()
    {
        if (startedType == "Menu")
        {
            blurAnimator.SetTrigger("Stop");
            menuAnimator.SetTrigger("Stop");
        }

        else if (startedType == "Hint")
        {
            blurAnimator.SetTrigger("Stop");
            hintAnimator.SetTrigger("Stop");
        }
       
        else if (startedType == "Skip")
        {
            Debug.Log("stop");
            blurAnimator.SetTrigger("Stop");
            skipAnimator.SetTrigger("Stop");
        }

        else if (startedType == "CheaperHint")
        {
            blurAnimator.SetTrigger("Start");
            cheaperHint.SetActive(false);
            cheaperHintAnimator.SetTrigger("Stop");
        }

        inputManager.interactable = true;
    }
}