using UnityEngine;
using System.Collections.Generic;

public class BezierMover : MonoBehaviour
{
    public Transform p0;                 // 시작점(고정)
    public Transform p3;                 // 도착점(고정)

    [Header("Random Ranges")]
    public float p1Radius = 2f;          // P0 근처에서 뽑는 반경
    public float p2Radius = 2f;          // P3 근처에서 뽑는 반경
    public float p1Height = 3f;          // P1 Y축 추가 높이 (선택)
    public float p2Height = 3f;          // P2 Y축 추가 높이 (선택)

    // 결과 제어점
    [HideInInspector] public Vector3 p1;
    [HideInInspector] public Vector3 p2;
    
    
    List<Vector3> points = new List<Vector3>();
    float time = 0f;

    bool isShooting = false;
    void Start()
    {
    }

    public void StartShooting()
    {
        GenerateRandomControlPoints();
        points = new List<Vector3> { p0.position, p1, p2, p3.position };
        isShooting = true;
    }

    private void Update()
    {
        if (!isShooting) return;
        time += Time.deltaTime / 2f; // 2초 동안 이동
        transform.position = DeCasteljau(points, time);
    }

    void GenerateRandomControlPoints()
    {        
        Vector2 rand1 = Random.onUnitSphere * p1Radius;
        p1 = p0.position + new Vector3(rand1.x, 0f, rand1.y);
        p1.y += p1Height;                    // 살짝 위로 띄워 궤적 상승
             
        Vector2 rand2 = Random.onUnitSphere * p2Radius;
        p2 = p3.position + new Vector3(rand2.x, 0f, rand2.y);
        p2.y += p2Height;                    // 도착 직전 살짝 꺾이도록
    }

    Vector3 DeCasteljau(List<Vector3> p, float t)
    {
        // p는 길이 n+1 의 제어점 배열/리스트
        while (p.Count > 1)
        {
            int last = p.Count - 1;
            var next = new List<Vector3>(last);
            for (int i = 0; i < last; i++)
                next.Add(Vector3.Lerp(p[i], p[i + 1], t)); 
            p = next;                       
        }
        return p[0];   
    }

}