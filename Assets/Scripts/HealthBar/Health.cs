using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //I Made this script using Brackeys How to make a health bar in unity//
    public Slider HealthBar;

    public void SetMaxHealth(int health)
    {
        HealthBar.maxValue = health;
        HealthBar.value = health;
    }

    public void SetHealth(int health)
    {
       HealthBar.value = health;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
