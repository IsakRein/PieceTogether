using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBalance : MonoBehaviour {

    public PopUp popUp;
    public SceneLoader sceneLoader;

    public void CheckIfPossible(string powerUp)
    {
        if (powerUp == "Hint")
        {
            if (Utilities.hintCount > 0)
            {
                popUp.InitPopUp(powerUp);
            }
            else
            {
                sceneLoader.LoadScene("6. Store");
            }

        }
        else if (powerUp == "Skip")
        {
            if (Utilities.skipCount > 0)
            {
                popUp.InitPopUp(powerUp);
            }
            else
            {
                sceneLoader.LoadScene("6. Store");
            }
        }
    }
}
