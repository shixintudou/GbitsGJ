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

    //��ǰʱ���
    private int nowTimeSection;
    public int NowTimeSection { get => Mathf.Clamp(nowTimeSection, 0, GameMode.Instance.TimeSectionNum); }

    bool isSelectingPosition;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //����ѡ��λ��
        if (GameMode.GamePlayMode == GamePlayMode.UIInteract && isSelectingPosition)
        {
            //�������
            if (Input.GetMouseButtonDown(0))
            {
                //�ж�λ���Ƿ���Ч
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
            //���ת�Ƶ�Ĭ�ϳ�����
            PlayerHJ player = GameMode.Instance.Player;
            if (player)
                player.transform.position = GameMode.Instance.DefaultBornTrans.position;
        }
        else
        {
            //ǰһʱ�����δ�����������ָ���µ�λ��
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
    //��ҵ����Ӧ��ť�ֶ�������ǰʱ���
    public void FinishThisTimeSection()
    {
        //�ж��Ƿ���Խ���
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
