using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldFlipEvent : MonoBehaviour {

    private UnityAction m_CameraListener;
    private UnityAction m_PlayerListener;

    void Awake()
    {
        m_CameraListener = new UnityAction(FlipCamera);
        m_PlayerListener = new UnityAction(FlipPlayers);
    }

    void OnEnable()
    {
        EventManager.StartListening("worldFlip", m_CameraListener);
        EventManager.StartListening("worldFlip", m_PlayerListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("worldFlip", m_CameraListener);
        EventManager.StopListening("worldFlip", m_PlayerListener);
    }

    void FlipCamera()
    {
        Camera camera = null;

        camera = FindObjectOfType(typeof(Camera)) as Camera;
        if(camera)
        {
            camera.GetComponent<CameraController>().Flip();
        }
        else
        {
            Debug.LogError("No camera found.");
        }
    }

    void FlipPlayers()
    {
        Physics.gravity = new Vector3(0, -Physics.gravity.y, 0);
    }

	
}
