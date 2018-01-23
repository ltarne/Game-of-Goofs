using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldArmController : MonoBehaviour {

    

    [SerializeField]
    public float m_RaisedAngle = -60;

    [SerializeField]
    public float m_RaiseSpeed = 5.0f;

    private float m_Counter = 0;
    private bool m_ShieldUp = false;

	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown("q"))
        {
            m_ShieldUp = true;
        }

        if(m_ShieldUp)
        {
            Vector3 axis = transform.TransformDirection(Vector3.left);
            if (m_Counter < -m_RaisedAngle)
            {
                transform.RotateAround(transform.parent.position, axis, -m_RaiseSpeed);
                m_Counter += 5;
            }
            else if(m_Counter >= -m_RaisedAngle && m_Counter <= 2 * -m_RaisedAngle)
            {
                transform.RotateAround(transform.parent.position, axis, m_RaiseSpeed);
                m_Counter += 5;
            }
            else
            {
                m_Counter = 0;
                m_ShieldUp = false;
            }

        }
		
	}

}
