using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An extremity of a chunk, that other chunks can attach to
public class Tip : MonoBehaviour {

    public PlantPart.Type generatedType; //type of generated plant part 
    [HideInInspector] public PlantPart parentPart;
    public float angle;


    public Vector3 worldScale;
    Transform parent;

    public void Destroy()
    {
        parentPart.tips.Remove(this);
        Destroy(this.gameObject);
    }


    private void Awake()
    {
        // parentPart.tips.Add(this);

        //worldScale = transform.lossyScale;
        parent = transform.parent;
    }

    private void Update()
    {
        transform.parent = null;
        transform.localScale = worldScale;
        transform.parent = parent;
    }

    public void OnClick()
    {
        if (!PlantManager.instance.isGameOver)
            Grow();
    }

    public void Grow()
    {
       
        GameObject newPartPrefab = PlantManager.instance.GetPartPrefab(generatedType);

        PlantPart part = Instantiate(
            newPartPrefab,
            transform.position,
            transform.rotation = Quaternion.Euler(0, 0, angle),
            //transform.parent
            PlantManager.instance.transform
        ).GetComponent<PlantPart>();

        
        part.yScaleFactor =Mathf.Lerp(0.6f, 1.2f, PlantManager.instance.growthPotential);
        //part.xScaleFactor =Mathf.Lerp(1.4f, 0.8f, PlantManager.instance.water);

        PlantManager.instance.GrowPart(newPartPrefab.GetComponent<PlantPart>());
        
        Destroy();


      
        
    }


}
