using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterDelay : MonoBehaviour
{
    public float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        // Нельзя использовать вызов Destroy(this) - он уничтожит сам
        // объект сценария RemoveAfterDelay.
    }
}
