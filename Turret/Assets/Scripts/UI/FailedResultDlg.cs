using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailedResultDlg : MonoBehaviour
{
    [SerializeField] Button m_btnRestart = null;
    [SerializeField] Button m_btnExit = null;


    private void Start()
    {
        m_btnRestart.onClick.AddListener(OnClicked_Restart);
        m_btnExit.onClick.AddListener(OnClicked_Exit);
    }


    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void OnClicked_Restart()
    {
        Show(false);
        GameScene kGameScene = GameMgr.Inst.m_GameScene;

        kGameScene.m_BattleFSM.SetReadyState();
    }

    private void OnClicked_Exit()
    {
        Show(false);
        SceneManager.LoadScene("MainScene");
    }
}
