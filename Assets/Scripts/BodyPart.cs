using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour
{
    bool detached = false;
    
    public void Detach()
    {
        detached = true;
        this.tag = "Untagged";
        transform.SetParent(null, true);
    }

    void Update()
    {
        if (detached == false)
            return;

        var rigidbody = GetComponent<Rigidbody2D>();

// about rigidbody sleeping http://www.cis.sojo-u.ac.jp/~izumi/Unity_Documentation_jp/Documentation/Components/RigidbodySleeping.html
        if (rigidbody.IsSleeping())
        {
            //Returns all components of Type type in the GameObject or any of its children.
            foreach (Joint2D joint in GetComponentsInChildren<Joint2D>())
            {
                Destroy(joint);
            }

            foreach (Rigidbody2D body in GetComponentsInChildren<Rigidbody2D>())
            {
                Destroy(body);
            }

            foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
            {
                Destroy(collider);
            }

            Destroy(this);
        }
    }
}
