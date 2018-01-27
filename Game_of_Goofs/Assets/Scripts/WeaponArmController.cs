using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArmController : MonoBehaviour {

    [SerializeField]
    public float m_RaisedAngle = -60;

    [SerializeField]
    public float m_RaiseSpeed = 5.0f;

    [SerializeField]
    public int m_MaxCharge = 200;

    [SerializeField]
    public float m_SwingAngle = 20.0f;

    [SerializeField]
    public float m_Range = 3.0f;

    [SerializeField]
    public float m_PushPower = 200.0f;

    [SerializeField]
    public string m_ActivateButton = "e";

    private bool m_Attack = false;
    private bool m_FinishAttack = false;
    private float m_Counter = 0;
    private int m_Charge = 0;

    private GameObject m_Player;
    private GameObject m_EnemyPlayer;

    void Start()
    {
        m_Player = transform.parent.parent.gameObject;
        m_EnemyPlayer = m_Player.GetComponent<PlayerController>().FindEnemyPlayer();
    }


    public bool IsHitting()
    {
        return m_FinishAttack;
    }

    public float GetPushStrength()
    {
        return m_Charge * m_PushPower;
    }

    public void Deflected()
    {
        m_Charge = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //Start of attack
        if (Input.GetKeyDown(m_ActivateButton))
        {
            m_Attack = true;
            m_Charge = 0;
        }
        //Charge weapon
        if(Input.GetKey(m_ActivateButton))
        {
            if(m_Charge < m_MaxCharge)
            {
                m_Charge++;
            }
        }
        if(!Input.GetKey(m_ActivateButton) && m_Attack)
        {
            m_FinishAttack = true;
        }

        if (m_Attack)
        {
            Vector3 axis = transform.TransformDirection(Vector3.left);
            if (m_Counter < -m_RaisedAngle)
            {
                transform.RotateAround(transform.parent.position, axis, m_RaiseSpeed);
                m_Counter += m_RaiseSpeed;
            }
            else if (m_Counter >= -m_RaisedAngle && m_Counter < (2 * -m_RaisedAngle) && m_FinishAttack)
            {
                transform.RotateAround(transform.parent.position, axis, -m_RaiseSpeed);
                m_Counter += m_RaiseSpeed;
            }
            else if(m_Counter == (2 * -m_RaisedAngle) && m_FinishAttack)
            {
                m_Counter = 0;
                m_Attack = false;
                m_FinishAttack = false;
                ResolveCombat();
                //Source: https://freesound.org/people/tec%20studios/sounds/106861/
                
            }

        }
    }

    void ResolveCombat()
    {

        if (m_EnemyPlayer)
        {

            if(m_EnemyPlayer.GetComponentInChildren<ShieldArmController>().IsDefending())
            {
                m_EnemyPlayer.GetComponentInChildren<ShieldArmController>().Defend();
            }
            else
            {
                Attack();
            }
        }
    }

    void Attack()
    {

        //Check if in arc
        Vector3 playersBodyPosition = m_Player.transform.position;
        Vector3 playerToEnemy = (m_EnemyPlayer.transform.position - playersBodyPosition);
        Vector3 playerToEdge = (transform.parent.parent.forward * m_Range);
        if (playerToEnemy.sqrMagnitude < m_Range * m_Range)
        {
            //              /dot(a,b)\
            //Angle = arccos|--------|
            //              \|a|.|b| /
            float angle = Mathf.Acos(Vector3.Dot(playerToEnemy, playerToEdge) / (playerToEnemy.magnitude * playerToEdge.magnitude));
            angle *= Mathf.Rad2Deg;
            if(angle > -m_SwingAngle && angle < m_SwingAngle)
            {
                //In arc
                m_EnemyPlayer.GetComponent<Rigidbody>().AddForce(m_Charge * m_PushPower * Vector3.Normalize(playerToEnemy));
                GetComponent<AudioSource>().Play();
            }
        }
       


    }
}
