using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSuccesDlg : MonoBehaviour
{
    [SerializeField] Button m_btnRestart = null;
    [SerializeField] Button m_btnNext = null;
    [SerializeField] Button m_btnExit = null;
    
    [SerializeField] Text m_txtStage = null;
    [SerializeField] Text m_txtScore = null;
    [SerializeField] Text m_txtTotal = null;


    private void Start()
    {
        Initialize();

        m_btnRestart.onClick.AddListener(OnClicked_btnRestart);
        m_btnNext.onClick.AddListener(OnClicked_btnNext);
        m_btnExit.onClick.AddListener(OnClicked_btnExit);
    }


    public void Initialize()
    {
        SetTextStage();
        SetTextScore();
        SetTextTotal();
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void SetTextStage()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        string str = kGameInfo.m_nStage.ToString();

        m_txtStage.text = str;
    }

    private void SetTextScore()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        string str = kGameInfo.CalculateScore().ToString();

        m_txtScore.text = str;
    }

    private void SetTextTotal()
    {
    }

    private void OnClicked_btnRestart()
    {
        Show(false);
        GameScene kGameScene = GetComponentInParent<GameScene>();
        kGameScene.m_BattleFSM.SetReadyState();
    }

    private void OnClicked_btnNext()
    {
        Show(false);
        SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;
        GameScene kGameScene = GetComponentInParent<GameScene>();

        kSaveInfo.m_LastStage++;
        kGameScene.m_BattleFSM.SetReadyState();
    }

    private void OnClicked_btnExit()
    {
        Show(false);
        SceneManager.LoadScene("MainScene");
    }
}
