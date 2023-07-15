using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct HorizontalTimeData
{
    public float val;
    public float startTime;
    public float endTime;
}

public struct PlayData
{
    public List<HorizontalTimeData> horizonData;
    public List<float> jumpTimes;
}
public class ReplayManager : MonoBehaviour
{
    public static ReplayManager instance;
    public List<PlayData> datas;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }
    public void SetDataNum(int num)
    {
        datas = new List<PlayData>(num);
    }
    public void StartReplay()
    {

    }
}
