using UnityEngine;
using System.Collections;

public class ShaderEditor : MonoBehaviour
{
    public float blurValue;
    public Material material;

    private void Start()
    {
        material.SetFloat("_Size", 0);
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_Size", blurValue);
    }
}
