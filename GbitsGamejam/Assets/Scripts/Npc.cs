using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Npc : BasePick
{
    bool talked = false;
    List<string> talkedDialog = new List<string>()
    {
        "...",
    };





    DialogHint dialogHint;

    void Start()
    {
        dialogHint = GetComponentInChildren<DialogHint>();
        canPick = false;
    }

    

    public override void OnDialogOver()
    {
        base.OnDialogOver();

        talked = true;
        description = talkedDialog;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && CheckCondition() && !talked)
        {
            dialogHint.Show();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogHint.Hide();
        }
    }
}
