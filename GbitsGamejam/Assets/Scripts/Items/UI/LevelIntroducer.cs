using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroducer : MonoBehaviour
{
    public Sprite[] LevelIntroduces;
    private static LevelIntroducer instance;
    public static LevelIntroducer Instance
    {
        get
        {
            return instance;
        }
    }
    Image image;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        image = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetIntroduceImageAndEnable(int index)
    {
        if (index < 0 && index >= LevelIntroduces.Length) return;
        GameMode.Instance.SetGameMode(GamePlayMode.UIInteract);
        image.sprite = LevelIntroduces[index];
        gameObject.SetActive(true);
        Invoke("DisableThis", 3f);
    }
    public void DisableThis()
    {
        gameObject.SetActive(false);
        GameMode.Instance.SetGameMode(GamePlayMode.Play);
    }
}
