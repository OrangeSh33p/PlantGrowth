using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPart : MonoBehaviour
{
    public enum Type { Leaf, Stem, Root}

    public Type type;

    public float buildCost;

    public float metabolism;
    public float waterEvaporation;

    public float fotosynthesis;
    public float waterAbsorption;
    public float mineralAbsorption;


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
    }


}
