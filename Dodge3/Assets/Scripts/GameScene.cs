using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameScene : MonoBehaviour
{

    public GameUI m_GameUI = null;
    public HudUI m_HudUI = null;

    [HideInInspector] public BattleFSM m_BattleFSM = new BattleFSM();


    private void Awake()
    {

        if (!AssetMgr.Inst.IsInstalled)
            AssetMgr.Inst.Initilaize();

        GameMgr.Inst.Initialize();
        LocalSave.Inst().Load();
        GameMgr.Inst.LoadFile();
    }

    private void Update()
    {
        if (m_BattleFSM != null)
        {
            m_BattleFSM.OnUpdate();

            if (m_BattleFSM.IsGameState())
                GameMgr.Inst.OnUpdate(Time.deltaTime);
        }
    }

    private void Start()
    {
        m_BattleFSM.Initialize(Callback_ReadyEnter, Callback_WaveEnter, Callback_GameEnter, Callback_ResultEnter);
        GameMgr.Inst.SetGameScene(this);

        m_BattleFSM.SetReadyState();
    }


    public void Callback_ReadyEnter()
    {
        m_HudUI.Init_ReadyEnter();
        m_GameUI.Init_ReadyEnter();
        Invoke("CallbackInvoke_Game", 4.0f);
    }

    void CallbackInvoke_Game()
    {
        m_BattleFSM.SetGameState();
    }

    public void Callback_WaveEnter()
    {

    }

    public void Callback_GameEnter()
    {
        GameMgr.Inst.InitStart();
        m_GameUI.Initialize();
    }

    public void Callback_ResultEnter()
    {
        m_GameUI.Init_ResultEnter();
        m_HudUI.Init_ResultEnter();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("[GameScene] App Quit .....");
        try
        {
            GameMgr.Inst.SaveFile();
            LocalSave.Inst().Save();
        }
        catch(System.Exception e)
        {
            Debug.Log("_Error_Quit " + e.ToString());
        }
    }
}
