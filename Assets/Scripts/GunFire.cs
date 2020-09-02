using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    public Transform shootTransform;
    public float shootForce = 5f;
    public float timer = 3f;
    public float speed = 3f;
    public AudioClip shootSound;

    private Vector3 pos1;
    private Vector3 pos2;
    // Start is called before the first frame update
    void Start()
    {
        pos1 = transform.position;
        pos2 = new Vector3(transform.position.x - 0.5f,transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Shoot();
            timer = 3f;
        }

        if (transform.position != pos1)
        {
            transform.localPosition = Vector3.Lerp(transform.position, pos1, 0.1f);
        }
    }

    void Shoot()
    {
        var audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.PlayOneShot(this.shootSound);
        }

        transform.localPosition = Vector3.Lerp(pos1, pos2, 1f);
        GameObject ball = Instantiate(ballPrefab, shootTransform.position, Quaternion.identity) as GameObject;
        if (transform.position.x > Camera.main.transform.position.x)
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-shootForce, 0f));
        else
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(shootForce, 0f));
    }
}
