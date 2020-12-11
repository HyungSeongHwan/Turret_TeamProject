using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class ActorInfo
{
    public string m_Name = "";
    public int m_HP = 0;
    public int m_MaxHP = 0;
    public int m_Atk = 0;
    public int m_AtkDistance = 0;
    public float m_Speed = 0.0f;
    public string m_MissilePrefab = "";

    private bool m_isInvin = false;

    public float m_fStartDashDelay = 0.0f;
    public float m_fDashDelay = 0.0f;
    public bool m_isDashDelay = false;

    public float m_fStartFireDelay = 0.0f;
    public float m_fFireDelay = 0.2f;
    public bool m_isFireDelay = false;

    public int m_AccDamage = 0;


    public void Initialize(int nPlayer)
    {
        AssetPlayer kAssPlayer = AssetMgr.Inst.GetAssetPlayer(nPlayer);

        m_Name = kAssPlayer.m_Name;
        m_HP = kAssPlayer.m_HP;
        m_MaxHP = kAssPlayer.m_HP;
        m_Atk = kAssPlayer.m_Attack;
        m_AtkDistance = kAssPlayer.m_AttackDistance;
        m_Speed = kAssPlayer.m_PlayerSpeed;
        m_MissilePrefab = kAssPlayer.m_BulletPrefabs;

        m_AccDamage = 0;
    }

    public void AddDamage(int nDamage)
    {
        if (!m_isInvin)
        {
            m_AccDamage += nDamage;
            // m_HP -= nDamage;
            if (m_HP <= 0)
                m_HP = 0;
        }
    }

    public void AccDamageInit(int nDamage)
    {
        m_AccDamage = nDamage;
    }
    
    public bool IsDie()
    {
        if (m_HP <= 0)
            return true;

        return false;
    }

    public float SetHPValue()
    {
        return ((float)m_HP / m_MaxHP);
    }

    public string SetHPText()
    {
        return string.Format("{0}", m_HP);
    }

    public void SetIsInvin(bool bInvin)
    {
        m_isInvin = bInvin;
    }

    public void SetIsDashDelay(bool bDashDelay)
    {
        m_isDashDelay = bDashDelay;
    }

    public void DashDelayStart(float fDelay)
    {
        m_fStartDashDelay = Time.time;
        SetIsDashDelay(true);
        m_fDashDelay = fDelay;
    }

    public bool IsDashDelayStartTime()
    {
        if (!m_isDashDelay) return false;

        return (Time.time - m_fStartDashDelay < m_fDashDelay);
    }

    public void DashDelayStop()
    {
        SetIsDashDelay(false);
    }

    public void SetIsFireDelay(bool bFireDelay)
    {
        m_isFireDelay = bFireDelay;
    }
    
    public void FireDelayStart(float Delay)
    {
        m_fStartFireDelay = Time.deltaTime;
        SetIsFireDelay(true);
        m_fFireDelay = Delay;
    }
    
    public bool IsFireDelayStartTime()
    {
        if (!m_isFireDelay) return false;
    
        return (Time.time - m_fStartFireDelay < m_fFireDelay); 
    }
    
    public void FireDelayStop()
    {
        SetIsFireDelay(false);
    }
}
