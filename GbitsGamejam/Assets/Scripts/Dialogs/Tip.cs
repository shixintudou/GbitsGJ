using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public DialogHint dialogHint;
    public DialogHint hint2;
    // Start is called before the first frame update
    void Start()
    {
        //dialogHint = GetComponentInChildren<DialogHint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogHint.Show();
            hint2.Show();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogHint.Hide();
            hint2.Hide();
        }
    }
}
