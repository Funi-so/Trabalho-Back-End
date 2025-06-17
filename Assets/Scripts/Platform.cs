using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Waypoint target;
    public Vector3 targetPosition;
    public float speed;
    public bool moveUntilWaypoint;
    public bool moving;
    public bool keepMoving;
    
    
    public bool state;
    bool playerNear;
    public Vector3 dir;
    Rigidbody rb;
    
    void Start(){
         targetPosition = target.transform.position;
    }
    void FixedUpdate()
    {
        if(moving || keepMoving){
            if (targetPosition == null) { return; }
            dir = targetPosition - transform.position;

            if (dir.magnitude < 0.1f)
            {
                keepMoving = false;
                targetPosition = target.nextPosition;
                target = target.next;
            }
            dir = dir.normalized;

            //rb.AddForce(dir * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            transform.Translate(dir * speed * Time.fixedDeltaTime);
        }
        if (moving && moveUntilWaypoint){
            keepMoving = true;
        }
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E)){
            if (!state) {
                moving= true;
                state = true;
                }
            else {
                moving = false;
                state = false;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            playerNear = true;
        }
        if(other.CompareTag("Waypoint")){
            Debug.Log("waypoint in plat");
            target = other.GetComponent<Waypoint>();
        }
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            playerNear = false;
        }
    }
}
