using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity;
    public float xRot = 0f;
    public float minY = -90f;
    public float maxY = 90f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    //Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        MouseLook();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, minY, maxY);

        transform.rotation = Quaternion.Euler(xRot, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        player.Rotate(Vector3.up * mouseX);
    }
}
