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

	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(m_ActivateButton))
        {
            m_ShieldUp = true;
        }

        if(m_ShieldUp)
        {
            Defend();


            Vector3 axis = transform.TransformDirection(Vector3.left);
            if (m_Counter < -m_RaisedAngle)
            {
                transform.RotateAround(transform.parent.position, axis, m_RaiseSpeed);
                m_Counter += 5;
            }
            else if(m_Counter >= -m_RaisedAngle && m_Counter <= 2 * -m_RaisedAngle)
            {
                transform.RotateAround(transform.parent.position, axis, -m_RaiseSpeed);
                m_Counter += 5;
            }
            else
            {
                m_Counter = 0;
                m_ShieldUp = false;
            }

        }
		
	}

    void Defend()
    {
        //Locate Enemy Player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject enemyPlayer = null;
        foreach (GameObject player in players)
        {
            if (player.transform != transform.parent.parent)
            {
                enemyPlayer = player;
            }
        }
        Vector3 playerToEnemy = enemyPlayer.transform.position - transform.parent.parent.position;
        if(playerToEnemy.magnitude <= m_ShieldRange)
        {
            WeaponArmController enemyController = enemyPlayer.GetComponent<WeaponArmController>();


            if (enemyController.IsHitting())
            {
                enemyPlayer.GetComponent<Rigidbody>().AddForce(enemyController.GetPushStrength() * Vector3.Normalize(playerToEnemy));
                enemyController.Deflected();
                //Source: https://freesound.org/people/kingof_thelab/sounds/340239/
                GetComponent<AudioSource>().Play();
            }
        }

       

    }

}
