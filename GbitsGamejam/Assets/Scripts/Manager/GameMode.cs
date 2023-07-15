using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(this);
        }
    }
    void Start()
    {
        //����
        if (instance != this)
        {
            Destroy(instance);
            instance = this;
        }
        //��ȡ�����й�������Ŀ��أ�ע��״̬
        var keys= FindObjectsOfType<SwitchToPass>();
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
        if(gamePlayMode != playMode)
        {
        gamePlayMode = playMode;
            if (playMode == GamePlayMode.UIInteract)
            {
                Time.timeScale = 0;
                if(Player)
                Player.rb.velocity = Vector2.zero;
            }
            else
                Time.timeScale = 1;
                
        }
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

    //���عؿ�
    public void ResetLevel()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPlayerTouchSectionBug(int bugSectionBelongTo)
    {

    }

}
