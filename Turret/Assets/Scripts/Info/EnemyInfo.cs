using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo
{
    public string m_Name = "";
    public float m_TurretHP = 0;
    public int m_Attack = 0;
    public int m_AtkDistance = 0;
    public float m_FireDelay = 0.0f;
    public float m_MissileSpeed = 0.0f;
    public string m_MissilePrefab = "";

    public void Initialize(int nItem)
    {
        AssetEnemy kAssEnemy = AssetMgr.Inst.GetAssetEnemy(nItem);

        m_Name = kAssEnemy.m_Name;
        m_TurretHP = kAssEnemy.m_TurretHP;
        m_Attack = kAssEnemy.m_Attack;
        m_AtkDistance = kAssEnemy.m_AttackDistance;
        m_FireDelay = kAssEnemy.m_FireDelay;
        m_MissileSpeed = kAssEnemy.m_BulletSpeed;
        m_MissilePrefab = kAssEnemy.m_BulletPrefabs;
    }

    public void AddDamage(float nDamage)
    {
        m_TurretHP -= nDamage;

        if (m_TurretHP <= 0)
            m_TurretHP = 0;
    }

    public bool IsTurretDie()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        int nAddScore = 100;

        if (m_TurretHP <= 0)
        {
            kGameInfo.StageClear();
            kGameInfo.AddScore(nAddScore);

            return true;
        }
            
        return false;
    }
}