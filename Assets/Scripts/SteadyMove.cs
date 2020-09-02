using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyMove : MonoBehaviour
{
    public float upwardSpeed = 3f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = (new Vector2(0f, upwardSpeed));
    }
}
