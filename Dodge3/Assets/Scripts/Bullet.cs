using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 m_Direction = Vector3.zero;

    public float m_MoveSpeed = 0.0f;


    private void Update()
    {
        Update_BulletSpeed();
        MoveBullet();
    }


    public void Initialize(Vector3 vTargetPos)
    {
        m_Direction = vTargetPos - transform.position;
        m_Direction.Normalize();

        transform.LookAt(vTargetPos);
    }

    public void Update_BulletSpeed()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        m_MoveSpeed = kGameInfo.BulletSpeed();
    }

    private void MoveBullet()
    {
        transform.Translate(m_Direction * m_MoveSpeed * Time.deltaTime, Space.World);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
