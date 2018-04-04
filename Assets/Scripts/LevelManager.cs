using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject shape;
    public GameObject fill;

    public InputManager inputManager;

    public int extraPoints;
    public int subShatterSteps;
    public Color32 shapeColor;
    public Sprite shapeSprite;

    void Start () {
        shape = transform.GetChild(0).gameObject;
        fill = transform.GetChild(1).gameObject;

        shape.GetComponent<SpriteRenderer>().sprite = shapeSprite;
        fill.GetComponent<SpriteRenderer>().sprite = shapeSprite;

        inputManager = transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<InputManager>();

        shape.SetActive(true);
        fill.SetActive(true);


        //import from manager
        //also import shape

        shape.GetComponent<GenerateShapes>().Generate(extraPoints, subShatterSteps, shapeColor);

        inputManager.CustomStart();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
