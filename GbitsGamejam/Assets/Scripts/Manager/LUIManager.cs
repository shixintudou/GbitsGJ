using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LUIManager : MonoBehaviour
{
    public void ShowTip(string str)
    {
        var Tip = Instantiate<GameObject>(ResoucesManager.Instance.Resouces["TipUI"]);
        print((Tip == null) + " | " + (Tip.GetComponentInChildren<TextMeshProUGUI>() == null));
        if (Tip != null)
        {
            Tip.GetComponentInChildren<TextMeshProUGUI>().text = str;
        }
    }
}

