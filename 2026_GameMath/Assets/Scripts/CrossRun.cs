using UnityEngine;

public class CurvedWallRun : MonoBehaviour
{
    public float wallRunSpeed = 10f;
    public float wallCheckDistance = 1.5f;
    public LayerMask wallLayer;

    private Rigidbody rb;

    void Start() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        RaycastHit hit;
        // 1. ���������� ���̸� ���� ��(���)�� ������ ������
        if (Physics.Raycast(transform.position, transform.right, out hit, wallCheckDistance, wallLayer))
        {
            // �� Ÿ�� ���� ����
            DoWallRun(hit.normal);
        }
        else
        {
            // ������ �������� �߷� ���� �� ó��
            rb.useGravity = true;
        }
    }

    void DoWallRun(Vector3 wallNormal)
    {
        rb.useGravity = false; // �� Ż ���� �߶� ����

        // 2. [�ٽ� ���� ����]
        // wallNormal: ���� ���� �о�� ����
        // Vector3.up: ������ �ϴ� ����
        // �� ���� �����ϸ�? ������ ���� �帣�� "�Ϻ��� ����" ������ ����
        Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up);

        // 3. ���� ���� �ڷ� ���ٸ� ������ ���� (�� �չ���� ���� ��� ��)
        if (Vector3.Dot(wallForward, transform.forward) < 0)
        {
            wallForward = -wallForward;
        }

        // 4. �̵� �� ���� �ٱ�
        // �ӵ��� �����ϰ�, ���ÿ� �� ������ ��¦ ���� �־� �������� �ʰ� ��
        rb.linearVelocity = wallForward * wallRunSpeed + (-wallNormal * 2f);

        // �ð�ȭ (������: �� ���, �Ķ���: �츮�� �� ���� ����)
        Debug.DrawRay(transform.position, wallNormal * 2f, Color.red);
        Debug.DrawRay(transform.position, wallForward * 2f, Color.blue);
    }
}