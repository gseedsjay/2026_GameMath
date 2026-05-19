using UnityEngine;
public class PhysicsTest : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
    }

    void Update()
    {
        speed = rb.linearVelocity.magnitude;
    }
}


