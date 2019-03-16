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

}
