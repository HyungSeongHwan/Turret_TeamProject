using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAsset
{
    public int m_Id = 0;
}

public class AssetStage : CAsset
{
    public int m_MapId = 0;
    public int m_TurretId = 0;
    public int m_StageKeepTime = 0;
    public int m_ItemAppearDelay = 0;
    public int m_ItemKeepTime = 0;
    public int m_ItemAppearRange = 0;
    public int m_TurretCount = 0;
}

public class AssetMapObject : CAsset
{
    public string m_MapName = "";
    public int m_MapSizeX = 0;
    public int m_MapSizeZ = 0;
}

public class AssetEnemy : CAsset
{
    public string m_Name = "";
    public int m_TurretHP = 0;
    public int m_Attack = 0;
    public int m_AttackDistance = 0;
    public float m_FireDelay = 0.0f;
    public float m_BulletSpeed = 0.0f;
    public string m_BulletPrefabs = "";
}

public class AssetItem : CAsset
{
    public int m_ItemType = 0;
    public int m_ItemObjectId = 0;
    public float m_Value = 0.0f;
    public float m_Value2 = 0.0f;
    public int m_FxId = 0;
    public string m_Desc = "";
}

public class AssetItemObject : CAsset
{
    public string m_PrefabName = "";
}

public class AssetEffect : CAsset
{
    public string m_Name = "";
}

public class AssetPlayer : CAsset
{
    public string m_Name = "";
    public int m_HP = 0;
    public int m_Attack = 0;
    public int m_AttackDistance = 0;
    public float m_PlayerSpeed = 0.0f;
    public string m_BulletPrefabs = "";
}

public class AssetMgr
{
    static AssetMgr _instance = null;

    public static AssetMgr Inst
    {
        get
        {
            if (_instance == null)
                _instance = new AssetMgr();
    
            return _instance;
        }
    }

    private AssetMgr()
    {
        IsInstalled = false;
    }


    public bool IsInstalled { get; set; }

    public List<AssetStage> m_AssStage = new List<AssetStage>();
    public List<AssetMapObject> m_AssMapObj = new List<AssetMapObject>();
    public List<AssetEnemy> m_AsssEnemy = new List<AssetEnemy>();
    public List<AssetItem> m_AssItem = new List<AssetItem>();
    public List<AssetItemObject> m_AssItemObj = new List<AssetItemObject>();
    public List<AssetEffect> m_AssEffect = new List<AssetEffect>();
    public List<AssetPlayer> m_AssPlayer = new List<AssetPlayer>();

    
    public void Initialize()
    {
        ParsingStage();
        ParsingMapObj();
        ParsingEnemy();
        ParsingItem();
        ParsingItemObj();
        ParsingEffect();
        ParsingPlayer();
    }

