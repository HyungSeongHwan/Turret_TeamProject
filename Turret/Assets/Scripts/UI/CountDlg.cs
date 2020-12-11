using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDlg : MonoBehaviour
{
    [SerializeField] Text m_txtCount = null;


    public void Initialize()
    {
        Show(true);
        StartCoroutine("EnumFunc_CountDown", 1.0f);
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }

    IEnumerator EnumFunc_CountDown(float fDelay)
    {
        int nCount = 3;
        while (nCount >= 0)
        {
            string strCount = nCount.ToString();
            if (nCount == 0)
                strCount = "START!";

            m_txtCount.text = strCount;

            nCount--;

            yield return new WaitForSeconds(fDelay);
        }

        Show(false);

        yield return null;
    }
}
