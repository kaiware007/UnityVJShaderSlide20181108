using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public float rotSpeed = 100;

    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(rotSpeed * Time.deltaTime, axis);
    }
}