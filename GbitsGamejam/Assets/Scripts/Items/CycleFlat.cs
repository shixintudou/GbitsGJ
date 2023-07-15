using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleFlat : FlatBase
{
    public enum State
    {
        Idle,StayLeft,StayRight,MoveLeft,MoveRight
    }
    // Start is called before the first frame update
    public float stayTime;
    public State state;
    public Transform leftTrans;
    public Transform rightTrans;
    void Start()
    {
        state = State.Idle;
        startPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.MoveLeft:
                Move(leftTrans.position,State.StayLeft);
                break;
            case State.MoveRight:
                Move(rightTrans.position, State.StayRight);
                break;
        }
    }
    public override void FlatEnable()
    {
        base.FlatEnable();
        state = State.MoveLeft;
    }
    public override void FlatDisable()
    {
        base.FlatDisable();
        state = State.Idle;
        transform.position = startPostion;
    }
    public void Move(Vector2 target,State state)
    {
        StartCoroutine(MoveCoroutine(target,state));
    }
    IEnumerator MoveCoroutine(Vector2 target,State state)
    {
        Vector2 tar = (target - startPostion).normalized;
        while(Vector2.Distance(new Vector2(transform.position.x,transform.position.y),target)>0.2f)
        {
            Vector2 vec = tar * speed * Time.deltaTime;
            transform.position += new Vector3(vec.x, vec.y, 0);
            yield return null;
        }
        this.state = state;
        if(state==State.StayLeft)
        {
            StartCoroutine(StayCoroutine(State.MoveRight));
        }
        else if (state==State.StayRight)
        {
            StartCoroutine(StayCoroutine(State.MoveLeft));
        }
    }
    IEnumerator StayCoroutine(State state)
    {
        yield return new WaitForSeconds(stayTime);
        this.state = state;
    }
}
