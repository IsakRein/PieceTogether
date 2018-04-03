using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject shape;
    public GameObject fill;

    void Start () {
        shape = transform.GetChild(0).gameObject;
        fill = transform.GetChild(1).gameObject;

        //import from manager
        //also import shape
        shape.GetComponent<GenerateShapes>().Generate(2, 0, new Color32(0, 0, 0, 255));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
