using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscListener : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Utilities.previousScenes.Count != 0)
            {
                Utilities.LoadPreviousScene();
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
