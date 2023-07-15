using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSectionData
{

    public TimeSectionButton button;
    public bool ifStarted;
    public bool ifEnded;
    public bool ifBug;
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
    public GameObject BugPrefab;

    List<TimeSectionData> timeSectionsData = new List<TimeSectionData>();

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
                        player.transform.position = Camera.main.ScreenToWorldPoint(clickPos);
                    FinishedNewBornPosition();
                }
            }
        }
    }
    public void TrySelectTimeSectionIndex(int index)
    {
        if (index < 0 || index > GameMode.Instance.TimeSectionNum) return;
        if (index == 0)
        {
            //玩家转移到默认出生点
            PlayerHJ player = GameMode.Instance.Player;
            if (player)
                player.transform.position = GameMode.Instance.DefaultBornTrans.position;
        }
        else
        {
            //前一时间段尚未结束，玩家需指定新的位置
            if (!timeSectionsData[index - 1].ifEnded)
            {
                StartedNewBornPosition();
            }
        }
        nowTimeSection = index;
    }
    bool CheckIfPosSelectValid(Vector3 pos)
    {
        return true;
    }
    void StartedNewBornPosition()
    {
        isSelectingPosition = true;
        DarkCanvas.SetActive(true);
        GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
    }
    void FinishedNewBornPosition()
    {
        isSelectingPosition = false;
        DarkCanvas.SetActive(false);
        GameMode.Instance.SetGameMode(GamePlayMode.Play);
    }
    //玩家点击对应按钮手动结束当前时间段
    public void FinishThisTimeSection()
    {
        //判定是否可以结束
        if (true)
        {
            PlayerHJ player = GameMode.Instance.Player;
            if (player)
            {
                Instantiate(BugPrefab, player.gameObject.transform);
                timeSectionsData[NowTimeSection].ifEnded = true;
            }
        }
    }

    public void RegisterTimeButton(TimeSectionButton button)
    {
        if (button)
        {
            timeSectionsData.Add(new TimeSectionData(button));
        }
    }

    public void ResetThisSection()
    {
        timeSectionsData[NowTimeSection].ifEnded = false;
        timeSectionsData[NowTimeSection].ifBug = false;
    }
    public int GetNowLogicBugNum()
    {
        int res = 0;
        foreach (TimeSectionData data in timeSectionsData)
            if (data.ifBug)
                res++;
        return res;
    }
}
