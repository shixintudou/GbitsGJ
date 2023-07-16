using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;
    public bool endRecord;
    public bool startRecord;
    public float time;
    public List<float> jumpTimes;
    public LaganController lagan;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
            
        else if (instance != this)
            Destroy(this);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartRecord(int num)
    {
        endRecord = false;
        startRecord = true;
        jumpTimes = new List<float>();
        lagan = null;
        StartCoroutine(RecordCoroutine(num));
    }
    IEnumerator RecordCoroutine(int num)
    {
        PlayData data = new PlayData()
        {
            horizonData = new List<HorizontalTimeData>(),
            jumpTimes = new List<float>(),
        };
        float horizontal = 0;
        time = 0f;
        HorizontalTimeData horizontalTimeData = new HorizontalTimeData();
        while (!endRecord)
        {
            if (Mathf.Abs(horizontal)<0.5f)
            {
                if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0.5f)
                {
                    horizontalTimeData.val = Input.GetAxisRaw("Horizontal");
                    horizontal = Input.GetAxisRaw("Horizontal");
                    horizontalTimeData.startTime = time;
                }
            }
            else
            {
                if(Mathf.Abs(horizontal*Input.GetAxisRaw("Horizontal"))<0.2f)
                {
                    horizontalTimeData.endTime = time;
                    data.horizonData.Add(horizontalTimeData);
                    horizontalTimeData = new HorizontalTimeData();
                    horizontal = 0;
                }
                else if(horizontal * Input.GetAxisRaw("Horizontal")<-0.8f)
                {
                    horizontalTimeData.endTime = time;
                    data.horizonData.Add(horizontalTimeData);
                    horizontalTimeData = new HorizontalTimeData();
                    horizontal = Input.GetAxisRaw("Horizontal");
                    horizontalTimeData.startTime = time;
                    horizontalTimeData.val = horizontal;
                }
            }

            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    data.jumpTimes.Add(time);
            //}
            time+= Time.deltaTime;
            yield return null;
        }
        if (horizontalTimeData.endTime<=horizontalTimeData.startTime&&Mathf.Abs(horizontalTimeData.val)>0.1f)
        {
            horizontalTimeData.endTime = time;
            data.horizonData.Add(horizontalTimeData);
        }
        data.jumpTimes = jumpTimes;
        data.lagan = lagan;
        ReplayManager.instance.datas[num]= data;
        startRecord = false;
        lagan = null;
    }
    public void AddJumpTimes()
    {
        jumpTimes.Add(time);
    }
}
