using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPart : MonoBehaviour
{

 
    public enum Type { Leaf, Stem, Root, Bulb}

    [Header("Part data")]
    public Type type;

    public float buildCost;

    public float metabolism;
    public float waterEvaporation;

    public float fotosynthesis;
    public float waterAbsorption;
    public float mineralAbsorption;

    [Header("References to assets")]
    public AudioClip growthClip;
    public AudioClip shrivelClip;
    public Sprite[] sprites;
    public Sprite[] shriveledSprites;

    [Header("References to components or objects")]
    public SpriteRenderer spriteRenderer;
    public GameObject blossom;

    [HideInInspector] public float yScaleFactor = 1;
    [HideInInspector] public float xScaleFactor = 1;

    int spriteNumber;
    bool isSchriveled;

    public bool IsSchriveled
    {
        get { return isSchriveled; }

        set
        {
            isSchriveled = value;

            if (value == true)
            {
                fotosynthesis = 0.001f;
                metabolism = 0.0002f;
            }

            spriteRenderer.sprite = shriveledSprites[spriteNumber];

            PlantManager.instance.GetComponent<AudioSource>().pitch = 1;
            PlantManager.instance.GetComponent<AudioSource>().PlayOneShot(shrivelClip);
        }
    }


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

        if (type == Type.Leaf)
            PlantManager.instance.unshriveledLeaves.Add(this);

      tips = new List<Tip>(GetComponentsInChildren<Tip>());
        foreach (Tip tip in tips)
        {
            tip.parentPart = this;
        }


        if (growthClip != null)
        {
            PlantManager.instance.GetComponent<AudioSource>().pitch = Mathf.Lerp(0.5f, 1.5f, Random.Range(0, 1));
            PlantManager.instance.GetComponent<AudioSource>().PlayOneShot(growthClip);
        }

        if (type != Type.Bulb)
        {
            StartCoroutine(GrowCoroutine());
            ChooseRandomSprite();
        }
    }


    public void ChooseRandomSprite()
    {
        spriteNumber = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[spriteNumber];
        if (Random.Range(0, 1) < 0.5f)
            spriteRenderer.flipX = true;
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

        float baseYScale = transform.localScale.y * yScaleFactor;
        float xScale = transform.localScale.x * xScaleFactor;

        float s = 0;
        while (s < 1)
        {
            transform.localScale =new Vector3(xScale, s * baseYScale, 1);
            speed = (float) (System.Math.Pow(4, time) - 1) * 4;
            s += speed * Time.deltaTime;
            time += Time.deltaTime;

            yield return null;
        }
        transform.localScale = new Vector3(transform.localScale.x, baseYScale, transform.localScale.z);

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
