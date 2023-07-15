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
    //0Ϊ����״̬ 1-N�ֱ�Ϊѡ���˵�N�οɷ���ʱ���
    private int nowTimeSection;
    public int NowTimeSection { get => nowTimeSection; }

    public static GamePlayMode gamePlayMode;

    public Dictionary<SwitchToPass, bool> KeysRequiredToPass;
    private int NowLogicBugsNum;

    void Start()
    {
        //����
        if (instance != this)
        {
            Destroy(instance);
            instance = this;
        }
        //��ȡ�����й�������Ŀ��أ�ע��״̬
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
    //����Ƿ�ͨ��
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

    //���ù����ж�
    public void ResetKeysState()
    {
        foreach (var key in KeysRequiredToPass)
            KeysRequiredToPass[key.Key] = false;
        NowLogicBugsNum++;
    }
}
