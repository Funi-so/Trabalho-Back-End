
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 15;
    public float maxSpeed;

    public float resistence = 1;
    public Rigidbody rb;
    Vector3 vel;

    public Animator animator;
    public GameObject model;
    public LayerMask groundMask;
    public bool grounded;
    [SerializeField] private UIInventory uiInventory;
    private Inventory inventory;

    void Awake()
    {
        
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        vel = GetInput();
        SpeedControl();
    }

    void FixedUpdate(){
        Move();
    }

    Vector3 GetInput(){
        Vector3 dir = new Vector3();
        dir.x = Input.GetAxisRaw("Horizontal") ;
        dir.y = Input.GetAxisRaw("Vertical") ;
        dir = dir.normalized; 

        return dir;
    }

    void Move(){

            rb.AddForce(vel * speed / resistence, ForceMode.Force);
            
            if(vel.magnitude > 0.1) animator.SetBool("running", true);
            else animator.SetBool("running", false);
            
            animator.SetBool("falling", false);
            animator.SetBool("jumping", false);

        //Rotação em Y
        if (vel.x < 0){
            model.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (vel.x > 0)
        model.transform.localScale = new Vector3(1, 1, 1);

    }

    void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > maxSpeed){
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
