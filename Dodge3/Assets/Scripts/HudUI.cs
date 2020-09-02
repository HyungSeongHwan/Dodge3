using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    //public MainUI m_MainUI = null;
    public CountDownDlg m_CountDownDlg = null;
    public HPBarUI m_HPBarUI = null;
    public StageTimeBarUI m_StageTimeBarUI = null;
    public ResultSuccesDlg m_ResultSuccesDlg = null;
    public ResultFailedDlg m_ResultFailDlg = null;


    public void Init_ReadyEnter()
    {
        StartReadyCount();
    }

    public void Init_ResultEnter()
    {
        ShowResultDlg();
    }

    public void StartReadyCount()
    {
        m_CountDownDlg.Show(true);
        m_CountDownDlg.Initialize();
    }

    public void ShowResultDlg()
    {
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;

        if (kActorInfo.IsDie())
            m_ResultFailDlg.Show(true);

        else
            m_ResultSuccesDlg.Show(true);
    }

}
