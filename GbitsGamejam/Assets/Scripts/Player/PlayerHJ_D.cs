using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHJ_D : PlayerHJ
{
    public override void Dead()
    {
        print("dead"+dead);
        if (!dead&&GameMode.GamePlayMode!=GamePlayMode.Replay)
        {
            dead = true;
            var Body = Instantiate(ResoucesManager.Instance.Resouces["PlayerBody"], this.transform.position, Quaternion.identity);
            Body.transform.rotation = Quaternion.Euler(0, 0, 90f);
            GameMode.Instance.playerDeathSection = GameMode.Instance.timeSectionManager.NowTimeSection;
            //转场效果
            RendererFeatureManager.instance.TransitionForSeconds(0.3f, 1);
            GameMode.Instance.RespawnPlayer(false);
            Destroy(this.gameObject);
        }
    }
}
