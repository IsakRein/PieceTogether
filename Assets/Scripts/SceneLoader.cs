using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

    public void LoadScene(string scene) 
    {
        Utilities.LoadScene(scene);
    }

    public void LoadPreviousScene() 
    {
        Utilities.LoadPreviousScene();
    }
}
