using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    [SerializeField]
    float m_WeaponPush = 1.0f;

    [SerializeField]
    float m_WeaponAngle = 90.0f;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e"))
        {
            Hit();
        }
    }

    void Hit()
    {
        transform.RotateAround(transform.position, Vector3.left, m_WeaponAngle);
    }
}
