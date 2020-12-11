using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInfo
{
    public int m_nStage = 1;
    public int m_Score = 0;
    public int m_TurretCount = 0;

    public bool m_isSuccess = false;

    public float m_fDurationTime = 0;

    public ActorInfo m_ActorInfo = null;
    public EnemyInfo m_EnemyInfo = new EnemyInfo();
    public AssetStage m_AssStage = null;

    public float m_fStartInvintime = 0;
    public float m_fInvinDelay = 0;
    public bool m_isInvin = false;

    public float m_fStartDmgMulTime = 0;
    public float m_fDmgMUlDelay = 0;
    public float m_MulValue = 0;
    public bool m_isDmgMul = false;

    public float m_ActorDmg = 0;
    public float m_DefultDmg = 0;

    public void Initilaize()
    {
        if( m_ActorInfo == null)
             m_ActorInfo = new ActorInfo();

        SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;
        m_nStage = kSaveInfo.m_LastStage;

        Init_Stage(m_nStage);
    }

    public void Init_Stage(int nStage)
    {
        m_AssStage = AssetMgr.Inst.GetAssetStage(nStage);
        m_ActorInfo.Initialize(1);
        m_EnemyInfo.Initialize(m_AssStage.m_TurretId);
        m_fDurationTime = 0;
        m_TurretCount = m_AssStage.m_TurretCount;

        m_DefultDmg = m_ActorInfo.m_Atk;
        m_ActorDmg = m_DefultDmg;
    }

    public void OnUpdate(float fElasedTime)
    {
        m_fDurationTime += fElasedTime;
        if (!IsDmgMulStartTime())
            DmgMulStop();
    }

    public void AddDamagePlayer()
    {
        m_ActorInfo.AddDamage(m_EnemyInfo.m_Attack);
    }

    public void AddDamageTurret()
    {
        m_EnemyInfo.AddDamage(m_ActorDmg);
    }

    public float CalculateStageKeepTime()
    {
        if (m_AssStage == null) return 0.0f;
        return ((float)(m_AssStage.m_StageKeepTime - m_fDurationTime) / m_AssStage.m_StageKeepTime);
    }

    public string DurationStageKeepTime()
    {
        if (m_AssStage == null) return m_fDurationTime.ToString();
        return (m_AssStage.m_StageKeepTime - (int)m_fDurationTime).ToString();
    }

    public bool IsDurationTime()
    {
        if (m_AssStage == null) return false;
        return (m_fDurationTime >= m_AssStage.m_StageKeepTime);
    }

    public void SetIsSuccess(bool bSuccess)
    {
        m_isSuccess = bSuccess;
    }

    public void InvinStart(float fDelay)
    {
        m_fStartInvintime = Time.time;
        SetIsInvin(true);
        m_fInvinDelay = fDelay;
    }

    public bool IsInvinStartTime()
    {
        if (!m_isInvin) return false;
        
        return (Time.time - m_fStartInvintime < m_fInvinDelay); 
    }

    public void InvinStop()
    {
        SetIsInvin(false);
    }

    private void SetIsInvin(bool bInvin)
    {
        m_isInvin = bInvin;
    }

    public void SetIsDmgMul(bool bDmgMul)
    {
        m_isDmgMul = bDmgMul;
    }

    public void DmgMulStart(float fDelay, float fMulValue)
    {
        m_fStartDmgMulTime = Time.time;
        SetIsDmgMul(true);
        m_fDmgMUlDelay = fDelay;
        m_MulValue = fMulValue;
    }

    public bool IsDmgMulStartTime()
    {
        if (!m_isDmgMul) return false;

        return (Time.time - m_fStartDmgMulTime < m_fDmgMUlDelay);
    }

    public void DmgMulStop()
    {
        SetIsDmgMul(false);
        m_ActorDmg = m_DefultDmg;
    }

    public void DmgMul()
    {
        if (IsDmgMulStartTime())
            m_ActorDmg *= m_MulValue;
    }

    public void StageClear()
    {
        GameScene kGameScene = GameMgr.Inst.m_GameScene;
        m_TurretCount--;

        if (m_TurretCount <= 0)
        {
            kGameScene.m_BattleFSM.SetResultState();
            m_isSuccess = true;
        }
    }

    public string DurationStageTime()
    {
        if (m_AssStage == null) return null;

        return ((int)m_fDurationTime).ToString();
    }

    public void AddScore(int add)
    {
        m_Score += add;
    }

    public string SetScoreString()
    {
        if (m_AssStage == null) return null;

        return m_Score.ToString();
    }
}
