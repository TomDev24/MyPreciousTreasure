using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    //где может находиться камера
    public float topLimit = 10f;
    public float bottomLimit = -10f;
    public float followSpeed = 0.5f;

    private Vector3 camVelocity = new Vector3(0, 0, 0);
    public Transform gameOverTarget;

    private void LateUpdate()
    {
        if (target)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.Lerp(newPosition.y, target.position.y, followSpeed);

            newPosition.y = Mathf.Max(newPosition.y, bottomLimit);
            newPosition.y = Mathf.Min(newPosition.y, topLimit);

            transform.position = newPosition; //new Vector3(newPosition.x, newPosition.y, -10);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 topPoint = new Vector3(this.transform.position.x, topLimit, this.transform.position.z);
        Vector3 bottomPoint = new Vector3(this.transform.position.x, bottomLimit, this.transform.position.z);
        Gizmos.DrawLine(topPoint, bottomPoint);
    }
    
    public IEnumerator FollowEnd()
    {
        gameOverTarget.parent = null;
        target = null;
        transform.position = Vector3.SmoothDamp(transform.position, gameOverTarget.position, ref camVelocity, 0.6f);
        yield return null;
    }
}
