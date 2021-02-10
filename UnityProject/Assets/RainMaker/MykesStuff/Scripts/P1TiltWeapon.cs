using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1TiltWeapon : MonoBehaviour {

    public float MoveAmount = 1;
    public float MoveSpeed = 1;
    public GameObject Gun;
    public float MoveOnX;
    public float MoveOnY;
    public Vector3 defaultPos;
    public Vector3 NewGunPos;
    public bool ONOFF;


    // Use this for initialization
    void Start () {
        //ONOFF = true;
        defaultPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        MoveOnX = Input.GetAxis("Player2LookHorizontal") * Time.deltaTime * MoveAmount;
        MoveOnY = Input.GetAxis("Player2LookVertical") * Time.deltaTime * MoveAmount;
        if (ONOFF == true)
        {
            
            NewGunPos = new Vector3(defaultPos.x + MoveOnX, defaultPos.y + MoveOnY, defaultPos.z);
            Gun.transform.localPosition = Vector3.Lerp(Gun.transform.localPosition, NewGunPos, MoveSpeed * Time.deltaTime);
            
            
        }
        else
        {
            ONOFF = false;
            Gun.transform.localPosition = Vector3.Lerp(Gun.transform.localPosition, defaultPos, MoveSpeed * Time.deltaTime);
        }
   
    }
}
