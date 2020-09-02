using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public int m_nStage = 1;
    public bool m_isSuccess = false;
    public float m_fDurationTime = 0;

    public ActorInfo m_ActorInfo = new ActorInfo();
    public List<ItemInfo> m_ListItemInfo = new List<ItemInfo>();
    public AssetStage m_AssetStage = null;

    // 데이터 양이 적을 때만 사용 (잘 사용하지 않음)
    public float fireDelayTime { get { return m_AssetStage.m_FireDelayTime; } }
    public float bulletSpeed { get { return m_AssetStage.m_BulletSpeed; } }
    public float stageKeepTime { get { return m_AssetStage.m_StageKeepTime; } }
    public int playerHP { get { return m_AssetStage.m_PlayerHP; } }
    public int bulletDamage { get { return m_AssetStage.m_BulletAttack; } }
    public int itemAppearDelay { get { return m_AssetStage.m_ItemAppearDelay; } }
    public int itemKeepTime { get { return m_AssetStage.m_ItemKeepTime; } }
    public int turretCount { get { return m_AssetStage.m_TurretCount; } }

    public void Initialize()
    {
        SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;
        m_nStage = kSaveInfo.m_LastStage;

        Initialize_Stage(m_nStage);
        Initialize_Item();
    }

    public void Initialize_Stage(int nStage)
    {
        m_AssetStage = AssetMgr.Inst.GetAssetStage(nStage);
        m_ActorInfo.Initialize(m_AssetStage.m_PlayerHP);
        m_fDurationTime = 0;
    }

    public void Initialize_Item()
    {
        for (int i = 0; i < AssetMgr.Inst.m_AssItems.Count; i++)
        {
            AssetItem kAss = AssetMgr.Inst.m_AssItems[i];
            ItemInfo kInfo = new ItemInfo();
            kInfo.Initialize(kAss.m_nType, kAss.m_fValue);
            m_ListItemInfo.Add(kInfo);
        }
    }

    public int CalculateScore()
    {
        int curHP = m_ActorInfo.m_HP;
        int maxHP = m_ActorInfo.CalculateMaxHP();
        int nScore = (int)(((float)curHP / maxHP) * Config.DMAX_SCORE);
        if (nScore < Config.DMIN_SCORE)
            nScore = Config.DMIN_SCORE;

        return nScore;
    }

    public void AddDamage()
    {
        m_ActorInfo.AddDamage(this.bulletDamage);
    }

    public bool IsPlayerDie()
    {
        return m_ActorInfo.IsDie();
    }

    public void OnUpdate(float fElasedTime)
    {
        m_fDurationTime += fElasedTime;
    }

    public bool IsSuccess()
    {
        return m_isSuccess;
    }

    public ItemInfo ActionItem(int nItemId)
    {
        ItemInfo kInfo = GetItemInfo(nItemId);
        if (kInfo.m_ItemType == (int)ItemInfo.Type.eHealing)
        {
            m_ActorInfo.m_HP += (int)kInfo.m_ItemType;
        }

        if (kInfo.m_ItemType == (int)ItemInfo.Type.eExplose)
        {
            //return kInfo;
        }

        return kInfo;
    }

    public ItemInfo GetItemInfo(int id)
    {
        if (id > 0 && id <= m_ListItemInfo.Count)
        {
            return m_ListItemInfo[id - 1];
        }

        return null;
    }

    public float CalculateStageKeepTime()
    {
        if (m_AssetStage == null) return 1.0f;

        return (float)(stageKeepTime - m_fDurationTime) / stageKeepTime;

    }

    public int DurationStageKeeptime()
    {
        if (m_AssetStage == null) return 0;

        if (m_fDurationTime >= stageKeepTime) return 0;
        return (int)(stageKeepTime - m_fDurationTime);
    }

    public int DurationTimerM()
    {
        int nTimerM = (int)m_fDurationTime / 60;

        return nTimerM;
    }

    public int DurationTimerS()
    {
        int nTimerS = (int)m_fDurationTime % 60;

        return nTimerS;
    }

    public float BulletSpeed()
    {
        if (m_AssetStage == null) return 0.0f;
        return bulletSpeed;
    }

    public bool IsDurationTime()
    {
        if (m_AssetStage == null) return false;
        return (m_fDurationTime >= stageKeepTime);
    }
}
