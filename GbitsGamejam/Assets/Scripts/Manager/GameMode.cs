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

    [HideInInspector]
    public TimeSectionManager timeSectionManager;

    [Header("�ؿ�����")]
    public int TimeSectionNum;
    public Transform DefaultBornTrans;
    //0Ϊ����״̬ 1-N�ֱ�Ϊѡ���˵�N�οɷ���ʱ���


    private static GamePlayMode gamePlayMode;
    public static GamePlayMode GamePlayMode { get => gamePlayMode; }

    public PlayerHJ Player
    {
        get => GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerHJ>(); 
    }

    Dictionary<SwitchToPass, bool> KeysRequiredToPass=new Dictionary<SwitchToPass, bool>();

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
        //��ȡ������ʱ�������
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

    public void OnPlayerTouchSectionBug(int bugSectionBelongTo)
    {

    }
}
