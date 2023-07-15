using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GamePlayMode
{
    Play,Replay,UIInteract
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


    TimeSectionManager timeSectionManager;

    [Header("�ؿ�����")]
    public int TimeSectionNum;
    //0Ϊ����״̬ 1-N�ֱ�Ϊѡ���˵�N�οɷ���ʱ���


    private static GamePlayMode gamePlayMode;
    public static GamePlayMode GamePlayMode { get => gamePlayMode; }

    Dictionary<SwitchToPass, bool> KeysRequiredToPass;

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
        //��ȡ������0ʱ�������
        timeSectionManager=FindObjectOfType<TimeSectionManager>();
        if (timeSectionManager == null)
            print("������ȱ��TimeSectionManager!");

    }

    void Update()
    {
        
    }

    public void SetGameMode(GamePlayMode playMode)
    {
        gamePlayMode = playMode;
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
        return timeSectionManager.GetNowLogicBugNum() == 0&&keysFinished;
    }

    //���ù����ж�
    public void ResetKeysState()
    {
        foreach (var key in KeysRequiredToPass)
            KeysRequiredToPass[key.Key] = false;
    }
}
