using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArmController : MonoBehaviour {

    [SerializeField]
    public float m_RaisedAngle = -60;

    [SerializeField]
    public float m_RaiseSpeed = 5.0f;

    private bool m_Attack = false;
    private float m_Counter = 0;

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e"))
        {
            m_Attack = true;
        }

        if (m_Attack)
        {
            if (m_Counter < -m_RaisedAngle)
            {
                transform.RotateAround(transform.parent.position, Vector3.left, -m_RaiseSpeed);
                m_Counter += 5;
            }
            else if (m_Counter >= -m_RaisedAngle && m_Counter <= (2 * -m_RaisedAngle))
            {
                transform.RotateAround(transform.parent.position, Vector3.left, m_RaiseSpeed);
                m_Counter += 5;
            }
            else
            {
                m_Counter = 0;
                m_Attack = false;
            }

        }
    }
}
