using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSectionData
{

    public TimeSectionButton button;
    public bool ifStarted;
    public bool ifEnded;
    public Vector3 playerPositonOnSectionStart;
    public Vector3 playerPositonOnSectionEnd;
    public TimeSectionData(TimeSectionButton _button)
    {
        button = _button;
        ifStarted = false;
        ifEnded = false;
    }
}

public class TimeSectionManager : MonoBehaviour
{
    public GameObject DarkCanvas;

    List<TimeSectionData> timeSectionsDataList = new List<TimeSectionData>();
    public List<TimeSectionData> TimeSectionsDataList { get => timeSectionsDataList; }

    //当前时间段
    private int nowTimeSection;
    public int NowTimeSection { get => Mathf.Clamp(nowTimeSection, 0, GameMode.Instance.TimeSectionNum); }

    private int nowBugsNum;
    public int NowBugsNum { get => nowBugsNum; }

    bool isSelectingPosition;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator)
            animator.SetInteger("MaxSection", GameMode.Instance.TimeSectionNum);
    }

    // Update is called once per frame
    void Update()
    {
        //正在选择位置
        if (GameMode.GamePlayMode == GamePlayMode.UIInteract && isSelectingPosition)
        {
            //按下鼠标
            if (Input.GetMouseButtonDown(0))
            {
                //判定位置是否有效
                Vector3 clickPos = Input.mousePosition;
                if (CheckIfPosSelectValid(Input.mousePosition))
                {
                    PlayerHJ player = GameMode.Instance.Player;
                    if (player)
                    {
                        player.transform.position = (Vector3)(Vector2)Camera.main.ScreenToWorldPoint(clickPos);
                        timeSectionsDataList[nowTimeSection - 1].playerPositonOnSectionStart = player.transform.position;
                        FinishedChoosingNewBornPosition();
                    }
                }
            }
        }
    }
    /// <summary>
    /// 选择时间段按钮，切换时间
    /// </summary>
    /// <param name="number"></param>从1-N
    public void TrySelectTimeSectionIndex(int number)
    {
        if (number <= 0 || number > GameMode.Instance.TimeSectionNum) return;

        if (NowTimeSection > 0)
        {
            TimeSectionData nowSectionData = timeSectionsDataList[NowTimeSection - 1];
            if (number == NowTimeSection)
            {
                GameMode.Instance.m_UIManager.ShowTip("已处于该时间段!");
                return;
            }
            if (nowSectionData.ifStarted && !nowSectionData.ifEnded && NowTimeSection != 0)
            {
                GameMode.Instance.m_UIManager.ShowTip("请先结束当前时间段!");
                return;
            }
        }
        print("选择时间段" + number);
        //选择的时间段已开始过
        if (timeSectionsDataList[number - 1].ifStarted)
        {
            GameMode.Instance.m_UIManager.ShowTip("该时间段已结束!");
            return;
        }
        //是否需要复活玩家
        if (GameMode.Instance.playerDeathSection >= 0 && number < GameMode.Instance.playerDeathSection)
        {
            GameMode.Instance.RespawnPlayer();
        }
        nowTimeSection = number;
        //选择的时间段未开始过
        if (number == 1)
        {
            //玩家转移到默认出生点
            GameMode.Instance.SetPlayerPos(GameMode.Instance.DefaultBornPos);
            timeSectionsDataList[0].playerPositonOnSectionStart = GameMode.Instance.DefaultBornPos;
            StartSection();
            GameMode.Instance.SetGameMode(GamePlayMode.Play);
        }
        else
        {
            //前一时间段尚未结束(/开始)，玩家需指定新的位置
            if (!timeSectionsDataList[number - 1 - 1].ifEnded)
                StartedChoosingNewBornPosition();
            //玩家转移到上一时间段结束位置
            else
            {
                Vector3 StarPos = timeSectionsDataList[number - 1 - 1].playerPositonOnSectionEnd;
                GameMode.Instance.SetPlayerPos(StarPos);
                timeSectionsDataList[nowTimeSection - 1].playerPositonOnSectionStart = StarPos;
                GameMode.Instance.SetGameMode(GamePlayMode.Play);
                StartSection();
            }
        }
        if (animator)
            animator.SetInteger("SelectSection", nowTimeSection);
        ////选择的时间段已开始过，玩家转移到该时间段结束位置
        //else
        //{
        //    GameMode.Instance.SetPlayerPos(timeSectionsDataList[number - 1].playerPositonOnSectionEnd);
        //    GameMode.Instance.SetGameMode(GamePlayMode.Play);
        //}
    }
    bool CheckIfPosSelectValid(Vector2 screenPos)
    {
        if (Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(screenPos), 0.5f))
            return false;
        return true;
    }
    void StartedChoosingNewBornPosition()
    {
        isSelectingPosition = true;
        DarkCanvas.SetActive(true);
        GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
    }
    void FinishedChoosingNewBornPosition()
    {
        isSelectingPosition = false;
        DarkCanvas.SetActive(false);
        PlayerHJ player = GameMode.Instance.Player;
        if (player)
        {
            //生成逻辑漏洞
            var Bug_obj = Instantiate(ResoucesManager.Instance.Resouces["LogicBug"], player.transform.position, Quaternion.identity);
            LogicBug bug = Bug_obj.GetComponent<LogicBug>();
            if (bug)
            {
                bug.SetSectionIndex(nowTimeSection);
            }
            timeSectionsDataList[NowTimeSection - 1].playerPositonOnSectionStart = player.transform.position;
            nowBugsNum++;
        }
        GameMode.Instance.SetGameMode(GamePlayMode.Play);
        StartSection();

    }

    //玩家点击对应按钮手动结束当前时间段
    public void FinishThisTimeSection()
    {
        //判定是否可以结束
        if (true && NowTimeSection > 0 && !timeSectionsDataList[NowTimeSection - 1].ifEnded)
        {
            PlayerHJ player = GameMode.Instance.Player;
            if (player)
            {
                timeSectionsDataList[NowTimeSection - 1].playerPositonOnSectionEnd = player.transform.position;
                EndSection();
                GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
            }
            GameMode.Instance.m_UIManager.ShowTip("时间段已结束");
        }
        else if (NowTimeSection == 0)
        {
            GameMode.Instance.m_UIManager.ShowTip("请先选择一段时间段！");
        }
    }
    public void OnClearABug(GameObject bug_obj)
    {
        //结束时间段
        if (nowTimeSection > 0)
            timeSectionsDataList[NowTimeSection - 1].playerPositonOnSectionEnd = bug_obj.transform.position;
        nowBugsNum--;
        EndSection();
        if (!GameMode.Instance.CheckIfPass())
        {
            GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
            bool ifAllSectionEnded = true;
            foreach (var data in TimeSectionsDataList)
                if (!data.ifEnded)
                {
                    ifAllSectionEnded = false;
                    break;
                }
            if (ifAllSectionEnded)
                GameMode.Instance.m_UIManager.ShowLongTip("似乎已无法继续游戏，请重试");
            else
                GameMode.Instance.m_UIManager.ShowTip("当前时间段已结束");
        }
    }

    public void StartSection()
    {
        //开始记录
        if (nowTimeSection > 0)
            timeSectionsDataList[nowTimeSection - 1].ifStarted = true;
      //  print("时间段" + nowTimeSection + "开始录制");
        RecordManager.instance.StartRecord(nowTimeSection - 1);
    }
    public void EndSection()
    {
        //结束记录
        if (nowTimeSection > 0)
            timeSectionsDataList[NowTimeSection - 1].ifEnded = true;
       // print("时间段" + nowTimeSection + "结束录制");
        RecordManager.instance.endRecord = true;
    }

    public void RegisterTimeButton(TimeSectionButton button)
    {
        if (button)
        {
            timeSectionsDataList.Add(new TimeSectionData(button));
        }
    }

    public void ResetThisSection()
    {
        var data = timeSectionsDataList[NowTimeSection];
        data.ifEnded = false;
        nowBugsNum = 0;
    }

    public TimeSectionData GetCurSectionData()
    {
        if (nowTimeSection == 0)
            return null;
        return TimeSectionsDataList[NowTimeSection - 1];
    }
    /// <summary>
    /// 返回当前的漏洞数量
    /// </summary>
    /// <returns></returns>
    public int GetNowLogicBugNum()
    {
        return nowBugsNum;
    }
}
