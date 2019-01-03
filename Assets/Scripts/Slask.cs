using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slask : MonoBehaviour
{
    public void Find()
    {
        GameObject.Find("AdManager").GetComponent<AdManager>().Slask();
    } 

   
}
