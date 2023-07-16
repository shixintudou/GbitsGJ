using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public bool followEnable = true;
    public Transform player;
    public Vector2 dis;
    void Start()
    {
        dis = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(followEnable)
        {
            Vector2 vec = new Vector2(player.position.x, player.position.y) + dis;
            transform.position = new Vector3(vec.x, vec.y, transform.position.z);
        }
    }
}
