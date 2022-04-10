using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MouseLook : MonoBehaviour
{
    public float mouseSens;

    private float m_xRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        m_xRot -= mouseY;
        m_xRot = Mathf.Clamp(m_xRot, -90, 90);

        transform.localRotation = Quaternion.Euler(m_xRot, 0, 0);
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
