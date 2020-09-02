using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyRotate : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float pingPong = Mathf.PingPong(Time.time * speed, 1f);
        //float x = Mathf.Lerp(transform.localScale.x, 1.4f, pingPong);
        //float y = Mathf.Lerp(transform.localScale.x, 1.4f, pingPong);
        //transform.localScale = new Vector3(x, y, 1);
        transform.Rotate(0, 0, -speed);
    }
}
