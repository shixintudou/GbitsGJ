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
    List<TimeSectionData> timeSectionsData = new List<TimeSectionData>();

    private int nowTimeSection;
    public int NowTimeSection { get => Mathf.Clamp(nowTimeSection,0, GameMode.Instance.TimeSectionNum); }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void TrySelectTimeSectionIndex(int index)
    {
        if (index < 0 || index > GameMode.Instance.TimeSectionNum) return;
        if (index == 0)
        {

        }
        else
        {
            //前一时间段尚未结束，玩家指定新的位置
            if (!timeSectionsData[index - 1].ifEnded)
            {
                GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
            }
        }
        nowTimeSection = index;
    }
    //bool CheckIfTimeSelectValid(int index)
    //{

    //}

    void FinishedNewBornPosition()
    {
        GameMode.Instance.SetGameMode(GamePlayMode.Play);
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
        timeSectionsData[NowTimeSection].ifBug=false;
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
