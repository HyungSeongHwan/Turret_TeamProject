using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] Image m_HPBar = null;
    [SerializeField] Text m_txtHPValue = null;


    private void Update()
    {
        Update_HPBar();
    }


    public void Update_HPBar()
    {
        ActorInfo kActorInfo = GameMgr.Inst.m_GameInfo.m_ActorInfo;
        if (kActorInfo == null) return;

        m_HPBar.fillAmount = kActorInfo.SetHPValue();
        m_txtHPValue.text = kActorInfo.SetHPText();
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }
}
