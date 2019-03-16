using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPart : MonoBehaviour
{
    public enum Type { Leaf, Stem, Root, Bulb}

    public Type type;

    public float buildCost;

    public float metabolism;
    public float waterEvaporation;

    public float fotosynthesis;
    public float waterAbsorption;
    public float mineralAbsorption;

    public AudioClip growthClip;

    public GameObject blossom;


    public List<Tip> tips; //The tips of this plant part

    public Tip RandomTip()
    {
        int rand = Random.Range(0, tips.Count);
        Debug.Log("random tip number" + rand, this);
        return tips[rand];
    
    }


    private void Start()
    {
        PlantManager.instance.plantParts.Add(this);
        tips = new List<Tip>(GetComponentsInChildren<Tip>());
        foreach (Tip tip in tips)
        {
            tip.parentPart = this;
        }

        PlantManager.instance.GetComponent<AudioSource>().PlayOneShot(growthClip);

        if (type != Type.Bulb)
            StartCoroutine(GrowCoroutine());
    }

    public IEnumerator GrowCoroutine()
    {
        float speed = 0;
        float time = 0;

        foreach (Tip tip in tips)
        {
            tip.gameObject.SetActive(false);
        }

        if (blossom != null)
            blossom.SetActive(false);

        float initialYScale = transform.localScale.y;

        float s = 0;
        while (s < 1)
        {
            transform.localScale =new Vector3(transform.localScale.x, s * initialYScale, transform.localScale.z);
            speed = (float) (System.Math.Pow(4, time) - 1) * 4;
            s += speed * Time.deltaTime;
            time += Time.deltaTime;

            yield return null;
        }
        transform.localScale = new Vector3(transform.localScale.x, initialYScale, transform.localScale.z);

        foreach (Tip tip in tips)
        {
            tip.gameObject.SetActive(true);
        }

        if (blossom != null)
        {
            blossom.SetActive(true);          
        }
    }
}
