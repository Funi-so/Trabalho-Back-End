
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 15;
    public float maxSpeed;
    public float2 axisMultiplier = new float2(1f,1f);
    public float jumpStrenght = 15;

    public float airResistence = 1;
    public float gravityMultiplier = 2;
    public Rigidbody rb;
    Vector3 vel;

    public Animator animator;
    public GameObject model;
    public LayerMask groundMask;
    bool jumped;
    public bool grounded;
    bool inPlatform;
    Platform plat;

    RaycastHit hit;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.2f, groundMask);
        if(grounded && hit.transform.CompareTag("Platform")){
            inPlatform = true;
            plat = hit.transform.GetComponent<Platform>();
        } 
        else {
            inPlatform = false;
        }

        vel = GetInput();
        SpeedControl();
    }

    void FixedUpdate(){
        Move();
    }

    Vector3 GetInput(){
        Vector3 dir = new Vector3();
        dir.z = Input.GetAxisRaw("Horizontal") * axisMultiplier.y;
        dir.x = -Input.GetAxisRaw("Vertical")* axisMultiplier.x;
        dir = dir.normalized; 

        if(Input.GetKeyDown(KeyCode.Space) && grounded){ jumped = true;}
        return dir;
    }

    void Move(){

        //Aumento de Gravidade
        if(rb.velocity.y < 0.2) {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
            rb.AddForce(0, -10 * gravityMultiplier , 0, ForceMode.Force);
        } else animator.SetBool("falling", false);

        //Movimentação normal
        if(grounded){
            if(inPlatform && (plat.moving || plat.keepMoving)){
                Vector3 bro = new Vector3(0, 0, plat.dir.z);
                transform.Translate(bro * plat.speed * Time.fixedDeltaTime);
            } 
            rb.AddForce(vel * speed, ForceMode.Force);
            
            if(vel.magnitude > 0.1) animator.SetBool("running", true);
            else animator.SetBool("running", false);
            
            animator.SetBool("falling", false);
            animator.SetBool("jumping", false);
        } 
        else{
            rb.AddForce(vel * speed / airResistence, ForceMode.Force);
            animator.SetBool("running", false);
        } 

        //Rotação em Y
        if (vel.z < 0){
            model.transform.localScale = new Vector3(1, 1, -1);
        }
        else if (vel.z > 0)
        model.transform.localScale = new Vector3(1, 1, 1);

        //Pulo
        if(jumped){
            jumped = false;
            animator.SetBool("jumping", true);
            rb.AddForce(0, jumpStrenght , 0, ForceMode.Impulse);
        }
    }

    void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > maxSpeed){
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
