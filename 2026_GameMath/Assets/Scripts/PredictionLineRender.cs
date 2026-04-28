using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PredictionLineRender : MonoBehaviour
{
    Transform startPos;   // A
    Transform endPos;     // B
    public CameraSlerp camSlerp;
    Vector3 originPos;

    [Range(1f, 5f)] public float extend = 1.5f;

    private LineRenderer lr;

    void Awake()
    {
        originPos = transform.position;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
        lr.widthMultiplier = 0.05f;        
        lr.material = new Material(Shader.Find("Unlit/Color"))
        {
            color = Color.red
        };
    }

    void Update()
    {
        if (!startPos || !endPos) return;

        Vector3 a = startPos.position;
        Vector3 b = endPos.position;

        Vector3 pred = Vector3.LerpUnclamped(a, b, extend);
        lr.positionCount = 2;
        lr.SetPosition(0, a);
        lr.SetPosition(1, pred);
    }

    public void OnRightClick(InputValue value)
    {
        if(!value.isPressed) return;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                endPos = hit.collider.transform;
                camSlerp.target = endPos; 
                startPos = transform;
            }

        }
        else
        {
            SetOrigin();
            camSlerp.SetOriginLook();
        }
    }

    void SetOrigin()
    {
        startPos = null;
        endPos = null;
        transform.position = originPos;
        lr.positionCount = 0;        
    }


}