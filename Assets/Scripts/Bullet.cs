using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_speed=10;

    public float m_lifeTime=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var transform1 = transform;
        transform1.position+=Time.deltaTime * m_speed * transform1.forward;
        m_lifeTime -= Time.deltaTime;
        if (m_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
