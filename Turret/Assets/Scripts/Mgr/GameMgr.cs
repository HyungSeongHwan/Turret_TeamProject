using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr
{
    static GameMgr _instance = null;

    public static GameMgr Inst
    {
        get
        {
            if (_instance == null)
                _instance = new GameMgr();

            return _instance;
        }
    }



    public GameInfo m_GameInfo = new GameInfo();
    public SaveInfo m_SaveInfo = new SaveInfo();
    public GameScene m_GameScene = null;


    public void Initialize()
    {
        Application.runInBackground = true;
    }

    public void OnUpdate(float fElasedTime)
    {
        m_GameInfo.OnUpdate(fElasedTime);
    }

    public void SetGameScene(GameScene kGameScene)
    {
        m_GameScene = kGameScene;
    }

    public GameScene GetGameScene()
    {
        return m_GameScene;
    }

    public void InitStart()
    {
        m_GameInfo.Initilaize();
    }

    public void SaveFile()
    {
        m_SaveInfo.SaveFile();
    }

    public void LoadFile()
    {
        m_SaveInfo.LoadFile();
    }
}
