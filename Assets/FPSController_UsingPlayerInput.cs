using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController_UsingPlayerInput : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed;
    [SerializeField]
    private float m_rotateSpeed;
    private Vector3 m_rotation;
    private Vector2 m_moveVector;
    private Vector2 m_lookVector;
    [SerializeField]
    private GameObject m_bulletPrefab;
    [SerializeField]
    private Transform m_spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(m_moveVector);
        Look(m_lookVector);
    }

    public void OnMove(InputAction.CallbackContext _ctx)
    {
        m_moveVector = _ctx.ReadValue<Vector2>();
        
    }

    public void OnLook(InputAction.CallbackContext _ctx)
    {
        m_lookVector = _ctx.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext _ctx)
    {
        if(_ctx.performed) Shoot();
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
    
    private void Shoot()
    {
        Instantiate(m_bulletPrefab, m_spawnPoint.position, m_spawnPoint.rotation);
    }
}
