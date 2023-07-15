using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSectionButton : MonoBehaviour
{
    Button button;
    public Button Button { get => button; }
    TimeSectionManager timeSectionManager;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        timeSectionManager = transform.parent.GetComponent<TimeSectionManager>();
        if (!button || !timeSectionManager)
        {
            print("¿‡π“‘ÿ¥ÌŒÛ!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
