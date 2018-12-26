using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PackText : MonoBehaviour {

    public TextMeshProUGUI text;

	// Use this for initialization
	void Start () 
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();  
        if (Utilities.currentPack != null)
        {
            text.SetText(Utilities.currentPack.ToLower());
        }
    }
}