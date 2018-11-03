using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {

    public float value = 0;
    public float amplitude;
    public float maxOpacity;

    public bool hasStarted = false;
    private bool direction;

    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            if (direction)
            {
                value += Time.deltaTime;

                if (value >= amplitude)
                {
                    direction = false;
                }
            }
         
            else
            {
                value -= Time.deltaTime;

                if (value <= 0)
                {
                    direction = true;
                }
            }

            foreach (SpriteRenderer sprite in sprites)
            {
                Color color = sprite.color;
                Color newColor = new Color(color.r, color.g, color.b, maxOpacity * (value / amplitude));
                sprite.color = newColor;
            }
        }
    }
}
