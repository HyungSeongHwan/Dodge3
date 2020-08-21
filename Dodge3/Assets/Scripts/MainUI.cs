using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    public Button m_btnStart = null;
    public Button m_btnSetting = null;

    public OptionDlg m_OptionDlg = null;


    private void Start()
    {
        m_btnStart.onClick.AddListener(OnClicked_btnStart);
        m_btnSetting.onClick.AddListener(OnClicked_btnSetting);
    }


    public void Initialize()
    {

    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void OnClicked_btnStart()
    {
    }

    private void OnClicked_btnSetting()
    {
        Show(false);

        m_OptionDlg.Show(true);
    }


}
