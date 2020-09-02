using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] private GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public float Temp()
    {
        float temp = (cam.transform.position.y * (1 - parallaxEffect));
        return temp;
    }

    void Update()
    {
        float temp = (cam.transform.position.y * (1 - parallaxEffect));
        float dist = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        //else if (temp < startpos - length) startpos -= length;
    }

    public GameObject GenerateNext()
    {
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3 position = new Vector3(transform.position.x, startpos + length, transform.position.z);
        GameObject newBg = Instantiate(gameObject, position, Quaternion.identity);
        newBg.transform.SetParent(gameObject.transform.parent);
        return newBg;
    }
}
