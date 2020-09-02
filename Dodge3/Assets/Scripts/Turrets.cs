using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{

    [SerializeField] GameObject m_PrefabBullet = null;
    [SerializeField] GameObject m_TurretBody = null;
    [SerializeField] GameObject m_FirePosition = null;
    private Transform m_BulletParent = null;

    private Transform m_Target = null;

    private float m_FireDelay = 1.0f;

    public bool m_isFire = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        RotationTurretBody();
    }


    public void Initialize(Transform target, Transform bulletParent)
    {
        m_Target = target;
        SetIsFire(true);
        m_BulletParent = bulletParent;

        FireBulletObject();
    }

    private void SetIsFire(bool bFire)
    {
        m_isFire = bFire;
    }

    private void RotationTurretBody()
    {
        if( m_Target != null)
            m_TurretBody.transform.LookAt(m_Target.transform.position);
    }

    private void FireBulletObject()
    {
        //int nValue = Random.Range(1, 50);
        //float fDelay = m_FireDelay + (float)nValue * 0.01f;

        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        if (kGameInfo.fireDelayTime == 0.0f) return;
        StartCoroutine("EnumFunc_AutoFire", kGameInfo.fireDelayTime);
    }

    private void CreateBullet()
    {
        GameObject goBullet = Instantiate(m_PrefabBullet);
        goBullet.transform.position = m_FirePosition.transform.position;
        goBullet.transform.parent = m_BulletParent;
        Bullet csBullet = goBullet.GetComponent<Bullet>();

        csBullet.Initialize(m_Target.transform.position);
    }

    IEnumerator EnumFunc_AutoFire(float delay)
    {
        while (m_isFire)
        {
            CreateBullet();

            yield return new WaitForSeconds(delay);
        }

        yield return null;
    }

    public void ReStartFire()
    {
        SetIsFire(true);
        FireBulletObject();
    }

    public void StopFire()
    {
        SetIsFire(false);
    }

}
