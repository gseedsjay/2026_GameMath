using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserEnemy : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 50f;
    public float detectionRange = 8f;
    public float dashSpeed = 15f;
    public float stopDistance = 1.2f;

    public bool isDashing = false;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (player == null) return;

        if (!isDashing) // 회전 모드
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            // [과제] 내적을 사용하여 '전방 시야 60도 이내' 판정 추가
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < detectionRange)
            {
                Debug.Log("발견! 돌진 모드로 전환");
                isDashing = true;
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stopDistance)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                rb.MovePosition(transform.position + dir * dashSpeed * Time.deltaTime);
            }
            else
            {               
                CheckParry();  // 공격 범위에 도달하면 패링 체크
                isDashing = false;
            }
        }
    }

    void CheckParry()
    {
        PlayerController pc = player.GetComponent<PlayerController>();

        // [과제] 외적을 사용하여 플레이어 기준 왼쪽/오른쪽 패링 판정 추가
        if (pc.isLeftParrying || pc.isRightParrying)
        {
            Debug.Log("패링 성공! 적 제거");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("패링 실패! 잡혔습니다.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}