    public void ParsingStage()
    {
        List<string[]> dataList = CSVParser.Load("TableData/stage");

        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetStage kAssStage = new AssetStage();

            kAssStage.m_Id = int.Parse(data[0]);
            kAssStage.m_MapId = int.Parse(data[1]);
            kAssStage.m_TurretId = int.Parse(data[2]);
            kAssStage.m_StageKeepTime = int.Parse(data[3]);
            kAssStage.m_ItemAppearDelay = int.Parse(data[4]);
            kAssStage.m_ItemKeepTime = int.Parse(data[5]);
            kAssStage.m_ItemAppearRange = int.Parse(data[6]);
            kAssStage.m_TurretCount = int.Parse(data[7]);

            m_AssStage.Add(kAssStage);
        }
    }

    public void ParsingMapObj()
    {
        List<string[]> dataList = CSVParser.Load("TableData/mapObject");
        
        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetMapObject kAssMapObj = new AssetMapObject();

            kAssMapObj.m_Id = int.Parse(data[0]);
            kAssMapObj.m_MapName = data[1];
            kAssMapObj.m_MapSizeX = int.Parse(data[2]);
            kAssMapObj.m_MapSizeZ = int.Parse(data[3]);

            m_AssMapObj.Add(kAssMapObj);
        }
    }

    public void ParsingEnemy()
    {
        List<string[]> dataList = CSVParser.Load("TableData/enemy");

        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetEnemy kAssEnemy = new AssetEnemy();

            kAssEnemy.m_Id = int.Parse(data[0]);
            kAssEnemy.m_Name = data[1];
            kAssEnemy.m_TurretHP = int.Parse(data[2]);
            kAssEnemy.m_Attack = int.Parse(data[3]);
            kAssEnemy.m_AttackDistance = int.Parse(data[4]);
            kAssEnemy.m_FireDelay = float.Parse(data[5]);
            kAssEnemy.m_BulletSpeed = float.Parse(data[6]);
            kAssEnemy.m_BulletPrefabs = data[7];

            m_AsssEnemy.Add(kAssEnemy);
        }
    }

    public void ParsingItem()
    {
        List<string[]> dataList = CSVParser.Load("TableData/item");

        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetItem kAssItem = new AssetItem();

            kAssItem.m_Id = int.Parse(data[0]);
            kAssItem.m_ItemType = int.Parse(data[1]);
            kAssItem.m_ItemObjectId = int.Parse(data[2]);
            kAssItem.m_Value = float.Parse(data[3]);
            kAssItem.m_Value2 = float.Parse(data[4]);
            kAssItem.m_FxId = int.Parse(data[5]);
            kAssItem.m_Desc = data[6];

            m_AssItem.Add(kAssItem);
        }
    }

    public void ParsingItemObj()
    {
        List<string[]> dataList = CSVParser.Load("TableData/itemObject");

        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetItemObject kAssItemObj = new AssetItemObject();

            kAssItemObj.m_Id = int.Parse(data[0]);
            kAssItemObj.m_PrefabName = data[1];

            m_AssItemObj.Add(kAssItemObj);
        }
    }

    public void ParsingEffect()
    {
        List<string[]> dataList = CSVParser.Load("TableData/effect");

        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetEffect kAssEffect = new AssetEffect();

            kAssEffect.m_Id = int.Parse(data[0]);
            kAssEffect.m_Name = data[1];

            m_AssEffect.Add(kAssEffect);
        }
    }

    public void ParsingPlayer()
    {
        List<string[]> dataList = CSVParser.Load("TableData/player");

        for (int i = 1; i < dataList.Count; i++)
        {
            string[] data = dataList[i];

            AssetPlayer kAssPlayer = new AssetPlayer();

            kAssPlayer.m_Id = int.Parse(data[0]);
            kAssPlayer.m_Name = data[1];
            kAssPlayer.m_HP = int.Parse(data[2]);
            kAssPlayer.m_Attack = int.Parse(data[3]);
            kAssPlayer.m_AttackDistance = int.Parse(data[4]);
            kAssPlayer.m_PlayerSpeed = float.Parse(data[5]);
            kAssPlayer.m_BulletPrefabs = data[6];

            m_AssPlayer.Add(kAssPlayer);
        }
    }

    public AssetStage GetAssetStage(int nId)
    {
        if (nId > 0 && nId <= m_AssStage.Count)
            return m_AssStage[nId - 1];

        return null;
    }

    public AssetPlayer GetAssetPlayer(int nId)
    {
        if (nId > 0 && nId <= m_AssPlayer.Count)
            return m_AssPlayer[nId - 1];

        return null;
    }

    public AssetEnemy GetAssetEnemy(int nID)
    {
        if (nID > 0 && nID <= m_AsssEnemy.Count)
            return m_AsssEnemy[nID - 1];

        return null;
    }

    public AssetItem GetAssetItem(int nId)
    {
        if (nId > 0 && nId <= m_AssItem.Count)
            return m_AssItem[nId - 1];

        return null;
    }

    public int GetAssetItemCount()
    {
        return m_AssItem.Count;
    }

    public AssetMapObject GetAssetMapObj(int nId)
    {
        if (nId > 0 && nId <= m_AssMapObj.Count)
            return m_AssMapObj[nId - 1];

        return null;
    }
}
