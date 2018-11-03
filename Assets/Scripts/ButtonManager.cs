using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    private Button button;
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        //play sound?
        Utilities.Vibrate();
    }
}
