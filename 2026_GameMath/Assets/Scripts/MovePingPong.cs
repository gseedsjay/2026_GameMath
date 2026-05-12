using UnityEngine;
using UnityEngine.InputSystem;

public class MovePingPong : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float t = 0f;

    private void Update()
    {
        t = Mathf.PingPong(Time.time / duration, 1f);
        transform.position = Vector3.Lerp(startPos.position, endPos.position, t);
    }

    public void OnRightClick(InputValue value)
    {
        if (!value.isPressed) return;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                // 타게팅
            }

        }
        else
        {
            // 초기화
        }
    }

}
