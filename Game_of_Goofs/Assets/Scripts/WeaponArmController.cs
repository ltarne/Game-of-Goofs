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
    public float m_Range = 3.0f;

    [SerializeField]
    public float m_PushPower = 200.0f;

    [SerializeField]
    public string m_ActivateButton = "e";

    private bool m_Attack = false;
    private bool m_FinishAttack = false;
    private float m_Counter = 0;
    private int m_Charge = 0;

	
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
                m_Counter += 5;
            }
            else if (m_Counter >= -m_RaisedAngle && m_Counter <= (2 * -m_RaisedAngle) && m_FinishAttack)
            {
                transform.RotateAround(transform.parent.position, axis, -m_RaiseSpeed);
                m_Counter += 5;
            }
            else if(m_Counter == (2 * -m_RaisedAngle)+5 && m_FinishAttack)
            {
                m_Counter = 0;
                m_Attack = false;
                m_FinishAttack = false;
                Attack();
            }

        }
    }

    void Attack()
    {
        //Locate Enemy Player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject enemyPlayer = null;
        foreach(GameObject player in players)
        {
            if(player.transform != transform.parent.parent)
            {
                enemyPlayer = player;
            }
        }

        if(enemyPlayer)
        {
            //Check if in arc
            Vector3 playersBodyPosition = transform.parent.parent.position;
            Vector3 playerToEnemy = (enemyPlayer.transform.position - playersBodyPosition);
            Vector3 playerToEdge = (transform.parent.parent.forward * m_Range);
            if (playerToEnemy.sqrMagnitude < m_Range * m_Range)
            {
                //              /dot(a,b)\
                //Angle = arccos|--------|
                //              \|a|.|b| /
                float angle = Mathf.Acos(Vector3.Dot(playerToEnemy, playerToEdge) / (playerToEnemy.magnitude * playerToEdge.magnitude));
                angle *= Mathf.Rad2Deg;
                if(angle > -20 && angle < 20)
                {
                    //In arc
                    enemyPlayer.GetComponent<Rigidbody>().AddForce(m_Charge * m_PushPower * Vector3.Normalize(playerToEnemy));
                }
            }
        }
       


    }
}
