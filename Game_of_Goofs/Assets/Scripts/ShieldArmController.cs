using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldArmController : MonoBehaviour {

    

    [SerializeField]
    public float m_RaisedAngle = -60;

    [SerializeField]
    public float m_RaiseSpeed = 5.0f;

    [SerializeField]
    public float m_ShieldRange = 3.0f;

    [SerializeField]
    public string m_ActivateButton = "q";

    private float m_Counter = 0;
    private bool m_ShieldUp = false;
    private GameObject m_Player;

    void Start()
    {
        m_Player = transform.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update () {

        if(Input.GetKeyDown(m_ActivateButton))
        {
            m_ShieldUp = true;
        }

        if(m_ShieldUp)
        {
            //Defend();


            Vector3 axis = transform.TransformDirection(Vector3.left);
            if (m_Counter < -m_RaisedAngle)
            {
                transform.RotateAround(transform.parent.position, axis, m_RaiseSpeed);
                m_Counter += m_RaiseSpeed;
            }
            else if(m_Counter >= -m_RaisedAngle && m_Counter < (2 * -m_RaisedAngle))
            {
                transform.RotateAround(transform.parent.position, axis, -m_RaiseSpeed);
                m_Counter += m_RaiseSpeed;
            }
            else
            {
                m_Counter = 0;
                m_ShieldUp = false;
            }

        }
		
	}

    public void Defend()
    {
        //Locate enemy player
        GameObject enemyPlayer = m_Player.GetComponent<PlayerController>().FindEnemyPlayer();

        if (enemyPlayer)
        {
            Vector3 playerToEnemy = enemyPlayer.transform.position - m_Player.transform.position;
            if (playerToEnemy.magnitude <= m_ShieldRange)
            {
                WeaponArmController enemyController = enemyPlayer.GetComponentInChildren<WeaponArmController>();


                //if (enemyController.IsHitting())
                //{
                Rigidbody body = enemyPlayer.GetComponent<Rigidbody>();
                body.AddForce(enemyController.GetPushStrength() * Vector3.Normalize(playerToEnemy));
                    //enemyController.Deflected();
                    //Source: https://freesound.org/people/kingof_thelab/sounds/340239/
                    GetComponent<AudioSource>().Play();
                //}
            }
        }
 

    }

    public bool IsDefending()
    {
        return m_ShieldUp;
    }

}
