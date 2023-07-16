using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    public static ActManager instance;
    public float actMoveSpeed;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    public void StartAct()
    {

    }
}
