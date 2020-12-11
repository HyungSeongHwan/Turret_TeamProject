using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTimeBarUI : MonoBehaviour
{
    [SerializeField] Image m_StageTimeBar = null;
    [SerializeField] Text m_txtValue = null;

    private bool m_IsAction = false;


    private void Update()
    {
        Update_StageTimeBar();
    }


    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    public void SetIsAction(bool bAction)
    {
        m_IsAction = bAction;
    }

    private void Update_StageTimeBar()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        GameScene kGameScene = GameMgr.Inst.m_GameScene;
        if (kGameInfo == null) return;

        m_StageTimeBar.fillAmount = kGameInfo.CalculateStageKeepTime();
        m_txtValue.text = kGameInfo.DurationStageKeepTime();

        if (kGameInfo.IsDurationTime() && m_IsAction)
        {
            m_IsAction = false;
            kGameScene.m_BattleFSM.SetResultState();
        }
    }
}
