using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp2 : MonoBehaviour
{
    public GameObject blur;
    public GameObject couldntLogIn;
    public GameObject confirmRestore;
    public GameObject restoreSuccess;
    public GameObject restoreFailure;

    public Animator blurAnimator;
    public Animator couldntLogInAnim;
    public Animator confirmRestoreAnim;
    public Animator restoreSuccessAnim;
    public Animator restoreFailureAnim;

    private string startedType;

    private void Start()
    {
        blurAnimator = blur.GetComponent<Animator>();
        couldntLogInAnim = couldntLogIn.GetComponent<Animator>();
        confirmRestoreAnim = confirmRestore.GetComponent<Animator>();
        restoreSuccessAnim = restoreSuccess.GetComponent<Animator>();
        restoreFailureAnim = restoreFailure.GetComponent<Animator>();
    }

    public void InitPopUp(string type)
    {
        startedType = type;
        blur.SetActive(true);

        if (type == "FailedToLogIn")
        {
            blurAnimator.SetTrigger("Start");
            couldntLogIn.SetActive(true);
            couldntLogInAnim.SetTrigger("Start");
        }

        if (type == "ConfirmRestore")
        {
            blurAnimator.SetTrigger("Start");
            confirmRestore.SetActive(true);
            confirmRestoreAnim.SetTrigger("Start");
        }
    }

    public void StopPopUp()
    {
        if (startedType == "FailedToLogIn")
        {
            blurAnimator.SetTrigger("Stop");
            couldntLogInAnim.SetTrigger("Stop");
        }

        if (startedType == "ConfirmRestore")
        {
            blurAnimator.SetTrigger("Stop");
            confirmRestoreAnim.SetTrigger("Stop");
        }
    }

    public void DeSelectProduct()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Animator>().SetTrigger("Stop");
        }
    }
}