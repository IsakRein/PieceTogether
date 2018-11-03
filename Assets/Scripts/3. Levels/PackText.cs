using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PackText : MonoBehaviour {

    private TextMeshProUGUI text;

	// Use this for initialization
	void Start () 
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();  
        text.SetText(Utilities.currentPack.ToLower());
    }

}
