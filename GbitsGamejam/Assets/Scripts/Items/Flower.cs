using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Color> colors = new List<Color>();
    private void OnEnable()
    {
        StartCoroutine(ShowCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ShowCoroutine()
    {
        float t = Random.Range(0.1f, 1.5f);
        yield return new WaitForSeconds(t);
        int i = Random.Range(0, colors.Count);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Animator>().enabled = true;
        GetComponent<SpriteRenderer>().color = colors[i];
    }
}
