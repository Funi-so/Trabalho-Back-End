using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isButton;
    public bool state;
    bool playerNear;
    public bool pressable = true;
    public Transform model;
    public Platform targetPlatform;
    
    void Start(){
        if(state){ On(); } else { Off(); }
    }
    void Update()
    {
        if (playerNear && pressable && Input.GetKeyDown(KeyCode.E)){
            if (!state) On();
            else Off();
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            playerNear = true;
        }
    }
    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            playerNear = false;
        }
    }
    void On(){
        targetPlatform.moving = true;
        state= true;
        model.localScale= Vector3.one;

        if(isButton){
            targetPlatform.keepMoving = true;
            StartCoroutine(DelayOff(0.5f));
            pressable = false;
        }

    }
    void Off(){
        targetPlatform.moving = false;
        state = false;
        model.localScale = new Vector3(1,1,-1);
    }

    IEnumerator DelayOff(float waitTime){
        yield return new WaitForSeconds(waitTime);
        Off();
        pressable = true;
    }
}
