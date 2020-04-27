using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//I Made this script using Brackeys How to make a health bar in unity//
public class BarMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public  Slider VirusBar;

    public void SetVirusBarMin(int VirusBarPos)
    {
        VirusBar.minValue = VirusBarPos;
        VirusBar.value = VirusBarPos;
    }

    public void SetVirusBar(int VirusBarPos)
    {
        VirusBar.value = VirusBarPos;
    }





    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
