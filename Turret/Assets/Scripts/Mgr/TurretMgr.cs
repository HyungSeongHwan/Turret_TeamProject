using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class TurretMgr : MonoBehaviour
{
    private GameObject m_PrefabTurret = null;

    public List<Transform> m_listPos = new List<Transform>();
    public Transform m_TurretParents = null;
    private Transform m_TargetTransform = null;
    private Transform m_MissileParent = null;

    public bool m_isSpawn = false;
    public bool m_isBaseSpawn = false;

    public List<Vector3> m_listPosition = new List<Vector3>();

    private List<int> nCheckCollision = new List<int>();

    private GameObject m_PrefabBase = null;


    private void Update()
    {
        //CreateTurretBase();
        SpawnTurret();
    }


    public void Initialize(Transform target, Transform missileParent)
    {
        EnemyInfo kEnemyInfo = GameMgr.Inst.m_GameInfo.m_EnemyInfo;

        m_PrefabTurret = (GameObject)Resources.Load(kEnemyInfo.m_Name);
        m_PrefabBase = (GameObject)Resources.Load("Prefabs/Base_square");
        m_TargetTransform = target;
        m_MissileParent = missileParent;
        m_isBaseSpawn = true;
        m_isSpawn = true;
        DividePos();
    }

    private void SpawnTurret()
    {
        if (m_isSpawn)
        {
            AssetStage kAssStage = GameMgr.Inst.m_GameInfo.m_AssStage;

            for (int i = 0; i < kAssStage.m_TurretCount; i++)
            {
                CreateTurret();
            }

            m_isSpawn = false;
        }
    }

    public void CreateTurretBase()
    {
        if (m_isBaseSpawn)
        {
            for (int i = 0; i < m_listPosition.Count; i++)
            {
                GameObject goBase = Instantiate(m_PrefabBase);
                goBase.transform.position = m_listPosition[i];
            }

            m_isBaseSpawn = false;
        }
    }

    private void CreateTurret()
    {
        GameObject goTurret = Instantiate(m_PrefabTurret);
        goTurret.transform.position = MakeRandPos2();
        goTurret.transform.parent = m_TurretParents;
        Turret csTurret = goTurret.GetComponent<Turret>();

        csTurret.Initialize(m_TargetTransform ,m_MissileParent);
    }

    public Vector3 MakeRandPos()
    {
        Vector3 vMax = m_listPos[0].position;
        Vector3 vMin = m_listPos[1].position;

        float xValue = Random.Range(vMin.x, vMax.x);
        float yValue = Random.Range(0.013f, 2.0f);
        float zValue = Random.Range(vMin.z, vMax.z);

        if (yValue < 1.0f)
            yValue = 0.13f;

        else yValue = 2.0f;

        return new Vector3(xValue, 0.013f, zValue);
    }

    public void DividePos()
    {
        int MaxPosX = 15;
        int MaxPosZ = 20;
        int MinPosX = -20;  
        int MinPosZ = -15;

        while (MaxPosX != MinPosX)
        {
            while (MaxPosZ != MinPosZ)
            {
                MaxPosZ -= 5;
                m_listPosition.Add(new Vector3(MaxPosX, 0, MaxPosZ));
            }
            MaxPosZ = 20;
            MaxPosX -= 5;
        }
    }

    public Vector3 MakeRandPos2()
    {
        int idx = non_overlap();

        nCheckCollision.Add(idx);

        return m_listPosition[idx];
    }

    public int non_overlap() // 재귀함수
    {
        int idx = Random.Range(0, m_listPosition.Count);

        
        for (int i = 0; i < nCheckCollision.Count; i++)
        {
            if (idx == nCheckCollision[i])
            {
              
                return non_overlap();
            }    
                
        }

        return idx;
    }
}
