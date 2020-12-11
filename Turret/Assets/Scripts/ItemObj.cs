using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour
{
    public int m_ID = 0;


    public void Initialize(int nAssId, Vector3 vLocalPos)
    {
        m_ID = nAssId;
        transform.position = vLocalPos;
        Show(true);
    }

    public void Show(bool bShow)
    {
        gameObject.SetActive(bShow);
    }
}
