using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour
{
    public Transform cameraFollowTarget;
    public Rigidbody2D ropeBody;

    public float delayBeforeRemoving = 3.0f;
    public AudioClip dudeDiedSound;
    public AudioSource musicSrc;

    bool dead = false;

    public enum DamageType
    {
        Slicing,
        Burning
    }

    public void DestroyDude() //DamageType type = DamageType.Burning)
    {
        var audio = GetComponent<AudioSource>();
        if (audio && !dead)
        {
            audio.PlayOneShot(this.dudeDiedSound);

        }

        StartCoroutine(AudioFadeOut.FadeOut(musicSrc, 0.1f));

        dead = true;

      StartCoroutine(Camera.main.GetComponent<CameraFollow>().FollowEnd());


        foreach (BodyPart part in GetComponentsInChildren<BodyPart>())
        {
            bool shouldDetach = Random.Range(0, 2) == 0;

            if (shouldDetach)
            {
                // Обеспечить удаление твердого тела и коллайдера
                // из этого объекта после достижения дна
                part.Detach();
            }

            var allJoints = part.GetComponentsInChildren<Joint2D>();
            foreach (Joint2D joint in allJoints)
            {
                Destroy(joint);
            }
        }
        // Добавить компонент RemoveAfterDelay в объект this
        StartCoroutine(DelayPause());
        var remove = gameObject.AddComponent<RemoveAfterDelay>();
        remove.delay = delayBeforeRemoving;
    }

    private IEnumerator DelayPause()
    {
        yield return new WaitForSeconds(2.5f);
        GameManager.instance.isDudeDead = true;
        GameManager.instance.SetPaused(true);
    }
}
