using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //--------------------------------------------------------------
    //Primary camera rotation
    [SerializeField]
    Vector3 m_PrimaryRotation = new Vector3(50,0,0);

    //Secondary camera rotation
    [SerializeField]
    Vector3 m_SecondaryRotation = new Vector3(-50, 0, 180);

    //The speed of the rotation
    [SerializeField]
    float m_FlipSpeed = 10.0f;

    //The time threshold to trigger the flip
    [SerializeField]
    float m_FlipTime = 5.0f;

    //---------------------------------------------------------------

    bool m_Flip = false;

    //---------------------------------------------------------------

	// Use this for initialization
	void Start () {
        transform.rotation = Quaternion.Euler(m_PrimaryRotation);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 currentRotation = transform.rotation.eulerAngles;

        if (Time.time > m_FlipTime)
        {
            m_Flip = true;
        }

       //Flip upside down
        if (m_Flip)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(m_SecondaryRotation), m_FlipSpeed * Time.deltaTime);
        }
        //Flippin right way up
        else if (!m_Flip)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(m_PrimaryRotation), m_FlipSpeed * Time.deltaTime);
        }
    }

}
