using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFSM
{

    public delegate void DelegateFunc();


    public class CState
    {
        public DelegateFunc m_OnEnterFunc = null;

        public virtual void Initialize(DelegateFunc func)
        {
            m_OnEnterFunc = new DelegateFunc(func);
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }

    public class CReadyState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }

    public class CWaveState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }

    public class CGameState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }

    public class CResultState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }



    private CState m_CurState = null;
    private CState m_NewState = null;

    private CState m_Ready = new CReadyState();
    private CState m_Wave = new CWaveState();
    private CState m_Game = new CGameState();
    private CState m_Result = new CResultState();


    public void Initialize(DelegateFunc kReady, DelegateFunc kWave, DelegateFunc kGame, DelegateFunc kResult)
    {
        m_Ready.Initialize(kReady);
        m_Wave.Initialize(kWave);
        m_Game.Initialize(kGame);
        m_Result.Initialize(kResult);
    }

    public void SetState(CState kState)
    {
        m_NewState = kState;
    }

    public void OnUpdate()
    {
        if (m_NewState != null)
        {
            if (m_CurState != null)
                m_CurState.OnExit();

            m_CurState = m_NewState;
            m_NewState = null;

            if (m_CurState != null)
                m_CurState.OnEnter();
        }

        if (m_CurState != null)
            m_CurState.OnUpdate();
    }

    public void SetReadyState()
    {
        SetState(m_Ready);
    }

    public void SetWaveState()
    {
        SetState(m_Wave);
    }

    public void SetGameState()
    {
        SetState(m_Game);
    }

    public void SetResultState()
    {
        SetState(m_Result);
    }

    public bool IsCurState(CState kState)
    {
        if (m_CurState == null)
            return false;

        return (m_CurState == kState) ? true : false;
    }

    public bool IsResultState()
    {
        return (m_CurState == m_Result) ? true : false;
    }
    public bool IsGameState()
    {
        return (m_CurState == m_Game) ? true : false;
    }

    public bool IsNoneState()
    {
        return (m_CurState == null) ? true : false;
    }

    public CState GetCurState()
    {
        return m_CurState;
    }

    public void SetNoneState()
    {
        m_NewState = null;
        m_CurState = null;
    }
}
