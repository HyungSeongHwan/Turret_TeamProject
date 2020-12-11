using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject m_FirePosition = null;
    [SerializeField] GameObject m_TurretTop = null;

    private GameObject m_PrefabMissile = null;

    private Transform m_BulletParent = null;
    private Transform m_Target = null;

    public float m_TurretHP = 0;

    public bool m_isFire = false;


    private void Update()
    {
        RotatieTurret();
        
    }


    public void Initialize(Transform kTarget, Transform kMissileParent)
    {
        m_Target = kTarget;
        EnemyInfo kEnemyInfo = GameMgr.Inst.m_GameInfo.m_EnemyInfo;
        m_PrefabMissile = (GameObject)Resources.Load(kEnemyInfo.m_MissilePrefab);
        m_BulletParent = kMissileParent;
        m_TurretHP = kEnemyInfo.m_TurretHP;
        Show(true);

        SetIsFire(true);
        FireMissile();
    }

    public void SetIsFire(bool bFire)
    {
        m_isFire = bFire;
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void RotatieTurret()
    {
        if (m_isFire)
            m_TurretTop.transform.LookAt(m_Target.transform.position);
    }

    public void FireMissile()
    {
        EnemyInfo kEnemyInfo = GameMgr.Inst.m_GameInfo.m_EnemyInfo;
        if (kEnemyInfo.m_FireDelay == 0.0f)
            return;

        StartCoroutine("EnumFunc_AutoFire", kEnemyInfo.m_FireDelay);
    }

    private void CreateBullet()
    {
        GameObject goMissile = Instantiate(m_PrefabMissile);
        goMissile.transform.position = m_FirePosition.transform.position;
        goMissile.transform.parent = m_BulletParent;
        Missile csMissile = goMissile.GetComponent<Missile>();

        csMissile.InitializeTurret(m_Target.transform.position);
    }

    IEnumerator EnumFunc_AutoFire(float fDelay)
    {
        while (m_isFire)
        {
            if (CheckAttackRange(10, m_Target))
                CreateBullet();

            yield return new WaitForSeconds(fDelay);
        }

        yield return null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile2")
        {
            Collision_Missile();
        }
    }

    private void Collision_Missile()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        //kGameInfo.AddDamageTurret();
        m_TurretHP -= kGameInfo.m_ActorDmg;

        if (m_TurretHP <= 0)
            m_TurretHP = 0;

        CheckTurretDie();
    }

    private void CheckTurretDie()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        int nAddScore = 100;

        if (m_TurretHP <= 0)
        {

            kGameInfo.StageClear();
            kGameInfo.AddScore(nAddScore);

            SetIsFire(false);
            Show(false);
        }
    }

    // 공격 범위안에 타겟이 들어왔는지 체크 하기
    // fRange  : 공격 범위 거리
    // kTarget : 타겟  
    // 주의) 공격 사정거리를 멤버변수로 두고 사용하면 편하다.
    public bool CheckAttackRange(float fRange, Transform kTarget)
    {
        float fDist = Vector3.Distance(kTarget.position, transform.position);
        if (kTarget.position.y < transform.position.y)
            return false;
        return fRange > fDist ? true : false;
    }
}
