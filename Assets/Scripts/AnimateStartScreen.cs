using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateStartScreen : MonoBehaviour
{
    public bool animate { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animate == true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(45, 55, 1), 0.08f);
            StartCoroutine(Deactivate());
        }
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
