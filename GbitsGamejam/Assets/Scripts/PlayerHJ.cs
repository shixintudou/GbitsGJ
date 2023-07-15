using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//横板跳跃玩家类
public class PlayerHJ : MonoBehaviour
{
    //public enum State
    //{
    //    Idle,Move,OnAir
    //}

    [Header("移动数据")]
    public float moveSpeed = 7f;
    public float jumpforce = 5f;
    public float jumpCompensate = 2f;
    public int maxJumpTimes = 2;

    int nowJumpTimes = 0;
    bool isOnGround = true;

    [SerializeField]
    private float pickRange;
    //State state = State.Move;

    Transform footTrans;
    [HideInInspector]
    public Rigidbody2D rb;
    Coroutine moveCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footTrans = transform.Find("foot");
        if (footTrans == null)
            print("未获取playerfoot");
        ReplayManager.instance.SetDataNum(GameMode.Instance.TimeSectionNum);
    }
    void Update()
    {
        if (GameMode.GamePlayMode == GamePlayMode.Play)
        {
            ControlMoveMentUpdate();
            Pick();
        }
    }
    private void FixedUpdate()
    {
        if (GameMode.GamePlayMode == GamePlayMode.Play)
        {
            ControlMoveMentFixupdate();
        }
    }
    public void ControlMoveMentUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nowJumpTimes < maxJumpTimes)
        {
            // rb.AddForce(Vector2.up * jumpforce);
            Jump();
        }
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        nowJumpTimes++;
    }

    public void ControlMoveMentFixupdate()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.1 && CheckIsOnGround())
        {
            if (!isOnGround)
                nowJumpTimes = 0;
            isOnGround = true;
        }
        else
            isOnGround = false;
        if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * jumpCompensate * Time.fixedDeltaTime;
        }
        //水平速度
        float y = 0;
        if (!isOnGround)
        {
            y = rb.velocity.y;
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, y);
    }
    public void MoveWithTargetAndTime(float target,float time)
    {
        if(moveCoroutine!=null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveCoroutine(target, time));
    }
    
    public void Pick()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D pickCol = Physics2D.OverlapBox(transform.position, new Vector2(pickRange, pickRange), 0, 0B1000);
            print(pickCol);
            if (pickCol)
            {
                BasePick pick = pickCol.GetComponent<BasePick>();
                if (pick && pick.CheckCondition())
                {

                    Dialog.instance.BeginShowDialog(pick.transform, pick.description, pick.OnDialogOver);
                }
            }
        }
    }

    bool CheckIsOnGround()
    {
        var bisOnGround = Physics2D.OverlapCircle(footTrans.position, 0.2f, LayerMask.GetMask("Ground"));
        return bisOnGround;
    }

    IEnumerator MoveCoroutine(float target, float time)
    {
        float t = 0;
        while(t<time)
        {
            rb.velocity = new Vector2(target * moveSpeed, rb.velocity.y);
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

}
