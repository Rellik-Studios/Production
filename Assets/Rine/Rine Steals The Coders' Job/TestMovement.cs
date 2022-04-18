using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float hspeed = 2.0f;
    public float vspeed = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Vector3 MoveTransform;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

        yaw += hspeed * Input.GetAxis("Mouse X");
        pitch -= vspeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        MoveTransform = new Vector3 (transform.forward.x, 0, transform.forward.z);

        if (Input.GetKey(KeyCode.W))
        {
            MoveTransform = new Vector3 (transform.forward.x, 0, transform.forward.z);
            transform.position += MoveTransform * Time.deltaTime * MovementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveTransform = new Vector3 (-transform.forward.x, 0, -transform.forward.z);
            transform.position += MoveTransform * Time.deltaTime * MovementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveTransform = new Vector3 (transform.right.x, 0, transform.right.z);
            transform.position += MoveTransform * Time.deltaTime * MovementSpeed;
        }
         if (Input.GetKey(KeyCode.A))
        {
            MoveTransform = new Vector3 (-transform.right.x, 0, -transform.right.z);
            transform.position += MoveTransform * Time.deltaTime * MovementSpeed;
        }
    
    }
}
