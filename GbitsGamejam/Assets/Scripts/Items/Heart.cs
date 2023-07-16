using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : BasePick
{
    // Start is called before the first frame update
    float len = 0.5f;
    Vector2 startPos;
    public GameObject film;
    void Start()
    {
        RendererFeatureManager.instance.SetLineActive(true);
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void BePicked()
    {
        base.BePicked();
        film.SetActive(false);
        RendererFeatureManager.instance.SetLineActive(false);
        RendererFeatureManager.instance.ShakeForSeconds(0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BePicked();
            collision.GetComponent<Animator>().SetBool("Heart", true);
            ActManager.instance.StartCoroutine(ActManager.instance.ActMoveCoroutine(5f, 1, "ActScene3"));
            Destroy(gameObject);
        }
    }
}
