using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GamePlayMode
{
    Play,Replay
}

public class GameMode : MonoBehaviour
{
    private static GameMode instance;
    public static GameMode Instance
    {
        get
        {
            if (instance == null)
                instance = new GameMode();
            return instance;
        }
    }

    public int TimeSectionNum;
    //0为自由状态 1-N分别为选中了第N段可分配时间段
    private int nowTimeSection;
    public int NowTimeSection { get => nowTimeSection; }

    public static GamePlayMode gamePlayMode;

    public Dictionary<SwitchToPass, bool> KeysRequiredToPass;
    private int NowLogicBugsNum;

    void Start()
    {
        //单例
        if (instance != this)
        {
            Destroy(instance);
            instance = this;
        }
        //获取场景中过关所需的开关，注册状态
       var keys= (SwitchToPass[])FindObjectsOfType(typeof(SwitchToPass));
        foreach (var key in keys)
            KeysRequiredToPass.Add(key, false);

    }

    void Update()
    {
        
    }

    void SelectTimeSectionIndex(int index)
    {
        if (index < 0||index>TimeSectionNum) return;
        nowTimeSection = index;
    }
    //检查是否通关
    bool CheckIfPass()
    {
        bool keysFinished=false;
        foreach (var key in KeysRequiredToPass)
            if (!key.Value)
            {
                keysFinished = false;
                break;
            }
        return NowLogicBugsNum == 0&&keysFinished;
    }

    //重置过关判定
    public void ResetKeysState()
    {
        foreach (var key in KeysRequiredToPass)
            KeysRequiredToPass[key.Key] = false;
        NowLogicBugsNum++;
    }
}
