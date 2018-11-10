 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundShapeScript : MonoBehaviour {
    
    public Vector3 target;
    float speed;

    private void Start()
    {
        target = new Vector3(transform.position.x, transform.position.y + (Camera.main.orthographicSize * 10 * Screen.height / Screen.width));

        speed = 2f + Random.Range(-0.5f, 0.5f);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);    

        if (transform.position.y >= target.y)
        {
            Destroy(gameObject);
        }
    }

}
