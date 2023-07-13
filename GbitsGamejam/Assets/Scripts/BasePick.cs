using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePick : MonoBehaviour
{
    new public string name;
    public List<string> description;
    //public Sprite sprite;
    public bool bPicked = false;
    public bool canPick = false;

    public virtual bool CheckCondition()
    {
        return true;
    }

    public virtual void OnDialogOver()
    {

    }
}
