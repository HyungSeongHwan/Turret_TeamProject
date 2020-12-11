using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * 
 * 파일 저장 정보
 * 
 */
public class SaveInfo
{
    public class SStage
    {
        public int m_Stage = 0;          // 스테이지
        public int m_Score = 0;          // 점수

        public SStage(int stage, int score)
        {
            m_Stage = stage;
            m_Score = score;
        }
        public SStage() { }
    }

    //---------------------------------------------------
    public const string DSAVENAME = "dodge3.data";


    public float m_MaxScore = 0.0f;      // 최고 점수
    public int m_AccumulateScore = 0;    // 누적 점수
    public int m_LastStage = 1;          // Last 스테이지

    public List<SStage> m_listStageScore = new List<SStage>();

    //----------------------------------------------------    

    public void Initialize()
    {
        LoadFile();
    }

    public bool SaveFile()
    {

        FileStream fs = new FileStream(DSAVENAME, FileMode.Create, FileAccess.Write);
        if (fs == null) return false;

        BinaryWriter bw = new BinaryWriter(fs);

        SaveData(bw);

        bw.Close();
        fs.Close();

        return true;
    }

    public bool LoadFile()
    {
        try
        {
            FileStream fs = new FileStream(DSAVENAME, FileMode.Open, FileAccess.Read);
            if (fs == null) return false;

            BinaryReader br = new BinaryReader(fs);

            LoadData(br);

            br.Close();
            fs.Close();

            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Error SaveInfo.LoadFile() - " + e.ToString());
        }
        return false;
    }

    public void SaveData(BinaryWriter bw)
    {
        bw.Write(m_MaxScore);
        bw.Write(m_LastStage);
        bw.Write(m_AccumulateScore);
        bw.Write(m_listStageScore.Count);
        for (int i = 0; i < m_listStageScore.Count; i++)
        {
            SStage kStage = m_listStageScore[i];
            bw.Write(kStage.m_Stage);
            bw.Write(kStage.m_Score);
        }
    }

    public void LoadData(BinaryReader br)
    {
        m_listStageScore.Clear();

        m_MaxScore = br.ReadSingle();
        m_LastStage = br.ReadInt32();
        m_AccumulateScore = br.ReadInt32();
        int nCount = br.ReadInt32();
        for (int i = 0; i < nCount; i++)
        {
            SStage kStage = new SStage();
            kStage.m_Stage = br.ReadInt32();
            kStage.m_Score = br.ReadInt32();

            m_listStageScore.Add(kStage);
        }
    }


    public void SetStage(int nStage)
    {
        m_LastStage = nStage;
    }

    private void SetMaxScore(float fScore)
    {
        m_MaxScore = (fScore > m_MaxScore) ? fScore : m_MaxScore;
    }

    public void AddAccumulateScore(int nScore)
    {
        m_AccumulateScore += nScore;
    }

    public void AddStageScore(int nStage, int nScore)
    {
        m_listStageScore.Add(new SStage(nStage, nStage));
    }

    public void SetStageScore(int nStage, int nScore)
    {
        SetMaxScore(nScore);
        AddAccumulateScore(nScore);

        if (nStage > m_listStageScore.Count)
        {
            SStage kStage = new SStage(nStage, nScore);
            m_listStageScore.Add(kStage);
        }
        else
        {
            SStage kStage = m_listStageScore[nStage - 1];
            kStage.m_Score = nScore;

            if (kStage.m_Stage != nStage)
            {
                Debug.LogError("kStage.m_Stage != nScore   !!!!!");
            }
        }
    }
}
