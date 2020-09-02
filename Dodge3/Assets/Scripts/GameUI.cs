using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    [SerializeField] PlayerController m_Player = null;
    [SerializeField] Turrets[] m_Turret = null;
    [SerializeField] ItemObjectMgr m_ItemObjectMgr = null;
    [SerializeField] Transform m_BulletParent = null;


    private void Start()
    {
    }

    public void Initialize()
    {
        m_Player.Initialize();

        foreach (Turrets kTurret in m_Turret)
            kTurret.Initialize(m_Player.transform, m_BulletParent);

        Initialize_Items();

        m_Player.OnMyCollision = MyCollision_Enter;
    }

    public void Init_ResultEnter()
    {
        m_Player.SetIsMove(false);
        for (int i = 0; i < m_Turret.Length; i++)
            m_Turret[i].StopFire();
        DestroyAllBullet();
    }

    public void Initialize_Items()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        float fKeepTime = kGameInfo.m_AssetStage.m_ItemKeepTime; // 데이터 양이 많을때
        float fDelay = kGameInfo.itemAppearDelay; // 데이터 양이 적을때(잘 사용하지 않음)

        m_ItemObjectMgr.Initialize(fKeepTime, fDelay);
    }

    public void Init_ReadyEnter()
    {

        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        if (kGameInfo.m_AssetStage != null)
            kActorInfo.m_HP = kGameInfo.playerHP;

        m_Player.m_isMove = false;
    }

    private void MyCollision_Enter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Collision_Bullet();
        }

        if (other.tag == "Item")
        {
            Collision_Item(other);
        }
    }

    private void Collision_Bullet()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        kGameInfo.AddDamage();
        if (kGameInfo.m_ActorInfo.IsDie())
        {
            m_Player.PlayerDie();
            GameScene kGameScene = GetComponentInParent<GameScene>();
            kGameScene.m_BattleFSM.SetResultState();
        }
    }

    private void Collision_Item(Collider other)
    {
        CItemObj kCItemObj = other.gameObject.GetComponent<CItemObj>();
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;
        AssetItem kAss = AssetMgr.Inst.GetAssetItem(kCItemObj.m_ID);

        if (kAss.m_nType == (int)ItemInfo.Type.eHealing)
        {
            m_ItemObjectMgr.HideItem(other.gameObject);
            kActorInfo.m_HP += (int)kAss.m_fValue;
            ActionHealingEffect();
        }

        else if (kAss.m_nType == (int)ItemInfo.Type.eExplose)
        {
            m_ItemObjectMgr.HideItem(other.gameObject);
            TurretFire(false);
            ActionExploseEffect();
            DestroyAllBullet();
            Invoke("CI_TurretResetFire", kAss.m_fValue);
        }
    }

    private void ActionHealingEffect()
    {
        m_ItemObjectMgr.ActionEffect((int)ItemInfo.Type.eHealing);
    }

    private void ActionExploseEffect()
    {
        m_ItemObjectMgr.ActionEffect((int)ItemInfo.Type.eExplose);
    }

    private void DestroyAllBullet()
    {
        Bullet[] kBullets = m_BulletParent.GetComponentsInChildren<Bullet>();
        foreach (Bullet kBullet in kBullets)
            Destroy(kBullet.gameObject, 0.01f);

        //for(int i = 0; i < kBullets.Length; i++)
        //{
        //    Destroy(kBullets[i].gameObject, 0.01f);
        //}
    }

    private void CI_TurretResetFire()
    {
        TurretFire(true);
    }

    private void TurretFire(bool bFire)
    {
        for (int i = 0; i < m_Turret.Length; i++)
        {
            if (bFire)
                m_Turret[i].ReStartFire();
            else
                m_Turret[i].StopFire();
        }

    }
}
