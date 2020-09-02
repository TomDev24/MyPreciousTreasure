using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapClass
{
    fly,
    star,
    gun
}

public class TrapManager : Singleton<TrapManager>
{
    public GameObject flyPrefab;
    public GameObject killStarPrefab;
    public GameObject gunPrefab;

    public GameObject[] platformPrefabs;

    private float camHeight;
    private float camWidth;
    private float timer = 3f;
    public float timerSet = 3f;

    // Start is called before the first frame update
    void Start()
    {
        camHeight = 2 * Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            DeployTrap((TrapClass)Random.Range(0,3));
            timer = timerSet;
        }
    }

    void DeployTrap(TrapClass trap)
    {
        float randHor = Random.Range(Camera.main.transform.position.x - (camWidth/2.2f), Camera.main.transform.position.x + (camWidth/2.2f));
        Vector3 trapPos = new Vector3(randHor, Camera.main.transform.position.y + camHeight, 0);
        switch(trap)
        {
            case TrapClass.fly:
                Instantiate(flyPrefab, trapPos, Quaternion.identity);
                break;

            case TrapClass.star:
                Instantiate(killStarPrefab, trapPos, Quaternion.identity);
                break;

            case TrapClass.gun:
                GeneratePlatform();
                if (Random.Range(0, 2) > 1)
                    trapPos.x = Camera.main.transform.position.x - (camWidth / 2.2f);
                else
                    trapPos.x = Camera.main.transform.position.x + (camWidth / 2.2f);

                GameObject gun = Instantiate(gunPrefab, trapPos, Quaternion.identity);

                if (gun.transform.position.x > Camera.main.transform.position.x)
                    gun.transform.Rotate(0, 180, 0);
                break;
        }
    }

    void GeneratePlatform()
    {
        float randHor = Random.Range(Camera.main.transform.position.x - (camWidth / 2.2f), Camera.main.transform.position.x + (camWidth / 2.2f));
        int index = Random.Range(0, platformPrefabs.Length);
        Vector3 platformPosition = new Vector3(randHor, Camera.main.transform.position.y + camHeight, 0);
        Instantiate(platformPrefabs[index], platformPosition, Quaternion.identity);
    }
}
