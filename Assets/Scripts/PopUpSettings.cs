using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSettings : MonoBehaviour
{
    public string title;
    public string subtitle;
    public string button;


    public TextMeshProUGUI titleObj;
    public TextMeshProUGUI subtitleObj;
    public TextMeshProUGUI buttonObj;
       
    void Start()
    {
        titleObj.SetText(title);
        subtitleObj.SetText(subtitle);
        buttonObj.SetText(button);
    }

}
