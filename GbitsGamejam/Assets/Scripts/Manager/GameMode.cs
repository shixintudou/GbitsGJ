using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GamePlayMode
{
    Play, Replay, UIInteract,Act
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

    [Header("��������")]
    bool ifPass;
    [HideInInspector]
    public int playerDeathSection = -1;
    [HideInInspector]
    public bool IfTouchTransport;
    public bool IfPass { get => ifPass; }

    [Header("�ؿ�����")]
    public int TimeSectionNum;
    public Vector3 DefaultBornPos;
    string playerPrefabName = "Player";
    //0Ϊ����״̬ 1-N�ֱ�Ϊѡ���˵�N�οɷ���ʱ���


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
        //����
        if (instance != this)
        {
            Destroy(instance);
            instance = this;
        }
        //��ȡ�����й�����������ˣ�ע��״̬
        var keys = FindObjectsOfType<LaganController>();
        foreach (var key in keys)
            laganRequiredToPass.Add(key);
        //��¼��ҳ���λ��
        DefaultBornPos = Player.transform.position;
        playerPrefabName = Player.name.Replace("Clone", "");

        //��ʱ����
        ReplayManager.instance.IsReadyForLoadNextScene = false;

        //��ȡ������ʱ�������
        timeSectionManager = FindObjectOfType<TimeSectionManager>();
        if (timeSectionManager == null)
            print("������ȱ��TimeSectionManager!");

        m_UIManager = GetComponent<LUIManager>();
        if (m_UIManager == null)
            m_UIManager = gameObject.AddComponent<LUIManager>();

        gamePlayMode = GamePlayMode.UIInteract;
        m_UIManager.ShowLongTip("ѡ��ʱ����Կ�ʼ��Ϸ");
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
                if (Player)
                {
                    Player.rb.velocity = Vector2.zero;
                }
            }
            else
            {
                Time.timeScale = 1;
                if (Player)
                {
                   // Player.SetVisible(true);
                    Player.rb.velocity = Vector2.zero;
                }
            }

        }
    }

    //����Ƿ�ͨ��
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
        print("���سɹ���");
        timeSectionManager.EndSection();
        if (RecordManager.instance.startRecord)
        {
            SetGameMode(GamePlayMode.Replay);
            m_UIManager.CanvasFadeInAndOut();
            StartCoroutine(ReplayCoroutine());
        }
        else
            StartCoroutine(LoadLevelCoroutine(1.2f));
    }
    IEnumerator ReplayCoroutine()
    {
        yield return 0.3f;
        ReplayManager.instance.StartReplay(DefaultBornPos);
    }

    //���عؿ�
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
                print("player����");
            playerDeathSection = -1;
        }
    }
    IEnumerator LoadLevelCoroutine(float delay = 1f)
    {
        print("delay" + delay + "S");
        yield return new WaitForSeconds(delay);
        LoadNextLevel();
    }
    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    /// <summary>
    /// ����ҽӴ����߼�©��
    /// </summary>
    /// <param name="bugSectionBelongTo"></param>
    /// <param name="Bug_obj"></param>
    public void OnPlayerTouchSectionBug(int bugSectionBelongTo, LogicBug Bug_obj)
    {
        //����һ�ε��߼�©��
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
