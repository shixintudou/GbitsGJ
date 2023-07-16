using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHJ_D : PlayerHJ
{
    public override void Dead()
    {
        print("dead"+dead);
        if (!dead)
        {
            dead = true;
            var Body = Instantiate(ResoucesManager.Instance.Resouces["PlayerBody"], this.transform.position, Quaternion.identity);
            Body.transform.rotation = Quaternion.Euler(0, 0, 90f);
            GameMode.Instance.playerDeathSection = GameMode.Instance.timeSectionManager.NowTimeSection;

            GameMode.Instance.RespawnPlayer(false);
            Destroy(this.gameObject);
        }
    }
}
