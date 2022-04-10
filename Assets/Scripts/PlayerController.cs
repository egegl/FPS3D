using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public GameObject playerCam;
    public float gravity = -9.8f;
    public float jumpForce;
    public float speed;

    private CinemachineVirtualCamera m_cam;
    private Animator m_anim;
    private CharacterController m_Controller;
    private bool m_CanJump = true;
    private float m_YDir;
    
    void Start()
    {
        m_cam = playerCam.GetComponent<CinemachineVirtualCamera>();
        m_anim = GetComponent<Animator>();
        m_Controller = GetComponent<CharacterController>();

        if (isLocalPlayer)
        {
            playerCam.SetActive(true);
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 moveDir = transform.forward * z + transform.right * x;

            if (m_Controller.isGrounded)
            {
                if (Input.GetButton("Jump") && m_CanJump)
                {
                    m_CanJump = false;
                    m_YDir = jumpForce;
                    StartCoroutine(JumpCooldown());
                }
            }
            m_YDir += gravity * Time.deltaTime;
            moveDir.y = m_YDir;
            m_Controller.Move(moveDir * speed * Time.deltaTime);
            float m_playerVel = m_Controller.velocity.magnitude;
            m_anim.SetFloat("Speed", m_playerVel);
            
            if (m_playerVel > 5)
            {
                m_cam.m_Lens.FieldOfView = Mathf.Lerp(m_cam.m_Lens.FieldOfView, 90, Time.deltaTime * 5);
            }
            else
            {
                m_cam.m_Lens.FieldOfView = Mathf.Lerp(m_cam.m_Lens.FieldOfView, 75, Time.deltaTime * 5);
            }
        }
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        m_CanJump = true;
    }
}
