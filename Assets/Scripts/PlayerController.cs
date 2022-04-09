using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpHeight;
    public float speed;
    public float mouseSens;
    public Transform playerCam;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public CharacterController characterController;

    private float m_xRot = 0f;
    private float ground = 0.1f;
    private float gravity = -9.8f;
    private bool m_Grounded;
    private Vector3 m_velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        m_Grounded = Physics.CheckSphere(groundCheck.position, ground, groundLayer);
        if (m_Grounded && m_velocity.y < 0)
        {
            m_velocity.y = -2;
        }

        if (Input.GetButtonDown("Jump") && m_Grounded)
        {
            m_velocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        m_xRot -= mouseY;
        m_xRot = Mathf.Clamp(m_xRot, -90f, 90f);
        playerCam.localRotation = Quaternion.Euler(m_xRot, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        Vector3 move = transform.forward * z + transform.right * x;
        characterController.Move(move * speed * Time.deltaTime);
        m_velocity.y += gravity * Time.deltaTime;
        characterController.Move(m_velocity * Time.deltaTime);
    }
}
