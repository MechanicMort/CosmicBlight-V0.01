using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//I Made this script using Brackeys How to make a health bar in unity//
public class VirusBarAbilityUse : MonoBehaviour
{//Virus Part
    public int MinBar= 0;

    public int VirusBarFill;
    public BarMovement VirusBar;

    public int Ability;

    private float Timer;
    const float TickSpeed = 0.5f;
 
    float Delay;
    





    //HealthPart
    int maxHealth = 100;
    public int Playerhealth;
    public Health HealthBar;




    void Start()
    {//Virus Part
        VirusBarFill = MinBar;
        VirusBar.SetVirusBarMin(MinBar);
        Ability = 0;



        //HealthPart
        Playerhealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);










    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Ability == 0)
            {

                

                Ability = 1;
            }
            else if (Ability == 1)
            {
               
                Ability = 0;
            }
        }



        if (Time.time > Delay)
        {
            if (Ability == 0)
            {
                Delay = Time.time + TickSpeed;

                if (VirusBarFill < 0)
                { VirusBarFill = 0; }
                else
                {
                    VirusBarFill -= 1;
                }
                VirusBar.SetVirusBar(VirusBarFill);
            }
            if (Ability == 1)
            {
                Delay = Time.time + TickSpeed;
                if (VirusBarFill > 50)
                {

                    VirusBarFill = 50;
                    //HealthPart
                    BarTick(5);
                }
                else
                {
                    VirusBarFill += 2;
                }
                VirusBar.SetVirusBar(VirusBarFill);

            }
        }
    }
    //HealthPart
   void BarTick (int tick)
    {
        Playerhealth -= tick;
        HealthBar.SetHealth(Playerhealth);
        if (Playerhealth == 0)
        {
        //////
        ///////
        ///////
       //////
        ///
        //Load DeathScreen
        ///
        ////
        ////
        ////
        ////
        }
        else { }

    }

}
