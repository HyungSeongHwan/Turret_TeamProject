using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{

    public enum Type
    {
        eHealing = 1,
        eExplose = 2,
        eDoubleAtk = 3
    }

    public int m_ItemType = 0;
    public float m_ItemValue = 0.0f;


    public void Initialize(int nType, float fValue)
    {
        m_ItemType = nType;
        m_ItemValue = fValue;
    }
}
