using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    private bool firstColor = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Update()
    {
        if (firstColor)
            GetComponent<Image>().color = Color.white;
        else
            GetComponent<Image>().color = Color.black;
    }
    public void ColorChange(bool changeColor)
    {
        firstColor = changeColor;
    }
}
