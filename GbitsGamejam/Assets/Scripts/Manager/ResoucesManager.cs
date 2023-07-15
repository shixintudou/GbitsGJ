using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoucesManager:Object
{
    private static ResoucesManager instance;
    public static ResoucesManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ResoucesManager();
            return instance;
        }
    }
    public Dictionary<string,GameObject> Resouces = new Dictionary<string,GameObject>();
    public ResoucesManager()
    {
        var arrays = Resources.LoadAll("Prefabs/");
        foreach (var array in arrays)
            Resouces.Add(array.name, array as GameObject);
        foreach (var item in Resouces)
        {
            Debug.Log("º”‘ÿ‘§÷∆ÃÂ"+item.Key);
        }
    }
}
