using UnityEngine;

public class RotateOn : MonoBehaviour
{
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;

    public float rotationSpeedY = 50f; // speed in degrees per second
    public float rotationSpeedX = 50f;
    public float rotationSpeedZ = 50f;


    public bool hover = false;        // enable hover effect
    public float hoverHeight = 0.5f;   // how high it moves up/down
    public float hoverSpeed = 2f;      // speed of the hover motion

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // rotate on X axis
        if (rotateX)
            transform.Rotate(rotationSpeedX * Time.deltaTime, 0, 0);
        // rotate on Z axis
        if (rotateZ)
            transform.Rotate(0, 0, rotationSpeedZ * Time.deltaTime);
        // if rotateY is true
        if (rotateY)
            transform.Rotate(0, rotationSpeedY * Time.deltaTime, 0);


        // hover effect
        if (hover)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
