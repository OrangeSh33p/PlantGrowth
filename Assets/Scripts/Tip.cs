using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An extremity of a chunk, that other chunks can attach to
public class Tip : MonoBehaviour {

    public PlantPart.Type generatedType; //type of generated plant part 
    public PlantPart parentPart;
    public PlantGrower grower;

    public void Destroy()
    {
        parentPart.tips.Remove(this);
        Destroy(this.gameObject);
    }


    private void Awake()
    {
        parentPart.tips.Add(this);
    }

    public void Grow(float angle)
    {
       
          
            GameObject newPartPrefab = grower.GetPartPrefab(generatedType);

            Instantiate(
                newPartPrefab,
                transform.position,
                transform.rotation * Quaternion.Euler(0, 0, angle),
                transform.parent
            );
            Destroy();
        
    }

}
