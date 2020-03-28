using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FPSController_UsingActions : MonoBehaviour
{

    public InputAction m_moveAction;
    public InputAction m_lookAction;

    [SerializeField]
    private float m_rotateSpeed;
    [SerializeField]
    private float m_moveSpeed;
    private Vector3 m_rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        m_lookAction.Enable();
        m_moveAction.Enable();
    }

    private void OnDisable()
    {
        m_lookAction.Disable();
        m_moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var look = m_lookAction.ReadValue<Vector2>();
        var move = m_moveAction.ReadValue<Vector2>();

        Look(look);
        Move(move);
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
