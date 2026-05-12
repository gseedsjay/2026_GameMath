using UnityEngine;
using UnityEngine.InputSystem;


public class BeizerShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;
        Shooting();
    }

    void Shooting()
    {

        for (int i = 0; i < 10; i++)
        {
            BezierMover bezier = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<BezierMover>();
            bezier.p0 = this.transform;
            bezier.p3 = target;
            bezier.StartShooting();
        }
    }
}
