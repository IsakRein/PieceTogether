using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadPack : MonoBehaviour {

    public string pack;
    TextMeshProUGUI text; 

    private void Start()
    {
        text = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        int levelsDone;

        if (PlayerPrefs.HasKey(pack)) {
            levelsDone = PlayerPrefs.GetInt(pack);
        }
        else
        {
            levelsDone = 0;
        }

        text.SetText("" + levelsDone + "/150");
    }

    public void Load () 
    {

        Utilities.currentPack = pack;
        Utilities.LoadScene("3. Levels");
	}
}
