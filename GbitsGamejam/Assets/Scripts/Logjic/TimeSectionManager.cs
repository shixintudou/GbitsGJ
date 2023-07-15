using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSectionData
{

    public TimeSectionButton button;
    public bool ifStarted;
    public bool ifEnded;
    public bool ifBug;
    public Vector3 playerPositonOnSectionStart;
    public Vector3 playerPositonOnSectionEnd;
    public TimeSectionData(TimeSectionButton _button)
    {
        button = _button;
        ifStarted = false;
        ifEnded = false;
        ifBug = false;
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

    bool isSelectingPosition;

    void Start()
    {

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
                if (CheckIfPosSelectValid(clickPos))
                {
                    PlayerHJ player = GameMode.Instance.Player;
                    if (player)
                    {
                        player.transform.position = Camera.main.ScreenToWorldPoint(clickPos);
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
        //选择的时间段未开始过
        if (!timeSectionsDataList[number - 1].ifStarted)
            if (number == 1)
            {
                //玩家转移到默认出生点
                GameMode.Instance.SetPlayerPos(GameMode.Instance.DefaultBornTrans.position);
                timeSectionsDataList[0].ifStarted = true;
                timeSectionsDataList[0].playerPositonOnSectionStart = GameMode.Instance.DefaultBornTrans.position;
                GameMode.Instance.SetGameMode(GamePlayMode.Play);
            }
            else
            {
                //前一时间段尚未结束，玩家需指定新的位置
                if (!timeSectionsDataList[number - 1 - 1].ifEnded)
                    StartedChoosingNewBornPosition();
                else
                {
                    GameMode.Instance.SetPlayerPos(timeSectionsDataList[number -1- 1].playerPositonOnSectionEnd);
                    GameMode.Instance.SetGameMode(GamePlayMode.Play);
                }
            }
        else 
        {
            GameMode.Instance.SetPlayerPos(timeSectionsDataList[number - 1].playerPositonOnSectionEnd);
            GameMode.Instance.SetGameMode(GamePlayMode.Play);
        }
        nowTimeSection = number;
    }
    bool CheckIfPosSelectValid(Vector3 pos)
    {
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
            timeSectionsDataList[NowTimeSection - 1].ifBug = true;
        }
        GameMode.Instance.SetGameMode(GamePlayMode.Play);
        timeSectionsDataList[nowTimeSection - 1].ifStarted = true;
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
                timeSectionsDataList[NowTimeSection - 1].ifEnded = true;
                GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
            }
        }
        else if (NowTimeSection == 0)
        {
            GameMode.Instance.m_UIManager.ShowTip("请先选择一段时间段！");
        }
    }
    public void OnClearABug(GameObject bug_obj)
    {
        //结束时间段
        timeSectionsDataList[NowTimeSection - 1].playerPositonOnSectionEnd = bug_obj.transform.position;
        timeSectionsDataList[NowTimeSection - 1].ifBug = false;
        timeSectionsDataList[NowTimeSection - 1].ifEnded = true;
        if (!GameMode.Instance.CheckIfPass())
            GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
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
        data.ifBug = false;

    }

    public TimeSectionData GetCurSectionData()
    {
        if(nowTimeSection == 0)
            return null;
        return TimeSectionsDataList[NowTimeSection - 1];
    }
    /// <summary>
    /// 返回当前的漏洞数量
    /// </summary>
    /// <returns></returns>
    public int GetNowLogicBugNum()
    {
        int res = 0;
        foreach (TimeSectionData data in timeSectionsDataList)
            if (data.ifBug)
                res++;
        return res;
    }
}
