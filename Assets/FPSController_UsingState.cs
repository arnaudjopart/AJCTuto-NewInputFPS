using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class FPSController_UsingState : MonoBehaviour
{
    private Keyboard m_keyboard;
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private float m_rotateSpeed;

    private Vector3 m_rotation;
    private Mouse m_mouse;

    // Start is called before the first frame update
    void Start()
    {    
        m_keyboard = Keyboard.current;
        m_mouse = Mouse.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_keyboard == null) return;
        
        var moveVector = GetMoveVector();
        var mouseDelta = new Vector2(m_mouse.delta.x.ReadValue(),m_mouse.delta.y.ReadValue());
            
        Move(moveVector);
        Look(mouseDelta);


    }

    private Vector2 GetMoveVector()
    {
        var xMove = 0;
        var zMove = 0;

        if (m_keyboard.aKey.isPressed)
        {
            xMove -= 1;
        }

        if (m_keyboard.dKey.isPressed)
        {
            xMove += 1;
        }

        if (m_keyboard.sKey.isPressed)
        {
            zMove -= 1;
        }

        if (m_keyboard.wKey.isPressed)
        {
            zMove += 1;
        }
        
        return new Vector2(xMove,zMove);
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
