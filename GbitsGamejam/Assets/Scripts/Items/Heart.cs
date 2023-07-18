using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : BasePick
{
    // Start is called before the first frame update
    float len = 0.5f;
    Vector2 startPos;
    public GameObject film;
    public float intensity;
    public GameObject ground;
    public GameObject group;
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
        RendererFeatureManager.instance.ShakeForSecondsWithIntensity(0.5f, intensity);
        ground.GetComponent<SpriteRenderer>().enabled = false;
        Flower();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BePicked();
            collision.GetComponent<Animator>().SetBool("Heart", true);
            StartCoroutine(PickCoroutine());
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public void Flower()
    {
        group.SetActive(true);
        
    }
    IEnumerator PickCoroutine()
    {
        yield return new WaitForSeconds(3f);
        ActManager.instance.StartCoroutine(ActManager.instance.ActMoveCoroutine(5f, 1, "ActScene3"));
        Destroy(gameObject);
    }

}
