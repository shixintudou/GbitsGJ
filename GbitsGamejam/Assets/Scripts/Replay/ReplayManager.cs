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
    public bool endReplay = false;
    Coroutine replayCoroutine;

    public bool IsReadyForLoadNextScene;
    // Start is called before the first frame update
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
    public void SetDataNum(int num)
    {
        datas = new List<PlayData>();
        for (int i = 0; i < num; i++)
        {
            datas.Add(new PlayData());
        }
    }
    public void StartReplay(Vector2 startPos)
    {
        endReplay = false;
        GameMode.Instance.SetGameMode(GamePlayMode.Replay);
        PlayerHJ player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHJ>();
        player.transform.position = startPos;
        player.rb.velocity = Vector2.zero;
        RendererFeatureManager.instance.SetOldTVActive(true);
        if (replayCoroutine != null)
        {
            StopCoroutine(replayCoroutine);
        }
        replayCoroutine = StartCoroutine(RePlayCoroutine(player));
    }

    IEnumerator RePlayCoroutine(PlayerHJ player)
    {
        foreach (PlayData data in datas)
        {
            int horiCount = data.horizonData.Count;
            int jumpCount = data.jumpTimes.Count;
            float time = 0f;
            if (horiCount > 0)
            {
                time = data.horizonData[horiCount - 1].endTime;
            }
            if (jumpCount > 0)
            {
                float t = data.jumpTimes[jumpCount - 1];
                time = time > t ? time : t;
            }
            if (time == 0f)
            {
                continue;
            }
            if (data.lagan != null && data.lagan.controlledFlat != null)
            {
                GameMode.Instance.m_UIManager.CanvasFadeInAndOut();
                data.lagan.controlledFlat.FlatDisable();
            }
            if (player == null || endReplay)
            {
                endReplay = true;
                break;
            }
            StartCoroutine(ReplayClip(player, time, data));
            yield return new WaitForSeconds(time);
        }
        GameMode.Instance.SetGameMode(GamePlayMode.Play);
        RendererFeatureManager.instance.SetOldTVActive(false);
        if (!IsReadyForLoadNextScene)
        {
            //1s∫Û«–ªª≥°æ∞
            IsReadyForLoadNextScene = true;
            yield return 1.0f;
            GameMode.Instance.LoadNextLevel();
        }
    }
    IEnumerator ReplayClip(PlayerHJ player, float time, PlayData data)
    {
        int jumpIndex = 0;
        int horiIndex = 0;
        float t = 0f;
        List<HorizontalTimeData> horizontalData = data.horizonData;
        List<float> jumpTimes = data.jumpTimes;
        if (player)
            player.rb.velocity = Vector2.zero;
        while (t < time)
        {
            if (player == null || endReplay)
            {
                endReplay = true;
                break;
            }
            if (jumpIndex < jumpTimes.Count)
            {
                if (jumpTimes[jumpIndex] <= t + 0.1f)
                {
                    if (player)
                    {
                        player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
                        player.Jump();
                    }

                    jumpIndex++;
                }
            }
            if (horiIndex < horizontalData.Count)
            {
                if (t >= horizontalData[horiIndex].startTime)
                {
                    var temp = horizontalData[horiIndex];
                    if (player)
                        player.MoveWithTargetAndTime(temp.val, temp.endTime - temp.startTime);
                    horiIndex++;
                }
            }
            t += Time.deltaTime;
            yield return null;
        }
    }
}
