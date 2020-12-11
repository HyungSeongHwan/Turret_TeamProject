using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class OptionDlg : MonoBehaviour
{
    [SerializeField] Toggle m_toggleBGM = null;
    [SerializeField] Toggle m_toggleSFX = null;

    [SerializeField] Button m_btnSave = null;
    [SerializeField] Button m_btnExit = null;

    [SerializeField] MainScene m_MainUI = null;

    private SoundSave m_SoundSave = new SoundSave();


    private void Start()
    {
        m_btnSave.onClick.AddListener(OnClicked_btnSave);
        m_btnExit.onClick.AddListener(OnClicked_btnExit);
    }


    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    private void OnClicked_btnSave()
    {
        m_SoundSave.SetSoundInfo(m_toggleBGM.isOn, m_toggleSFX.isOn);
        m_SoundSave.SaveSound();
    }

    private void OnClicked_btnExit()
    {
        Show(false);
        m_MainUI.Show(true);
    }
}
