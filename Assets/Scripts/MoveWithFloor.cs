﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour{

    CharacterController player;

    Vector3 groundPosition;
    Vector3 lastGroundPosition;
    string groundName;
    string lastGroundName;

    Quaternion actualRot;
    Quaternion lastRot;

    public Vector3 originOffset;
    public float factorDivision = 4.2f;

    void Start(){
        
        player = this.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update(){
        if(player.isGrounded){
            RaycastHit hit;

            if(Physics.SphereCast(transform.position + originOffset, player.radius/factorDivision, -transform.up, out hit)){

                GameObject groundedIn = hit.collider.gameObject;
                groundName = groundedIn.name;
                groundPosition = groundedIn.transform.position;

                actualRot = groundedIn.transform.rotation;

                if(groundPosition != lastGroundPosition && groundName == lastGroundName){
                    this.transform.position += groundPosition - lastGroundPosition;
                } 

                if (actualRot != lastRot && groundName  == lastGroundName){
                    var newRot = this.transform.rotation * (actualRot.eulerAngles - lastRot.eulerAngles);
                    this.transform.RotateAround(groundedIn.transform.position, Vector3.up, newRot.y);
                }


                lastGroundName = groundName;
                lastGroundPosition = groundPosition;
                lastRot = actualRot;
            }


        }
        else if(!player.isGrounded){

            lastGroundName = null;
            lastGroundPosition = Vector3.zero;
            lastRot = Quaternion.Euler(0,0,0);
        }
    }
    private void OnDrawGizmos(){
        player = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position + originOffset, player.radius /factorDivision);
    }
}
