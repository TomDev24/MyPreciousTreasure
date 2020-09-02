using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 a, b;
    // Start is called before the first frame update
    void Start()
    {
        a = transform.position;
        b = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(a, b, pingPong);
    }
}
