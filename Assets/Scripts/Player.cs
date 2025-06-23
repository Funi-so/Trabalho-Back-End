
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 15;
    public float maxSpeed;

    public float resistence = 1;
    public Rigidbody2D rb;
    Vector3 vel;

    public Animator animator;
    public GameObject model;
    public LayerMask groundMask;
    public bool grounded;
    [SerializeField] private UIInventory uiInventory;
    public MainHUD hud;
    public Inventory inventory;

    void Awake()
    {
        inventory = new Inventory();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        uiInventory.SetInventory(inventory);
    }
    void Update()
    {
        vel = GetInput();
        SpeedControl();
    }

    void FixedUpdate()
    {
        Move();
    }

    Vector3 GetInput()
    {
        Vector3 dir = new Vector3();
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        dir = dir.normalized;

        return dir;
    }

    void Move()
    {
        if (!DialogController.controller.isTalking || hud.storeEnabled)
        {
            rb.AddForce(vel * speed / resistence, ForceMode2D.Force);

            if (vel.magnitude > 0.1) animator.SetBool("running", true);
            else animator.SetBool("running", false);

            animator.SetBool("falling", false);
            animator.SetBool("jumping", false);

            //Rotação em Y
            if (vel.x < 0)
            {
                model.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (vel.x > 0)
                model.transform.localScale = new Vector3(1, 1, 1);
        }
        else { rb.velocity = Vector2.zero; animator.SetBool("running", false); }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector2(rb.velocity.x, rb.velocity.y);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector2 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
}
