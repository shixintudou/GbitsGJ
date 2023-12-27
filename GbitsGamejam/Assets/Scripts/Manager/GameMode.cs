using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GamePlayMode
{
    Play, Replay, UIInteract, Act
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

    [Header("过关数据")]
    bool ifPass;
    public bool IfPass { get => ifPass; }
    bool IfStartLevel;
    [HideInInspector]
    public int playerDeathSection = -1;
    [HideInInspector]
    public bool IfTouchTransport;
    public static Dictionary<int, bool> IfFirstEnterMap;

    [Header("关卡配置")]
    public int TimeSectionNum;
    [HideInInspector]
    public Vector3 DefaultBornPos;
    string playerPrefabName = "Player";


    private static GamePlayMode gamePlayMode;
    public static GamePlayMode GamePlayMode { get => gamePlayMode; }

    private PlayerHJ m_player;
    public PlayerHJ Player
    {
        get
        {
            if (m_player == null)
            {
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                m_player = playerObject ? playerObject.GetComponent<PlayerHJ>() : null;
            }
            return m_player;
        }
    }

    public delegate void Func_NoParam();

    private void Awake()
    {
        if (instance == null)
        {
            print("初始化GameModeManager");
            if (IfFirstEnterMap == null || IfFirstEnterMap.Count == 0)
            {
                //初始化是否首次进入场景的记录
                IfFirstEnterMap = new Dictionary<int, bool>();
                int sceneCount = SceneManager.sceneCountInBuildSettings;
                for (int i = 0; i < sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneByBuildIndex(i);
                    if (scene != null)
                    {
                        IfFirstEnterMap.Add(i, false);
                    }
                }
            }
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    void Start()
    {
        //单例
        if (instance != this)
        {
            instance = this;
            Destroy(instance);
        }
        //记录玩家出生位置
        DefaultBornPos = Player.transform.position;
        playerPrefabName = Player.name.Replace("Clone", "");

        //临时设置
        if (ReplayManager.instance != null)
        {
            ReplayManager.instance.IsReadyForLoadNextScene = false;
            ReplayManager.instance.SetDataNum(GameMode.Instance.TimeSectionNum);
        }

        //获取场景中时间轴组件
        timeSectionManager = FindObjectOfType<TimeSectionManager>();
        if (timeSectionManager == null)
            print("场景中缺少TimeSectionManager!");

        //获取/生成uimanager组件
        m_UIManager = GetComponent<LUIManager>();
        if (m_UIManager == null)
            m_UIManager = gameObject.AddComponent<LUIManager>();

        //准备开始关卡
        CheckShowLevelIntroduce();
    }

    /// <summary>
    /// 检查关卡前置流程
    /// </summary>
    /// <param name="IfLoadNextLevel"></param>
    /// <returns></returns>
    public bool CheckShowLevelIntroduce(bool IfLoadNextLevel = false)
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (!IfFirstEnterMap[curSceneIndex] && LevelIntroducer.Instance != null && IsCurrentSceneLevel())
        {
            print("关卡level" + (curSceneIndex + 1) + "初见");
            IfFirstEnterMap[curSceneIndex] = true;
            //显示介绍
            LevelIntroducer.Instance.SetIntroduceImageAndEnable(SceneManager.GetActiveScene().buildIndex, StartLevel);
            return true;
        }
        StartLevel();
        return false;
    }
    public void StartLevel()
    {
        if (!IfStartLevel)
        {
            //是关卡关，显示关卡开始的提示
            if (IsCurrentSceneLevel())
            {
                SetGameMode(GamePlayMode.UIInteract);
                m_UIManager.ShowLongTip("选择时间段以开始游戏");
            }
            IfStartLevel = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
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
                if (Player&&Player.rb)
                    Player.rb.velocity = Vector2.zero;
            }
            else
            {
                Time.timeScale = 1;
                if (Player && Player.rb)
                    Player.rb.velocity = Vector2.zero;
            }

            if (timeSectionManager)
            {
                if (playMode == GamePlayMode.Replay)
                    timeSectionManager.SwitchSkillReplayButton(true);
                else
                    timeSectionManager.SwitchSkillReplayButton(false);
            }
        }
    }

    //检查是否通关
    public bool CheckIfPass()
    {
        //bool laganFinished = true;
        //foreach (var lagan in laganRequiredToPass)
        //    if (!lagan.bPicked)
        //    {
        //        laganFinished = false;
        //        break;
        //    }
        if (!IfPass)
        {
            if (gamePlayMode != GamePlayMode.Replay)
            {
                if (timeSectionManager)
                    ifPass = timeSectionManager.GetNowLogicBugNum() == 0 && IfTouchTransport;
                if (ifPass)
                {
                    OnSuccessPassed();
                    return true;
                }
            }
            return false;
        }
        return true;
    }

    void OnSuccessPassed()
    {
        print("过关成功！");
        timeSectionManager.EndSection();
        if (RecordManager.instance.startRecord)
        {
            SetGameMode(GamePlayMode.Replay);
            m_UIManager.CanvasFadeInAndOut();
            StartCoroutine(ReplayCoroutine());
        }
        else
            StartCoroutine(LoadLevelCoroutine());
    }
    IEnumerator ReplayCoroutine()
    {
        yield return 0.3f;
        ReplayManager.instance.StartReplay(DefaultBornPos);
    }

    //重载关卡
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RespawnPlayer(bool CheckIfPlayerNull = true)
    {
        if (Player == null || !CheckIfPlayerNull)
        {
            Vector3 spawnPos = timeSectionManager.GetCurSectionData() != null ? timeSectionManager.GetCurSectionData().playerPositonOnSectionStart : DefaultBornPos;
            var player_obj = Instantiate(ResoucesManager.Instance.Resouces[playerPrefabName], spawnPos, Quaternion.identity);
            if (player_obj == null)
                print("player重生");
            playerDeathSection = -1;
        }
    }
    public IEnumerator LoadLevelCoroutine(float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        LoadNextLevel();
    }
    /// <summary>
    /// 立即加载下一关
    /// </summary>
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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

    public bool CanPlayerInput()
    {
        return gamePlayMode != GamePlayMode.UIInteract;
    }
    bool IsCurrentSceneLevel()
    {
        return SceneManager.GetActiveScene().name.ToLower().Contains("lev");//其实应该是level，宽松一点
    }

    public void SetCursorImage()
    {
    }
    public void SetCursorVisible(bool visible)
    {
        Cursor.visible = visible;
    }
}
