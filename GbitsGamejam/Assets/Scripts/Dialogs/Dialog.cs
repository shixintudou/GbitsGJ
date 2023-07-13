using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogCanvas;
    public TextMeshProUGUI text;
    public static Dialog instance;
    public RectTransform panelTrans;
    public Vector3 originScale;
    public float offsetx;
    public float offsety;
    List<string> dias;
    public Transform playerTransform;
    public Transform thingTransform;
    int index = 0;
    TalkState state;
    Vector3 offset;
    public float length;
    bool kb;
    [SerializeField]
    Vector3 tipPosition;
    Sprite originSprite;
    [SerializeField]
    Sprite tipSprite;
    Action onDialogOver;


    public enum TalkState
    {
        Player,Npc,Tips
    }
    private void Awake()
    {
        dialogCanvas = GameObject.FindGameObjectWithTag("DialogCanvas");
        text = dialogCanvas.GetComponentInChildren<TextMeshProUGUI>();
        dias=new List<string>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        instance = this;
        state = TalkState.Npc;
        offset = new Vector3(offsetx, offsety, 0);
        kb = false;
        panelTrans = GameObject.FindGameObjectWithTag("Panel").GetComponent<RectTransform>();
        originScale = panelTrans.localScale;
        originSprite = panelTrans.GetComponent<Image>().sprite;
    }
    void Start()
    {
        dialogCanvas.SetActive(false);
        if (instance != this)
            Destroy(this);
    }

    // Update is called once per frame
    public void BeginShowDialog(Transform thingTransform,List<string> dias, Action ondialogover = null)
    {
        
        onDialogOver = ondialogover;
        this.thingTransform = thingTransform;
        dialogCanvas.SetActive(true);
        SetTextPosition(thingTransform.position, offset);
        CopyDialogs(dias);
        Collider2D collider;
        collider = thingTransform.GetComponent<Collider2D>();
        if(collider)
        {
            collider.enabled = false;
        }
        if (dias.Count == 0)
        {
            EndShowDialog();
            return;
        }
        StartCoroutine(ShowDialogCoroutine());
    }
    public void SetTextPosition(Vector3 position,Vector3 offset)
    {
        Vector3 scrernPosition = Camera.main.WorldToScreenPoint(position);
        text.rectTransform.position = scrernPosition + offset;
    }
    public void SetPanelScale(string dia)
    {
        int x = 0;
        int y = 1;
        int tx = 0;
        foreach(char i in dia)
        {
            if (i.Equals('\n')||i.Equals(';'))
            {
                y++;
                x = x > tx ? x : tx;
                tx = 0;
            }   
            else
            {
                tx++;
            }
        }
        if (tx > x)
            x = tx;
        panelTrans.localScale = new Vector3(originScale.x * x, originScale.y * y, originScale.z);
    }
    public void CopyDialogs(List<string> dias)
    {
        this.dias.Clear();
        foreach(string i in dias)
        {
            this.dias.Add(i);
        }
    }
    public void OnIndexChange()
    {
        if(index<dias.Count)
        {
            GetTalkPeople(dias[index]);    
            SetPanelScale(dias[index]);
        }
    }
    public void GetTalkPeople(string dia)
    {
        if (dia.Contains("Player:"))
        {
            state = TalkState.Player;
            panelTrans.GetComponent<Image>().sprite = originSprite;
        }
            
        else if (dia.Contains("System:"))
        {
            state = TalkState.Tips;
            panelTrans.GetComponent<Image>().sprite = tipSprite;
            SetTextPositionByScreenPosition(tipPosition);
        }            
        else
        {
            state = TalkState.Npc;
            panelTrans.GetComponent<Image>().sprite = originSprite;
        }
            
    }
    public string GetDialog(int index)
    {
        if(index<dias.Count)
        {
            if (dias[index].Contains("Player:") || (dias[index].Contains("System:")))
            {
                string t = "";
                for(int i = 7; i < dias[index].Length;i++)
                {
                    t += dias[index][i];
                }
                t = t.Replace(";", "\\n");
                Debug.Log(t);
                return t;
                
            }
            else
            {
                string t = dias[index];
                t = t.Replace(";", "\\n");
                Debug.Log(t);
                return t;
            }
        }
        return null;
    }
    public void EndShowDialog()
    {
        index = 0;
        text.text = "";
        dias.Clear();
        Debug.LogWarning("End Show Dialog !!!");
        dialogCanvas.SetActive(false);
        thingTransform.GetComponent<Collider2D>().enabled = true;
        kb = false;
        if (onDialogOver != null)
        {
            onDialogOver();
            onDialogOver = null;
        }
    }
    public void SetTextPositionByScreenPosition(Vector3 position)
    {
        text.rectTransform.position = position;
    }
    IEnumerator ShowDialogCoroutine()
    {
        
        int i = 0;
        text.text = GetDialog(i);
        text.text = text.text.Replace("\\n", "\n");
        SetPanelScale(text.text);
        while(index<dias.Count)
        {
            if (state == TalkState.Player)
                SetTextPosition(playerTransform.position, offset);
            else if(state==TalkState.Npc)
                SetTextPosition(thingTransform.position, offset);
            if(Input.GetMouseButtonDown(0))
            {
                index++;
                OnIndexChange();
            }
            if(Input.GetKeyDown(KeyCode.E)&&kb)
            {
                index++;
                OnIndexChange();
            }
            if(i!=index&&index<dias.Count)
            {
                i++;
                text.text = GetDialog(i);
                text.text = text.text.Replace("\\n", "\n");
                SetPanelScale(text.text);
            }
            if(Vector2.Distance(playerTransform.position,thingTransform.position)>length)
            {
                
                EndShowDialog();
            }
            yield return null;
            kb = true;
        }
        EndShowDialog();
        
    }
}
