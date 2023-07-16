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

    //��ǰʱ���
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
        //����ѡ��λ��
        if (GameMode.GamePlayMode == GamePlayMode.UIInteract && isSelectingPosition)
        {
            //�������
            if (Input.GetMouseButtonDown(0))
            {
                //�ж�λ���Ƿ���Ч
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
    /// ѡ��ʱ��ΰ�ť���л�ʱ��
    /// </summary>
    /// <param name="number"></param>��1-N
    public void TrySelectTimeSectionIndex(int number)
    {
        if (number <= 0 || number > GameMode.Instance.TimeSectionNum) return;

        if (NowTimeSection > 0)
        {
            TimeSectionData nowSectionData = timeSectionsDataList[NowTimeSection - 1];
            if (number == NowTimeSection)
            {
                GameMode.Instance.m_UIManager.ShowTip("�Ѵ��ڸ�ʱ���!");
                return;
            }
            if (nowSectionData.ifStarted && !nowSectionData.ifEnded && NowTimeSection != 0)
            {
                GameMode.Instance.m_UIManager.ShowTip("���Ƚ�����ǰʱ���!");
                return;
            }
        }
        print("ѡ��ʱ���" + number);
        //ѡ���ʱ����ѿ�ʼ��
        if (timeSectionsDataList[number - 1].ifStarted)
        {
            GameMode.Instance.m_UIManager.ShowTip("��ʱ����ѽ���!");
            return;
        }
        //�Ƿ���Ҫ�������
        if (GameMode.Instance.playerDeathSection >= 0 && number < GameMode.Instance.playerDeathSection)
        {
            GameMode.Instance.RespawnPlayer();
        }
        nowTimeSection = number;
        //ѡ���ʱ���δ��ʼ��
        if (number == 1)
        {
            //���ת�Ƶ�Ĭ�ϳ�����
            GameMode.Instance.SetPlayerPos(GameMode.Instance.DefaultBornPos);
            timeSectionsDataList[0].playerPositonOnSectionStart = GameMode.Instance.DefaultBornPos;
            StartSection();
            GameMode.Instance.SetGameMode(GamePlayMode.Play);
        }
        else
        {
            //ǰһʱ�����δ����(/��ʼ)�������ָ���µ�λ��
            if (!timeSectionsDataList[number - 1 - 1].ifEnded)
                StartedChoosingNewBornPosition();
            //���ת�Ƶ���һʱ��ν���λ��
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
        ////ѡ���ʱ����ѿ�ʼ�������ת�Ƶ���ʱ��ν���λ��
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
            //�����߼�©��
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

    //��ҵ����Ӧ��ť�ֶ�������ǰʱ���
    public void FinishThisTimeSection()
    {
        //�ж��Ƿ���Խ���
        if (true && NowTimeSection > 0 && !timeSectionsDataList[NowTimeSection - 1].ifEnded)
        {
            PlayerHJ player = GameMode.Instance.Player;
            if (player)
            {
                timeSectionsDataList[NowTimeSection - 1].playerPositonOnSectionEnd = player.transform.position;
                EndSection();
                GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
            }
            GameMode.Instance.m_UIManager.ShowTip("ʱ����ѽ���");
        }
        else if (NowTimeSection == 0)
        {
            GameMode.Instance.m_UIManager.ShowTip("����ѡ��һ��ʱ��Σ�");
        }
    }
    public void OnClearABug(GameObject bug_obj)
    {
        //����ʱ���
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
                GameMode.Instance.m_UIManager.ShowLongTip("�ƺ����޷�������Ϸ��������");
            else
                GameMode.Instance.m_UIManager.ShowTip("��ǰʱ����ѽ���");
        }
    }

    public void StartSection()
    {
        //��ʼ��¼
        if (nowTimeSection > 0)
            timeSectionsDataList[nowTimeSection - 1].ifStarted = true;
        print("ʱ���" + nowTimeSection + "��ʼ¼��");
        RecordManager.instance.StartRecord(nowTimeSection - 1);
    }
    public void EndSection()
    {
        //������¼
        if (nowTimeSection > 0)
            timeSectionsDataList[NowTimeSection - 1].ifEnded = true;
        print("ʱ���" + nowTimeSection + "����¼��");
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
    /// ���ص�ǰ��©������
    /// </summary>
    /// <returns></returns>
    public int GetNowLogicBugNum()
    {
        return nowBugsNum;
    }
}