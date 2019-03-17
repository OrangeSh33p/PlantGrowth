using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blossom : MonoBehaviour
{

    Vector3 baseScale;

    Color color;
    SpriteRenderer spriteRenderer;
    ParticleSystem particles;
    ParticleSystem.MainModule psmain;


    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();
       // psmain = particles.main;

        color = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);

        spriteRenderer.color = color;
       // psmain.startColor = color;

        //GetComponentInChildren<ChangeColor>().ChangeCOlor(color);
          
        baseScale = transform.localScale;
        StartCoroutine(EntryAnimation());
        StartCoroutine(Win());
        transform.parent = PlantManager.instance.transform;
           
    }

 
    public IEnumerator Win()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Win!");
        PlantManager.instance.Win();
    }

   public IEnumerator EntryAnimation()
    {
        float speed = 0.6f;
        float s = 0;
        while (s < 1)
        {
            transform.localScale = baseScale *s;
            s += speed * Time.deltaTime;
            yield return null;
        }
    }
  
}
