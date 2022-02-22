/*
 * Made By: Ava Fritts
 * Created: Feb 18th 2022
 * 
 * Last edited: Feb 19th 2022
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool goalMet = false;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile") //checking to see if projectile
        {
            Goal.goalMet = true;
            print("Goal Hit");

            Material mat = GetComponent<Renderer>().material; //changing color
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }

}
