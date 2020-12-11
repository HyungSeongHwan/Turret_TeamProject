using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GameUI : MonoBehaviour
{
    [SerializeField] PlayerController m_Player = null;
    //[SerializeField] Turret[] m_Turrets = null;
    [SerializeField] Transform m_TurretMissileParent = null;
    [SerializeField] Transform m_PlayerMissileParent = null;
    [SerializeField] TurretMgr m_TurretMgr = null;
    [SerializeField] Transform m_TurretParent = null;
    [SerializeField] ItemObjMgr m_ItemObjMgr = null;
    [SerializeField] MapObjMgr m_MapObjMgr = null;
    [SerializeField] SphereBoss m_SphereBoss = null;

    private bool m_PlayADAnim = false;


    public void Init_GameEnter()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        m_MapObjMgr.Initialize(kGameInfo.m_AssStage.m_MapId);
        m_Player.Initialize(m_PlayerMissileParent);
        m_TurretMgr.Initialize(m_Player.transform, m_TurretMissileParent);
        m_Player.OnPlayerCollision = PlayerCollision_Enter;
        m_ItemObjMgr.Initialize();
        m_SphereBoss.Initialize(m_Player.transform);

        SetIsPlayADAnim(true);
    }

    public void Init_ResultEnter()
    {
        m_Player.StopPlayerMove();

        Turret[] m_Turrets = m_TurretParent.GetComponentsInChildren<Turret>();
        for (int i = 0; i < m_Turrets.Length; i++)
        {
            m_Turrets[i].SetIsFire(false);
        }

        m_ItemObjMgr.Init_Result();

        DestroyAllMissile();
        DestroyAllTurret();
    }

    private void PlayerCollision_Enter(Collider other)
    {
        if (other.tag == "Missile")
        {
            Collision_Missile();
        }

        if (other.tag == "Item")
        {
            Collision_Item(other);
        }

        if (other.tag == "Turret")
        {
            Debug.Log("asd");
            m_Player.m_Rigidbody.velocity = new Vector3(0, 0, 0);
            //m_Player.m_isMove = false;
        }

        if (other.tag == "SphereBoss")
        {
            Debug.Log("coll");
        }
    }

    private void Collision_Missile()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;

        if (!kGameInfo.IsInvinStartTime())
        {
            kGameInfo.AddDamagePlayer();

            if (m_PlayADAnim)
            {
                StartCoroutine("EnumFunc_AddDamageAnim", 0.00001f);
                SetIsPlayADAnim(false);
            }
        }

        if (!kGameInfo.IsInvinStartTime())
            kGameInfo.InvinStop();
    }

    private void Collision_Item(Collider other)
    {
        ItemObj kItemObj = other.gameObject.GetComponent<ItemObj>();
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;
        AssetItem kAssItem = AssetMgr.Inst.GetAssetItem(kItemObj.m_ID);
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        if (kAssItem.m_ItemType == (int)ItemInfo.Type.eHealing)
        {
            m_ItemObjMgr.HideItem(other.gameObject);
            kActorInfo.m_HP += (int)kAssItem.m_Value;

            if (kActorInfo.m_HP > kActorInfo.m_MaxHP)
                kActorInfo.m_HP = kActorInfo.m_MaxHP;
        }

        else if (kAssItem.m_ItemType == (int)ItemInfo.Type.eExplose)
        {
            m_ItemObjMgr.HideItem(other.gameObject);
            DestroyTurMissile();
            kGameInfo.InvinStart(kAssItem.m_Value);
            Debug.Log("d");
        }

        else if (kAssItem.m_ItemType == (int)ItemInfo.Type.eDoubleAtk)
        {
            m_ItemObjMgr.HideItem(other.gameObject);
            kGameInfo.DmgMulStart(kAssItem.m_Value2, kAssItem.m_Value);
            kGameInfo.DmgMul();
        }
    }

    IEnumerator EnumFunc_AddDamageAnim(float Delay)
    {
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;
        EnemyInfo kEnemyInfo = GameMgr.Inst.m_GameInfo.m_EnemyInfo;
        kActorInfo.AccDamageInit(kEnemyInfo.m_Attack);

        Debug.Log("adssa");

        int curDamage = 0;
        while (curDamage < kActorInfo.m_AccDamage)
        {
            kActorInfo.m_HP -= 1;
            curDamage++;

            if (kActorInfo.m_HP <= 0)
            {
                m_Player.CheckPlayerDie();
                break;
            }

            yield return new WaitForSeconds(Delay);
        }

        SetIsPlayADAnim(true);
        yield return null;
    }

    public void SetIsPlayADAnim(bool isPlayADAnim)
    {
        m_PlayADAnim = isPlayADAnim;
    }

    private void DestroyAllMissile()
    {
        DestroyTurMissile();
        DestroyPlaMissile();
    }

    private void DestroyTurMissile()
    {
        Missile[] kMissile = m_TurretMissileParent.GetComponentsInChildren<Missile>();

        for (int i = 0; i < kMissile.Length; i++)
        {
            Destroy(kMissile[i].gameObject);
        }
    }

    private void DestroyPlaMissile()
    {
        Missile[] kMissile = m_PlayerMissileParent.GetComponentsInChildren<Missile>();

        for (int i = 0; i < kMissile.Length; i++)
        {
            Destroy(kMissile[i].gameObject);
        }
    }

    private void DestroyAllTurret()
    {
        Turret[] kTurret = m_TurretParent.GetComponentsInChildren<Turret>();

        for (int i = 0; i < kTurret.Length; i++)
        {
            Destroy(kTurret[i].gameObject);
        }
    }
}
