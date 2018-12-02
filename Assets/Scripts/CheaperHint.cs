using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheaperHint : MonoBehaviour {

    public float maxTime;
    public float time;

    public PopUp popUp;

	// Use this for initialization
	void Start () {
        time = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            //activate help


            time = maxTime;
        }
	}
}
