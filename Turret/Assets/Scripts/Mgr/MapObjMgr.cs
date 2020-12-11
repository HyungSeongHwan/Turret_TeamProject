using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjMgr : MonoBehaviour
{
    public string m_mapName = "";

    private GameObject m_MapObj = null;

    public void Initialize(int nMapID)
    {
        AssetMapObject kAssMapObj = AssetMgr.Inst.GetAssetMapObj(nMapID);

        m_mapName = kAssMapObj.m_MapName;
        DeleteMap();
        LoadMap();
    }

    private void LoadMap()
    {
        GameObject goPref = (GameObject)Resources.Load(m_mapName);
        GameObject goMap =  Instantiate(goPref, transform) as GameObject;
        //goMap.transform.parent = transform;

        m_MapObj = goMap;
    }

    public void DeleteMap()
    {
        Destroy(m_MapObj);
    }   
}
