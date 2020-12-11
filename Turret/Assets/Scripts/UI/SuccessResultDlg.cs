using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuccessResultDlg : MonoBehaviour
{
    [SerializeField] Button m_btnRestart = null;
    [SerializeField] Button m_btnNext = null;
    [SerializeField] Button m_btnExit = null;

    [SerializeField] Text m_txtScore = null;
    [SerializeField] Text m_txtClearTime = null;


    private void Start()
    {
        m_btnRestart.onClick.AddListener(OnClicked_Restart);
        m_btnNext.onClick.AddListener(OnClicked_Next);
        m_btnExit.onClick.AddListener(OnClicked_Exit);

        SetTextClearTime();
        SetTextScore();
    }


    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void SetTextScore()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        string sScore = string.Format("Score : {0}점", kGameInfo.SetScoreString());
        m_txtScore.text = sScore;
    }

    private void SetTextClearTime()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        string sClearTime = string.Format("ClearTime : {0}초", kGameInfo.DurationStageTime());
        m_txtClearTime.text = sClearTime;
    }

    private void OnClicked_Restart()
    {
        Show(false);

        GameScene kGameScene = GameMgr.Inst.m_GameScene;
        kGameScene.m_BattleFSM.SetReadyState();
    }

    private void OnClicked_Next()
    {
        Show(false);

        SaveInfo kSaveInfo = GameMgr.Inst.m_SaveInfo;
        GameScene kGameScene = GameMgr.Inst.m_GameScene;

        kSaveInfo.m_LastStage++;
        kGameScene.m_BattleFSM.SetReadyState();
    }

    private void OnClicked_Exit()
    {
        Show(false);

        SceneManager.LoadScene("MainScene");
    }
}
