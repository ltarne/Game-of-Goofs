using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // --------------------------------------------------------------

    // The character's running speed
    [SerializeField]
    float m_RunSpeed = 5.0f;

    // The gravity strength
    [SerializeField]
    float m_Gravity = 60.0f;

    // The maximum speed the character can fall
    //[SerializeField]
    //float m_MaxFallSpeed = 20.0f;

    // The character's jump height
    [SerializeField]
    float m_JumpHeight = 4.0f;

    // Identifier for Input
    [SerializeField]
    string m_PlayerInputString = "_P1";

    // --------------------------------------------------------------


    //The rigid body of the player
    Rigidbody m_RigidBody;

    // The current movement direction in x & z.
    Vector3 m_MovementDirection = Vector3.zero;

    // The current movement speed
    float m_MovementSpeed = 0.0f;

    // The current vertical / falling speed
    //float m_VerticalSpeed = 0.0f;

    // The current movement offset
    Vector3 m_CurrentMovementOffset = Vector3.zero;

    // The starting position of the player
    Vector3 m_SpawningPosition = Vector3.zero;

    // Whether the player is alive or not
    bool m_IsAlive = true;

    // The time it takes to respawn
    const float MAX_RESPAWN_TIME = 1.0f;
    float m_RespawnTime = MAX_RESPAWN_TIME;

    // --------------------------------------------------------------

    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -m_Gravity, 0);
    }

    // Use this for initialization
    void Start()
    {
        m_SpawningPosition = transform.position;
    }

    void Jump()
    {
        //m_VerticalSpeed = Mathf.Sqrt(m_JumpHeight * m_Gravity);
        m_RigidBody.AddForce(Vector3.up * Mathf.Sqrt(m_JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }


    void UpdateMovementState()
    {
        // Get Player's movement input and determine direction and set run speed
        float horizontalInput = Input.GetAxisRaw("Horizontal" + m_PlayerInputString);
        float verticalInput = Input.GetAxisRaw("Vertical" + m_PlayerInputString);

        m_MovementDirection = new Vector3(horizontalInput, 0, verticalInput);
        m_MovementSpeed = m_RunSpeed;
 
    }

    void UpdateJumpState()
    {
        // Character can jump when standing on the ground
        //if (Input.GetButtonDown("Jump" + m_PlayerInputString) && m_RigidBody.isGrounded)
        //{
        //    Jump();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is dead update the respawn timer and exit update loop
        if(!m_IsAlive)
        {
            UpdateRespawnTime();
            return;
        }

        // Update movement input
        UpdateMovementState();

        // Update jumping input and apply gravity
        UpdateJumpState();


        //m_RigidBody.AddForce((m_MovementDirection * m_MovementSpeed + new Vector3(0, m_VerticalSpeed, 0)) /** Time.deltaTime*/);

        // Calculate actual motion
        m_CurrentMovementOffset = (m_MovementDirection * m_MovementSpeed) * Time.deltaTime;


        // Rotate the character in movement direction
        if (m_MovementDirection != Vector3.zero)
        {
            RotateCharacter(m_MovementDirection);
        }
    }

    void FixedUpdate()
    {
        m_RigidBody.MovePosition(m_RigidBody.position + m_CurrentMovementOffset);
    }

    void RotateCharacter(Vector3 movementDirection)
    {
        Quaternion lookRotation = Quaternion.LookRotation(movementDirection);
        if (m_RigidBody.rotation != lookRotation)
        {
            m_RigidBody.rotation = lookRotation;
        }

        if(Physics.gravity.y > 0)
        {
            Vector3 upsideDownRotation = new Vector3(m_RigidBody.rotation.eulerAngles.x + 180, m_RigidBody.rotation.eulerAngles.y, m_RigidBody.rotation.eulerAngles.z);
            m_RigidBody.rotation = Quaternion.Euler(upsideDownRotation); 
        }
    }

    public int GetPlayerNum()
    {
        if(m_PlayerInputString == "_P1")
        {
            return 1;
        }
        else if (m_PlayerInputString == "_P2")
        {
            return 2;
        }

        return 0;
    }

    public void Die()
    {
        m_IsAlive = false;
        m_RespawnTime = MAX_RESPAWN_TIME;
    }

    void UpdateRespawnTime()
    {
        m_RespawnTime -= Time.deltaTime;
        if (m_RespawnTime < 0.0f)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        m_IsAlive = true;
        m_RigidBody.position = m_SpawningPosition;
        m_RigidBody.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    
}
