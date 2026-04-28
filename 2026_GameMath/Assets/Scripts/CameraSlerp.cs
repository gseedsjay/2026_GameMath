using UnityEngine;

public class CameraSlerp : MonoBehaviour
{
    [HideInInspector]
    public Transform target;

    float speed = 2f;

    Quaternion originLook;
    
    void Start()
    {
        originLook = transform.rotation;
    }

    public void SetOriginLook ()
    {
        target = null;
        transform.rotation = originLook;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
        float t = 1f - Mathf.Exp(-speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, t);
    }
}
