using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSectionButton : MonoBehaviour
{
    int index;
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
        var childCount = timeSectionManager.transform.childCount;
        for (int i = 0; i < childCount; i++)
            if (timeSectionManager.transform.GetChild(i).gameObject == this.gameObject)
            {
                index = i+1;
                break;
            }
        if (index == 0)
        {
            print("ŒÔÃÂπ“‘ÿ¥ÌŒÛ£°");
        }
        //◊¢≤·
        timeSectionManager.RegisterTimeButton(this);
        button.onClick.AddListener(OnButtonClick);
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnButtonClick()
    {
        print(index);
        timeSectionManager.TrySelectTimeSectionIndex(index);
    }
}
