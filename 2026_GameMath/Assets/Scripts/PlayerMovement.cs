using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 5f; public float sprintMultiplier = 2f; // 전력질주 시 속도 배율
    private Vector2 mouseScreenPosition; 
    private Vector3 targetPosition;      
    private bool isMoving = false;
    private bool isSprinting = false; 
    
    [Header("Dash Settings")]
    public float dashSpeed = 20f;      // 대시 속도
    public float dashDuration = 0.2f;  // 대시 지속 시간 (초)
    private float dashTimer = 0f;      // 남은 대시 시간 측정용
    private bool isDashing = false;    // 대시 중인가?
    private Vector3 lastDirection;     // 마지막 이동 방향 기억

    public void OnDash(InputValue value)
    {
        // 버튼을 눌렀고, 이미 대시 중이 아닐 때만 발동
        if (value.isPressed && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration; // 타이머 장전

            // 🔥 중요: 대시가 시작되면 기존의 '클릭 이동'은 해제합니다.
            isMoving = false;

            // 만약 캐릭터가 한 번도 움직이지 않았다면 정면으로 대시
            if (lastDirection == Vector3.zero) lastDirection = transform.forward;
        }
    }

    public void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed; // 버튼을 누르고 있으면 true, 떼면 false
    }

    public void OnPoint(InputValue value)
    {
        mouseScreenPosition = value.Get<Vector2>(); // 마우스 위치 업데이트 
    }
    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);            
            RaycastHit[] hits = Physics.RaycastAll(ray);    // 레이저 경로에 있는 모든 물체를 탐색

            foreach (RaycastHit hit in hits) // 모든 물체에 한해 반복 
            {              
                if (hit.collider.gameObject != gameObject)   // 부딪힌 물체가 나 자신이 아닐 때만
                {
                   
                    targetPosition = hit.point;  // Plane에 부딪힌 지점을 타겟
                    targetPosition.y = transform.position.y;
                    isMoving = true;
                   
                    break;  // 탐색 했으니 foreach 반복 중단
                }
            }
        }
    }

    void Update()
    {
        if (isDashing)
        {
            // 마지막으로 가던 방향(lastDirection)으로 강제 이동
            transform.Translate(lastDirection * dashSpeed * Time.deltaTime);

            // 타이머 감소
            dashTimer -= Time.deltaTime;

            // 시간이 다 되면 대시 종료
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }
        else if (isMoving)
        {
            Vector3 offset = targetPosition - transform.position;
            float sqrDist = offset.x * offset.x + offset.y * offset.y + offset.z * offset.z;
            float dist = Mathf.Sqrt(sqrDist);
            float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

            transform.Translate(offset / dist * currentSpeed * Time.deltaTime);

            if (dist < 0.1f) // 목적지에 거의 도착했다면 이동 중지
            {
                isMoving = false;
            }
            else
            {
                lastDirection = offset.normalized; // 이동 방향 업데이트
            }
        }
    }
}
