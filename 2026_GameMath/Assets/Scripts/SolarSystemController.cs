using UnityEngine;

public class SolarSystemController : MonoBehaviour
{
    public Transform sun;
    public Transform mercury;
    public Transform venus;
    public Transform earth;
    public Transform moon;
    public Transform mars;
    public Transform jupiter;

    public float mercuryRadius = 2f;
    public float venusRadius = 3.5f;
    public float earthRadius = 5f;
    public float moonRadius = 1f;
    public float marsRadius = 6.5f;
    public float jupiterRadius = 8.5f;

    public float mercurySpeed = 80f;
    public float venusSpeed = 60f;
    public float earthSpeed = 45f;
    public float moonSpeed = 180f;
    public float marsSpeed = 35f;
    public float jupiterSpeed = 20f;

    private float mercuryAngle;
    private float venusAngle;
    private float earthAngle;
    private float moonAngle;
    private float marsAngle;
    private float jupiterAngle;

    void Update()
    {
        mercuryAngle += mercurySpeed * Time.deltaTime;
        venusAngle += venusSpeed * Time.deltaTime;
        earthAngle += earthSpeed * Time.deltaTime;
        moonAngle += moonSpeed * Time.deltaTime;
        marsAngle += marsSpeed * Time.deltaTime;
        jupiterAngle += jupiterSpeed * Time.deltaTime;

        mercury.position = GetOrbitPosition(sun.position, mercuryRadius, mercuryAngle);
        venus.position = GetOrbitPosition(sun.position, venusRadius, venusAngle);
        earth.position = GetOrbitPosition(sun.position, earthRadius, earthAngle);
        mars.position = GetOrbitPosition(sun.position, marsRadius, marsAngle);
        jupiter.position = GetOrbitPosition(sun.position, jupiterRadius, jupiterAngle);

        moon.position = GetOrbitPosition(earth.position, moonRadius, moonAngle);
    }

    Vector3 GetOrbitPosition(Vector3 center, float radius, float angleInDegree)
    {
        float rad = angleInDegree * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad) * radius;
        float z = Mathf.Sin(rad) * radius;

        return new Vector3(center.x + x, center.y, center.z + z);
    }

}