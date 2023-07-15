using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LUIManager : MonoBehaviour
{
    public void ShowTip(string str)
    {
        var Tip = Instantiate<GameObject>(ResoucesManager.Instance.Resouces["TipUI"]);
        if (Tip != null)
        {
            Tip.GetComponentInChildren<TextMeshProUGUI>().text = str;
        }
    }

    public void CanvasFadeInAndOut()
    {
        Instantiate(ResoucesManager.Instance.Resouces["FadeCanvas"]);
    }
}

