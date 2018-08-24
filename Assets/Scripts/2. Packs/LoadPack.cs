using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPack : MonoBehaviour {

    public void Load (string pack) 
    {
        Utilities.currentPack = pack;
        Utilities.LoadScene("3. Levels");
	}
}
