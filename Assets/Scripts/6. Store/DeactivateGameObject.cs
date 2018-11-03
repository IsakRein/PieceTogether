using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateGameObject : MonoBehaviour {

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
