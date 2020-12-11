using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjMgr : MonoBehaviour
{
    public List<ItemObj> m_listItem = null;
    public List<Transform> m_listPos = null;

    private float m_ItemKeepTime = 0.0f;
    private float m_ItemAppearDelay = 0.0f;

    private bool m_isCreateItem = false;


    public void Initialize()
    {
        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;

        m_ItemKeepTime = kGameInfo.m_AssStage.m_ItemKeepTime;
        m_ItemAppearDelay = kGameInfo.m_AssStage.m_ItemAppearDelay;
        IsCreateItem(true);

        StartCoroutine("EnumFunc_SpawnItem");
    }

    public void Init_Result()
    {
        IsCreateItem(false);
        HideAllItem();
    }

    IEnumerator EnumFunc_SpawnItem()
    {
        float fKeepTime = m_ItemKeepTime;
        //float fAppearDelay = m_ItemAppearDelay;
        float fAppearDelay = 2;

        while (m_isCreateItem)
        {
            yield return new WaitForSeconds(fAppearDelay);

            if (!m_isCreateItem)
                break;

            int nAssId = 0;
            int idx = MakeRandomItemObjID(ref nAssId) - 1;

            ItemObj kItem = m_listItem[idx];
            kItem.Initialize(nAssId, MakeRamdomPos());

            yield return new WaitForSeconds(fKeepTime);

            kItem.Show(false);
        }
    }

    public void IsCreateItem(bool bCreateItem)
    {
        m_isCreateItem = bCreateItem;
    }

    private int MakeRandomItemObjID(ref int rAssId)
    {
        int nItemCount = AssetMgr.Inst.GetAssetItemCount();
        int nId = Random.Range(1, nItemCount + 1);
        AssetItem kAssItem = AssetMgr.Inst.GetAssetItem(nId);

        rAssId = nId;
        return kAssItem.m_ItemType;
    }

    public void HideAllItem()
    {
        for (int i = 0; i < m_listItem.Count; i++)
        {
            m_listItem[i].Show(false);
        }
    }

    public void HideItem(GameObject go)
    {
        go.SetActive(false);
    }

    public void SetIsCreateItem(bool bIsCreateItem)
    {
        m_isCreateItem = bIsCreateItem;
    }

    public Vector3 MakeRamdomPos()
    {
        Vector3 vMax = m_listPos[0].position;
        Vector3 vMin = m_listPos[1].position;

        float x = Random.Range(vMin.x, vMax.x);
        float z = Random.Range(vMin.z, vMax.z);

        return new Vector3(x, 1.7f, z);
    }
}
