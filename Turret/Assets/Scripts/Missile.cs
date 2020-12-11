using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector3 m_Direction = Vector3.zero;

    public float m_MoveSpeed = 0.0f;


    private void Update()
    {
        MoveMissile();
    }


    public void InitializeTurret(Vector3 vTargetPos)
    {
        m_Direction = vTargetPos - transform.position;
        m_Direction.Normalize();
        transform.LookAt(vTargetPos);

        EnemyInfo kEnemyInfo = GameMgr.Inst.m_GameInfo.m_EnemyInfo;
        m_MoveSpeed = kEnemyInfo.m_MissileSpeed;
    }

    public void InitializePlayer(Vector3 vTargetPos)
    {
        m_Direction = vTargetPos - transform.position;
        m_Direction.Normalize();
        transform.LookAt(vTargetPos);

        EnemyInfo kEnemyInfo = GameMgr.Inst.m_GameInfo.m_EnemyInfo;
        m_MoveSpeed = kEnemyInfo.m_MissileSpeed;
    }

    private void MoveMissile()
    {
        transform.Translate(m_Direction * m_MoveSpeed * Time.deltaTime, Space.World);
    }

    public void DestroyMissile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            DestroyMissile();
        }

        if (other.tag == "Player" && this.tag == "Missile")
        {
            DestroyMissile();
        }

        if (other.tag == "Turret" && this.tag == "Missile2")
        {
            DestroyMissile();
        }
    }
}
