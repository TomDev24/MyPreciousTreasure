using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(Collider2D))]
[AddComponentMenu("TrapScript")]
public class SignalOnTouch : MonoBehaviour
{
    public UnityEvent onTouch;
    public bool playAudioOnTouch = false;
    //Event метод не работает с префабами ( он их не запоминает)ю

    void OnTriggerEnter2D(Collider2D collider)
    {
        SendSignal(collider.gameObject);
        if (collider.gameObject.tag == "Player")
            collider.transform.parent.GetComponent<Dude>().DestroyDude();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SendSignal(collision.gameObject);
        if (collision.gameObject.tag == "Player")
            collision.transform.parent.GetComponent<Dude>().DestroyDude();
    }

    private void SendSignal(GameObject objectThatHit)
    {
        if (objectThatHit.CompareTag("Player"))
        {
            if (playAudioOnTouch)
            {
                var audio = GetComponent<AudioSource>();
                // Если имеется аудиокомпонент
                // и родитель этого компонента активен,
                // воспроизвести звук
                if (audio && audio.gameObject.activeInHierarchy)
                    audio.Play();
            }
            onTouch.Invoke();
        }
    }
}
