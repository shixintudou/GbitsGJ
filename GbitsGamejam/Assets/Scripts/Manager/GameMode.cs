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

    [Header("关卡配置")]
    public int TimeSectionNum;
    public Transform DefaultBornTrans;
    //0为自由状态 1-N分别为选中了第N段可分配时间段


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
        //单例
        if (instance != this)
        {
            Destroy(instance);
            instance = this;
        }
        //获取场景中过关所需的开关，注册状态
        var keys= FindObjectsOfType<SwitchToPass>();
        foreach (var key in keys)
            KeysRequiredToPass.Add(key, false);
        //获取场景中时间轴组件
        timeSectionManager=FindObjectOfType<TimeSectionManager>();
        if (timeSectionManager == null)
            print("场景中缺少TimeSectionManager!");

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
        return timeSectionManager.GetNowLogicBugNum() == 0&&keysFinished;
    }

    //重载关卡
    public void ResetLevel()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPlayerTouchSectionBug(int bugSectionBelongTo)
    {

    }

}
