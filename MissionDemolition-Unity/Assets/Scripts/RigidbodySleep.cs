/*
 * made By: Ava Fritts
 * Created: Feb 16th 2022
 * 
 * Last edited: Feb 16th 2022
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //adds rigidbody if not there.
public class RigidbodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.Sleep();
    }

}
