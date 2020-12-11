using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    public CountDlg m_CountDlg = null;
    public StageUI m_StageUI = null;
    public HPBarUI m_HpBarUI = null;
    public StageTimeBarUI m_StageTimeBarUI = null;
    public SuccessResultDlg m_SuccessResualtDlg = null;
    public FailedResultDlg m_FailedResultDlg = null;


    public void Init_ReadyEnter()
    {
        m_CountDlg.Initialize();
    }

    public void Init_GameEnter()
    {
        m_StageUI.Show(true);
        m_HpBarUI.Show(true);
        m_StageTimeBarUI.Show(true);

        m_StageUI.Initialize();

        m_StageTimeBarUI.SetIsAction(true);
    }

    public void Init_ResultEnter()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;

        m_StageUI.Show(false);
        m_HpBarUI.Show(false);
        m_StageTimeBarUI.Show(false);

        if (kActorInfo.IsDie())
            m_FailedResultDlg.Show(true);

        else m_SuccessResualtDlg.Show(true);
    }
}
