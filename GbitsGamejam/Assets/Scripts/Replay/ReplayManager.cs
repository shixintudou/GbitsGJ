using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HorizontalTimeData
{
    public float val;
    public float startTime;
    public float endTime;
}

[Serializable]
public struct PlayData
{
    public List<HorizontalTimeData> horizonData;
    public List<float> jumpTimes;
    public LaganController lagan;
}
public class ReplayManager : MonoBehaviour
{
    public static ReplayManager instance;
    public List<PlayData> datas;

    Coroutine replayCoroutine;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
        DontDestroyOnLoad(this);
    }
    public void SetDataNum(int num)
    {
        datas=new List<PlayData>();
        for(int i=0;i<num;i++)
        {
            datas.Add(new PlayData());
        }
    }
    public void StartReplay(Vector2 startPos)
    {
        GameMode.Instance.SetGameMode(GamePlayMode.Replay);
        PlayerHJ player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHJ>();
        player.transform.position = startPos;
        player.rb.velocity = Vector2.zero;
        if(replayCoroutine!=null)
        {
            StopCoroutine(replayCoroutine);
        }       
        replayCoroutine = StartCoroutine(RePlayCoroutine(player));
    }

    IEnumerator RePlayCoroutine(PlayerHJ player)
    {
        foreach(PlayData data in datas)
        {
            float time = data.horizonData[data.horizonData.Count - 1].endTime;
            if(data.lagan!=null)
            {
                data.lagan.controlledFlat.FlatDisable();
            }
            StartCoroutine(ReplayClip(player, time, data));
            yield return new WaitForSeconds(time);
        }
        GameMode.Instance.SetGameMode(GamePlayMode.Play);
    }
    IEnumerator ReplayClip(PlayerHJ player ,float time ,PlayData data)
    {
        int jumpIndex = 0;
        int horiIndex = 0;
        float t = 0f;
        List<HorizontalTimeData> horizontalData = data.horizonData;
        List<float> jumpTimes = data.jumpTimes;
        while (t<time)
        {
            if(jumpIndex < jumpTimes.Count)
            {
                if (jumpTimes[jumpIndex]<=t)
                {
                    player.Jump();
                    jumpIndex++;
                }
            }
            if(horiIndex<horizontalData.Count)
            {
                if (t >= horizontalData[horiIndex].startTime)
                {
                    var temp = horizontalData[horiIndex];
                    player.MoveWithTargetAndTime(temp.val, temp.endTime - temp.startTime);
                    horiIndex++;
                }
            }
            t += Time.deltaTime;
            yield return null;
        }
    }
}
