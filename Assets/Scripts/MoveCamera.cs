
using Unity.Mathematics;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public float3 offset;
    public Vector3 axisMultiplier;

    public float speed;
    public float maxSpeed;
    public float damping;
    Vector3 velocity = new Vector3();
    Vector3 offsetTarget;
    void FixedUpdate()
    {
        offsetTarget = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
        
        velocity = offsetTarget - transform.position;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        Vector3 n1 = velocity * damping * damping * Time.deltaTime;
        
        velocity.x = n1.x * axisMultiplier.x;
        velocity.y = n1.y * axisMultiplier.y;
        velocity.z = n1.z * axisMultiplier.z;

        transform.Translate(velocity * speed * Time.deltaTime);
    }
}
