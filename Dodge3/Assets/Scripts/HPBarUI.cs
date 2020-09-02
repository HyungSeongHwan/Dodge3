using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Image m_imgHPBar = null;
    public Text m_txtHPValue = null;

    private void Update()
    {
        Update_HPBar();
    }

    public void Update_HPBar()
    {
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;
        if (kActorInfo.m_MaxHP == 0) return;

        float fHP = ((float)kActorInfo.m_HP / kActorInfo.m_MaxHP);

        m_imgHPBar.fillAmount = fHP;
        m_txtHPValue.text = kActorInfo.m_HP.ToString();
    }
}
