using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGoTo : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;

    private bool inStartPosition = true;
    // Start is called before the first frame update

    // Update is called once per frame
    public void changePos(bool inStart)
    {
        inStartPosition = inStart;
    }

    private void Update()
    {
        if (inStartPosition)
            transform.position = Vector3.Lerp(transform.position, pos2.position, 0.4f);
        else
            transform.position = Vector3.Lerp(transform.position, pos1.position, 0.4f);
    }
}
