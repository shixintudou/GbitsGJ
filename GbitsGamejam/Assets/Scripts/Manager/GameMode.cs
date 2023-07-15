using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GamePlayMode
{
    Play, Replay, UIInteract
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
    [HideInInspector]
    public LUIManager m_UIManager;

    [Header("关卡配置")]
    public int TimeSectionNum;
    public Vector3 DefaultBornTrans;
    //0为自由状态 1-N分别为选中了第N段可分配时间段


    private static GamePlayMode gamePlayMode;
    public static GamePlayMode GamePlayMode { get => gamePlayMode; }

    public PlayerHJ Player
    {
        get => GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerHJ>();
    }

    List<LaganController> laganRequiredToPass = new List<LaganController>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    void Start()
    {
        var aa = ResoucesManager.Instance.Resouces["LogicBug"];
        //单例
        if (instance != this)
        {
            Destroy(instance);
            instance = this;
        }
        //获取场景中过关所需的拉杆，注册状态
        var keys = FindObjectsOfType<LaganController>();
        foreach (var key in keys)
            laganRequiredToPass.Add(key);

        //获取场景中时间轴组件
        timeSectionManager = FindObjectOfType<TimeSectionManager>();
        if (timeSectionManager == null)
            print("场景中缺少TimeSectionManager!");

        m_UIManager = GetComponent<LUIManager>();
        if (m_UIManager == null)
            m_UIManager = gameObject.AddComponent<LUIManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return))
            timeSectionManager.FinishThisTimeSection();
    }

    public void SetGameMode(GamePlayMode playMode)
    {
        if (gamePlayMode != playMode)
        {
            gamePlayMode = playMode;
            if (playMode == GamePlayMode.UIInteract)
            {
                Time.timeScale = 0;
                if (Player)
                    Player.rb.velocity = Vector2.zero;
            }
            else
                Time.timeScale = 1;

        }
    }

    //检查是否通关
    public bool CheckIfPass()
    {
        bool laganFinished = true;
        foreach (var lagan in laganRequiredToPass)
            if (!lagan.bPicked)
            {
                laganFinished = false;
                break;
            }
        bool ifPass = timeSectionManager.GetNowLogicBugNum() == 0 && laganFinished;
        if (ifPass)
            OnSuccessPassed();
        return ifPass;
    }
    void OnSuccessPassed()
    {
        print("过关成功！");
        SetGameMode(GamePlayMode.Replay);
        m_UIManager.CanvasFadeInAndOut();
        StartCoroutine(ReplayCoroutine());
    }
    IEnumerator ReplayCoroutine()
    {
        yield return 0.3f;
        ReplayManager.instance.StartReplay(DefaultBornTrans);
    }

    //重载关卡
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ReloadLevel()
    {
        yield return 0.3f;

    }
    /// <summary>
    /// 当玩家接触到逻辑漏洞
    /// </summary>
    /// <param name="bugSectionBelongTo"></param>
    /// <param name="Bug_obj"></param>
    public void OnPlayerTouchSectionBug(int bugSectionBelongTo, LogicBug Bug_obj)
    {
        //是下一段的逻辑漏洞
        if ((timeSectionManager.NowTimeSection + 1) == bugSectionBelongTo)
        {
            timeSectionManager.OnClearABug(Bug_obj.gameObject);
            Destroy(Bug_obj.gameObject);
        }
    }
    public void SetPlayerPos(Vector3 postion)
    {
        PlayerHJ player = GameMode.Instance.Player;
        if (player)
            player.transform.position = postion;
    }

    public void SwitchCursorImage(bool IfHandmode)
    {
    }
}
