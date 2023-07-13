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
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footTrans = transform.Find("foot");
        if (footTrans == null)
            print("未获取playerfoot");

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nowJumpTimes < maxJumpTimes)
        {
           // rb.AddForce(Vector2.up * jumpforce);
            rb.velocity =new Vector2(rb.velocity.x,jumpforce);
            nowJumpTimes++;
        }
        Pick();
    }
    private void FixedUpdate()
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
        if(!isOnGround)
        {
            y=rb.velocity.y;
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, y);
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
    private void OnDrawGizmos()
    {
    }

}
