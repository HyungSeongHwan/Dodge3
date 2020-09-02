using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultFailedDlg : MonoBehaviour
{
    [SerializeField] Button m_btnRestart = null;
    [SerializeField] Button m_btnExit = null;


    private void Start()
    {
        m_btnRestart.onClick.AddListener(OnClicked_btnRestart);
        m_btnExit.onClick.AddListener(OnClicked_btnExit);
    }


    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void OnClicked_btnRestart()
    {
        Show(false);
        GameScene kGameScene = GetComponentInParent<GameScene>();
        kGameScene.m_BattleFSM.SetReadyState();
    }

    private void OnClicked_btnExit()
    {
        Show(false);
        SceneManager.LoadScene("MainScene");
    }
}
