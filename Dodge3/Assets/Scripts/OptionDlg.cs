using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionDlg : MonoBehaviour
{

    public Toggle m_toggleBGM = null;
    public Toggle m_toggleSFX = null;

    public Button m_btnSave = null;
    public Button m_btnExit = null;

    public MainUI m_MainUI = null;

    private SoundSave m_SoundSave = new SoundSave();

    private void Start()
    {
        m_btnSave.onClick.AddListener(OnClicked_btnSave);
        m_btnExit.onClick.AddListener(OnClicked_btnExit);
    }


    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void OnClicked_btnSave()
    {
        m_SoundSave.SetSoundInfo(m_toggleBGM.isOn, m_toggleSFX.isOn);
        m_SoundSave.SaveSound();
    }


    private void OnClicked_btnExit()
    {
        Show(false);

        m_MainUI.Show(true);
    }

}
