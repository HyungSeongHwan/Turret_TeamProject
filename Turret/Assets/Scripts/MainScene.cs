using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    [SerializeField] GameObject m_MainUI = null;
    [SerializeField] Button m_btnStart = null;
    [SerializeField] Button m_btnOption = null;
    [SerializeField] OptionDlg m_OptionDlg = null;


    private void Start()
    {
        m_btnStart.onClick.AddListener(OnClicked_btnStart);
        m_btnOption.onClick.AddListener(OnClicked_btnOption);
    }


    public void Show(bool bShow)
    {
        m_MainUI.SetActive(bShow);
    }

    private void OnClicked_btnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnClicked_btnOption()
    {
        Show(false);
        m_OptionDlg.Show(true);
    }
}
