/*
 * made By: Ava Fritts
 * Created: Feb 15th 2022 
 * Last edited: Feb 16th 2022
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40; //# of clouds to make
    public GameObject cloudPrefab; //cloud prefab
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; //min scale for clouds
    public float cloudScaleMax = 3; //max scale for clouds
    public float cloudSpeedMult = .5f; //speed of clouds

    private GameObject[] cloudInstances;

    // Start is called before the first frame update
    void Awake()
    {
        cloudInstances = new GameObject[numClouds]; //make array for clouds

        GameObject anchor = GameObject.Find("CloudAnchor"); //find the parent

        GameObject cloud; //making clouds
        for(int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab); //making instances

            //cloud position
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            //scale cloud.
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU); //put smaller clouds lower

            cPos.z = 100 - 90 * scaleU;//Putting smaller clouds farther away

            //applying transformations to the child
            cloud.transform.position = cPos;
            //print(cPos); Proved to me that the transformations were working.
            cloud.transform.localScale = Vector3.one * scaleVal;

            cloud.transform.SetParent(anchor.transform); //setting parent...

            cloudInstances[i] = cloud; //add to cloudInstances
        }//end for
    }//end awake

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject cloud in cloudInstances)
        {
            //getting cloud scale and position
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;

            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult; //larger ones move faster

            if (cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x; //resets position if OOB
            } //end if

            cloud.transform.position = cPos;
        }//end for loop
    }//end update
}
