/*
 * made By: Ava Fritts
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    /***VARIABLES***/
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public GameObject rope;
    public Vector3 ropePos;
    public Vector3 launchPos; //launch position of projectile
    public GameObject projectile; //projectile instance
    public bool aimingMode; //is player aiming
    public Rigidbody projectileRB; //rigidbody of projectile

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint"); //find child object

        launchPoint = launchPointTrans.gameObject; //the game object of child object?
        launchPoint.SetActive(false); //disable game object
        launchPos = launchPointTrans.position;

        Transform ropeTrans = transform.Find("Rope");
        rope = ropeTrans.gameObject; //the game object of child object?
        rope.SetActive(false); //disable game object
        ropePos = ropeTrans.position;
    }//end Wake

    private void Update()
    {
        if (!aimingMode) return;

        //get current mouse position in 2d screen coords
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos; //pixel change amount between 3D and launchPos

        //limit mouseDelta to slingshoot collider radius
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize(); //sets vector to same direction with length of 1
            mouseDelta *= maxMagnitude;
        }//end if

        //move projectile to new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMultiplier;
            projectile = null; //remove the projectile from the instance/script
        }

    }//end update

    private void OnMouseEnter()
    {
        launchPoint.SetActive(true); //enable game object
        rope.SetActive(true);
        print("Slingshot Entered");
    }//End OnMouseEnter

    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
        rope.SetActive(false);
        print("Slingshot Left");
    }//end OnMouseExit

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.isKinematic = true;
    }//end OnMouseDown
}
