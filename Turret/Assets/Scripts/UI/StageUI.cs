using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] Text m_txtStage = null;


    public void Initialize()
    {
        SetTextStage();
    }


    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }
    
    public void SetTextStage()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        string sStage = string.Format("Stage : {0}", kGameInfo.m_nStage);
        m_txtStage.text = sStage;
    }
}
