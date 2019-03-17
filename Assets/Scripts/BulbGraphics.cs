using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbGraphics : MonoBehaviour
{
    public float minScale = 0.7f;
    public float maxScale = 1.3f;
    
    Vector3 baseScale;

    public AudioClip shrivelSound;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        baseScale = transform.localScale;
    }

    public Sprite[] bulbSprites;

    public void UpdateGraphics()
    {

        int bulbShrivelingGraphicalStage = 2;
        if (PlantManager.instance.water < 0.1)
        {
            if (bulbShrivelingGraphicalStage != 0)
                PlantManager.instance.GetComponent<AudioSource>().PlayOneShot(shrivelSound);
            bulbShrivelingGraphicalStage = 0;

        }
        else if (PlantManager.instance.water < 0.3)
        {
            if (bulbShrivelingGraphicalStage == 2)
                PlantManager.instance.GetComponent<AudioSource>().PlayOneShot(shrivelSound);
            bulbShrivelingGraphicalStage = 1;
        }
        else
            bulbShrivelingGraphicalStage = 2;

        //(int)Mathf.Floor(PlantManager.instance.water / (1f / bulbSprites.Length));
        // Debug.Log("humidity graphical stage: " + humidityGraphicalStage);
        //if (bulbShrivelingGraphicalStage == bulbSprites.Length)
        //    bulbShrivelingGraphicalStage--;

        spriteRenderer.sprite = bulbSprites[bulbShrivelingGraphicalStage];

        float newScaleFactor = Mathf.Lerp(minScale, maxScale, PlantManager.instance.growthPotential);


        transform.localScale = baseScale * newScaleFactor;
    }
}
