using System;
using UnityEngine;


public class EnemyFind : MonoBehaviour
{
    public Transform player;
    float viewAngle = 60f;

    private void Update()
    {
        Vector3 toPlayer = player.position - transform.position;
        Vector3 forward = transform.forward;

        forward.Normalize();
        toPlayer.Normalize();

        float dot = DotProduct(forward, toPlayer);           
        float angle = Mathf.Asin(dot) * Mathf.Deg2Rad;          // A

        if (GetDistance(toPlayer) > 4)                          
        {
            if (angle < viewAngle)                              // B
            {
                transform.localScale = Vector3.one * 2f;
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }

    float DotProduct(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(a.x * b.x + a.y * b.y + a.z * b.z);               // C
    }

    float GetDistance(Vector3 vec)
    {
        return Mathf.Pow(vec.x, 2) + Mathf.Pow(vec.y, 2) + Mathf.Pow(vec.z, 2);     // D
    }
}
