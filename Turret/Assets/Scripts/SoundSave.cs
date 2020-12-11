using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSave
{

    public bool m_isBSM = false;
    public bool m_isSFX = false;

    public void SaveSound()
    {
        int bgmValue = m_isBSM ? 1 : 0;
        int fxmValue = m_isSFX ? 1 : 0;

        PlayerPrefs.SetInt("BGM", bgmValue);
        PlayerPrefs.SetInt("FXM", fxmValue);
    }

    public void LoadSound()
    {
        int bgmValue = PlayerPrefs.GetInt("BGM", 0);
        int sfxValue = PlayerPrefs.GetInt("FXM", 0);

        m_isBSM = bgmValue != 0;
        m_isSFX = sfxValue != 0;
    }

    public void SetSoundInfo(bool isToggleBGM, bool isToggleSFX)
    {
        m_isBSM = isToggleBGM;
        m_isSFX = isToggleSFX;
    }

}
