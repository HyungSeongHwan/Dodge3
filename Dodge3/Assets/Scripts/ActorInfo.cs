using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInfo
{
    public int m_HP = 0;
    public int m_MaxHP = 0;
    public int m_ExtraHP = 0;

    public void Initialize(int nMaxHP)
    {
        m_MaxHP = nMaxHP;
        m_ExtraHP = CalculateAddHP();
        m_HP = nMaxHP + m_ExtraHP;
    }

    public void AddDamage(int nDamage)
    {
        m_HP -= nDamage;
        if (m_HP <= 0)
            m_HP = 0;
    }

    public void HealHP(int nHeal)
    {
        m_HP += nHeal;

    }

    public bool IsDie()
    {
        return m_HP == 0;
    }

    public int CalculateAddHP()
    {
        SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;
        return (int)(kSaveInfo.m_AccumulateScore * 0.001f * Config.DMIN_ADD_HP);
    }

    public int CalculateMaxHP()
    {
        return m_MaxHP + m_ExtraHP;
    }

}
