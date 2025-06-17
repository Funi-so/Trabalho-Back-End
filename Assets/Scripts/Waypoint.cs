using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint next;
    public Vector3 nextPosition;

    void Start()
    {
        nextPosition = next.transform.position;
    }
}
