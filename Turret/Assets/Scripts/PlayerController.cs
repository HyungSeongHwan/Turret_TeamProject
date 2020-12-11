using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform m_FirePosition = null;
    [SerializeField] Transform m_TargetPosition = null;

    public Rigidbody m_Rigidbody = null;
    private GameObject m_PrefabMissile = null;
    private Transform m_MissileParent = null;

    public float m_MoveSpeed = 0.0f;
    public float m_JumpPower = 5.0f;

    public int m_nCountFire = 0;

    public float m_nChargeCount = 0;
    public float m_nChargeMax = 1.5f;

    public bool m_isMove = false;
    public bool m_isFire = false;
    public bool m_isBindly = false;
    public bool m_isJump = true;

    public Action<Collider> OnPlayerCollision = null;


    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        FireMissile();
        Dash();
        JumpPlayer();
        LockUp();   
    }


    public void Initialize(Transform missileParent)
    {
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;

        m_Rigidbody = GetComponent<Rigidbody>();
        m_PrefabMissile = (GameObject)Resources.Load(kActorInfo.m_MissilePrefab);
        m_MissileParent = missileParent;
        m_MoveSpeed = (kActorInfo.m_Speed * 15);
        m_nCountFire = 0;

        transform.position = new Vector3(0, 1.7f, 0);
        m_JumpPower = 8.0f;

        SetIsMove(true);
        SetIsFire(true);
        SetIsBindly(true);
        m_isJump = true;
    }

    public void SetIsMove(bool bMove)
    {
        m_isMove = bMove;
    }

    public void SetIsFire(bool bFire)
    {
        m_isFire = bFire;
    }

    public void SetIsBindly(bool bBindly)
    {
        m_isBindly = bBindly;
    }

    public void SetIsJump(bool bJump)
    {
        m_isJump = bJump;
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void MovePlayer()
    {
        if (m_isMove)
        {
            float xValue = Input.GetAxis("Horizontal");
            float zValue = Input.GetAxis("Vertical");

            xValue *= m_MoveSpeed * Time.deltaTime;
            zValue *= m_MoveSpeed * Time.deltaTime;

            transform.Translate(new Vector3(xValue, 0, zValue));
        }
    }

    private void JumpPlayer()
    {
        if (m_Rigidbody == null) return;

        if (m_isJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("j");
                m_Rigidbody.AddForce(Vector3.up* m_JumpPower, ForceMode.Impulse);
           }
        }
    }

    private void LockUp()
    {
        if (m_Rigidbody == null) return;

        if (transform.position.y > 1.8f)
        {
            m_Rigidbody.useGravity = true;

            SetIsJump(false);
        }

        else if (transform.position.y <= 1.8f)
        {
            m_Rigidbody.useGravity = false;
            transform.position = new Vector3(transform.position.x, 1.8f, transform.position.z);

            SetIsJump(true);
        }
    }

    private void RotatePlayer()
    {
        if (m_isMove)
        {
            if (Input.GetKey(KeyCode.Q))
                transform.Rotate(0, -1, 0);

            else if (Input.GetKey(KeyCode.E))
                transform.Rotate(0, 1, 0);

            // 위아래 각도조절 - 기각
            //if (Input.GetKey(KeyCode.Space))
            //    transform.Rotate(1, 0, 0);
            //
            //else if (Input.GetKey(KeyCode.LeftControl))
            //    transform.Rotate(-1, 0, 0);
        }
    }



    private void CreateMissile()
    {
        GameObject goMissile = Instantiate(m_PrefabMissile);
        goMissile.transform.position = m_FirePosition.transform.position;
        goMissile.transform.parent = m_MissileParent;
        Missile csMissile = goMissile.GetComponent<Missile>();

        csMissile.InitializePlayer(m_TargetPosition.transform.position);
    }

    private void FireMissile()
    {
        if (m_isBindly)
            Fire_Blindly();

        else
            Fire_Charge();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnPlayerCollision != null)
            OnPlayerCollision(other);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Turret")
            m_isMove = true;
    }

    public void StopPlayerMove()
    {
        SetIsFire(false);
        SetIsMove(false);
        m_Rigidbody.velocity = new Vector3(0, 0, 0);
    }

    public void CheckPlayerDie()
    {
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;
        GameScene kGameScene = GameMgr.Inst.m_GameScene;

        if (kActorInfo.IsDie())
            kGameScene.m_BattleFSM.SetResultState();
    }

    private void Dash()
    {
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        if (kActorInfo == null) return;

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if (!kActorInfo.IsDashDelayStartTime())
            {
                kActorInfo.DashDelayStart(4);
                kGameInfo.InvinStart(1);
                transform.Translate(new Vector3(0, 0, 2.5f));
            }
        }

        if (!kActorInfo.IsDashDelayStartTime())
            kActorInfo.DashDelayStop();

    }

    private void Fire_Blindly()
    {
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;

        if (!m_isFire)
            return;

        if (Input.GetMouseButton(0) && Time.time > kActorInfo.m_fStartFireDelay)
        {
            kActorInfo.m_fStartFireDelay = Time.time + kActorInfo.m_fFireDelay;
            CreateMissile();
            ++m_nCountFire;
        }

        if (m_nCountFire >= 20)
        {
            StartCoroutine("EnumFunc_Load", 3);
            m_nCountFire = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("EnumFunc_Load", 3);
            m_nCountFire = 0;
        }

    }

    private void Fire_Charge()
    {
        if (!m_isFire)
            return;

        if (Input.GetMouseButton(0))
        {
            m_nChargeCount += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (m_nChargeCount >= m_nChargeMax)
                CreateMissile();

            m_nChargeCount = 0;
        }

    }

    IEnumerator EnumFunc_Load(float Time)
    {
        SetIsFire(false);

        yield return new WaitForSeconds(Time);

        SetIsFire(true);

        yield return null;
    }
}
