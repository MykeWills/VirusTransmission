using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{

    public Transform center;
    public float DistanceX = 0.0f;
    public float DistanceY = 0.0f;
    public float DistanceZ = 0.0f;
    private Vector3 distance;
    public float degreesPerSecond = -65.0f;


    public bool OrbitX;
    public bool OrbitY;
    public bool OrbitZ;
    

    //private Vector3 v;

    void Start()
    {

        distance = transform.position - center.position;
        distance.x = DistanceX;
        distance.y = DistanceY;
        distance.z = DistanceZ;
    }

    void Update()
    {
        if (OrbitX)
        {
            OrbitY = false;
            OrbitZ = false;
            distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.right) * distance;
        }
       
        if (OrbitY)
        {
            distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * distance;
            OrbitX = false;
            OrbitZ = false;
        }
        if (OrbitZ)
        {
            distance = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * distance;
            OrbitX = false;
            OrbitY = false;
        }
        transform.position = center.position + distance;
    }
}