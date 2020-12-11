using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameUI m_GameUI = null;
    public HudUI m_HudUI = null;

    [HideInInspector] public BattleFSM m_BattleFSM = new BattleFSM();


    private void Awake()
    {
        if (!AssetMgr.Inst.IsInstalled)
            AssetMgr.Inst.Initialize();

        GameMgr.Inst.Initialize();
        GameMgr.Inst.LoadFile();
    }

    private void Start()
    {
        m_BattleFSM.Initialize(Callback_ReadyEnter, Callback_WaveEnter, Callback_GameEnter, Callback_ResultEnter);
        GameMgr.Inst.SetGameScene(this);

        m_BattleFSM.SetReadyState();
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

    public void Callback_ReadyEnter()
    {
        m_HudUI.Init_ReadyEnter();
        Invoke("CallbackInvoke_Game", 4.0f);
    }

    private void CallbackInvoke_Game()
    {
        m_BattleFSM.SetGameState();
    }

    public void Callback_WaveEnter()
    {

    }

    public void Callback_GameEnter()
    {
        GameMgr.Inst.InitStart();
        m_GameUI.Init_GameEnter();
        m_HudUI.Init_GameEnter();
    }

    public void Callback_ResultEnter()
    {
        m_GameUI.Init_ResultEnter();
        m_HudUI.Init_ResultEnter();
    }
}
