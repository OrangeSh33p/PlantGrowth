using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An extremity of a chunk, that other chunks can attach to
public class Tip : MonoBehaviour {

    public PlantPart.Type generatedType; //type of generated plant part 
    [HideInInspector] public PlantPart parentPart;
    public float angle;
   

    public void Destroy()
    {
        parentPart.tips.Remove(this);
        Destroy(this.gameObject);
    }


    private void Awake()
    {
       // parentPart.tips.Add(this);
    }

    public void Grow()
    {
       
            GameObject newPartPrefab = PlantManager.instance.GetPartPrefab(generatedType);

            Instantiate(
                newPartPrefab,
                transform.position,
                transform.rotation = Quaternion.Euler(0, 0, angle),             
                transform.parent
            );
            Destroy();

        PlantManager.instance.GrowPart(newPartPrefab.GetComponent<PlantPart>());
        
    }


}
