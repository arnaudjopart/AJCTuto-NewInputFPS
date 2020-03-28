using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController_UsingActionAsset : MonoBehaviour
{

    private FPS_InputSystem m_controls;
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private float m_rotateSpeed;
    private Vector3 m_rotation;

    // Start is called before the first frame update

    private void Awake()
    {
        m_controls = new FPS_InputSystem();
    }

    void Start()
    {
        
        
    }

    private void OnEnable()
    {
        m_controls.Enable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var moveVector = m_controls.Gameplay.Move.ReadValue<Vector2>();
        var lookVector = m_controls.Gameplay.Look.ReadValue<Vector2>();
        
        Look(lookVector);
        Move(moveVector);
        
    }
    
    private void Move(Vector2 _moveVector)
    {
        if (_moveVector.sqrMagnitude < 0.01) return;
        
        var moveSpeedPerFrame = m_moveSpeed * Time.deltaTime;
        var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(_moveVector.x, 0, _moveVector.y);
        transform.position += move * moveSpeedPerFrame;
    }
    
    private void Look(Vector2 _rotate)
    {
        if (_rotate.sqrMagnitude < 0.01) return;
        
        var rotateSpeedPerFrame = m_rotateSpeed * Time.deltaTime;
        m_rotation.y += _rotate.x * rotateSpeedPerFrame;
        m_rotation.x = Mathf.Clamp(m_rotation.x - _rotate.y * rotateSpeedPerFrame, -89, 89);
        transform.localEulerAngles = m_rotation;
    }
}
