using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]

public class sFPSController : MonoBehaviour
{
    [SerializeField] private bool m_IsWalking = false;          
    [SerializeField] private float m_JumpSpeed = 10f;           
    [SerializeField] private float m_WalkSpeed = 5f;            
    [SerializeField] private float m_RunSpeed = 10f;                                                   
    [SerializeField] private float m_StickToGroundForce = 9.81f;
    [SerializeField] private float m_GravityMultiplier = 2f;
    [SerializeField] private sFPSMouseLook m_FPSMouseLook;
    private bool m_PreviouslyGrounded;          
    private Vector3 m_MoveDir = Vector3.zero;   
    private Vector2 m_Input;                    
    private bool m_Jump;                        
    private bool m_Jumping;                                                   
    private Camera m_Camera;
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;   

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();    
        m_Camera = Camera.main;                                                                                                                                                                                                    
        m_Jumping = false; 
        m_FPSMouseLook.InitMouseLook(transform, m_Camera.transform);
    }

    private void FixedUpdate()
    {
        float speed;
        GetInput(out speed);
        Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo, m_CharacterController.height / 2f, ~0, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.z = desiredMove.z * speed;

        if (m_CharacterController.isGrounded)
        {
            m_MoveDir.y = -m_StickToGroundForce;

            if (m_Jump)
            {
                m_MoveDir.y = m_JumpSpeed;
                m_Jump = false;
                m_Jumping = true;
            }
        }
        else
        {
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
        }
        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
    }

    void Update()
    {
        m_FPSMouseLook.LookRotationMouse(transform, m_Camera.transform);
        if (!m_Jump)
        {
            m_Jump = Input.GetButtonDown("Jump");
        }
        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
        {
            m_MoveDir.y = 0f;
            m_Jumping = false;
        }
        if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
        {
            m_MoveDir.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        m_PreviouslyGrounded = m_CharacterController.isGrounded;
    }

    private void GetInput(out float speed)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

#if !MOBILE_INPUT
         m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
        speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
        m_Input = new Vector2(horizontal, vertical);

        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (m_CollisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }

}