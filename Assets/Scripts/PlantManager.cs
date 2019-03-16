using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{

    PlantGrower plantGrowthManager;

    public static PlantManager instance;

    float waterLoseThreshold = 0.01f;
    float growPotentialLoseThreshold = 0.01f;
    float waterWinThreshold = 0.3f;
    float growPotentialWinThreshold = 0.95f;

    float numberLeaves;

    
    //plant stats
    float water = 0.5f;  //between 0 and 1
    float growthPotential = 0.3f; //between 0 and 1

    [HideInInspector] public List<PlantPart> plantParts;

    [Header("Plant part prefabs")]
    public GameObject leafPrefab;
    public GameObject rootPrefab;
    public GameObject stemPrefab;
    public GameObject stemWithLeafPrefab;


    private void Awake()
    {
        instance = this;

        
    }

    void Start()
    {
        plantGrowthManager = GetComponent<PlantGrower>();
        water = 0.5f;
        growthPotential = 0.5f;
    }

    void Update()
    {

        CheckForWinLoseConditions();
    }


    public void NextTimeStep()
    {
        UpdatePlantStats();

        WeatherManager.instance.SelectRandomWeather();
      
    }

   



    public GameObject GetPartPrefab(PlantPart.Type type)
    {
        switch (type)
        {
            case PlantPart.Type.Leaf:
                return leafPrefab;
            case PlantPart.Type.Root:
                return rootPrefab;
            case PlantPart.Type.Stem:
                if (growthPotential <= 0.3)
                    return stemPrefab;
                else
                    return stemWithLeafPrefab;
            default:
                return null;
        }
    }



    public void UpdatePlantStats()
    {
        foreach( PlantPart part in plantParts)
        {
            growthPotential -= part.metabolism;
            growthPotential += part.fotosynthesis;
         //   growthPotential += part.mineralAbsorption;
            growthPotential = Mathf.Clamp(growthPotential, 0, 1);


            water -= part.waterEvaporation * WeatherManager.instance.luminosity;
            water += part.waterAbsorption * WeatherManager.instance.soilHumidity;
            water = Mathf.Clamp(water, 0, 1);

            
        }

        Debug.Log("growth potential: " + growthPotential);
        Debug.Log("water: " + water);
    }


    public void GrowPart(PlantPart part)
    {
        growthPotential = Mathf.Clamp(growthPotential - part.buildCost, 0, 1);
        plantParts.Add(part);
        NextTimeStep();
    }

    public void CheckForWinLoseConditions()
    {
        if (water >= waterWinThreshold && growthPotential >= growPotentialWinThreshold)
            Blossom();


        else if (water <= waterLoseThreshold || growthPotential <= growPotentialLoseThreshold)
        {
            //Debug.Log(water);
            //Debug.Log(waterLoseThreshold);
            //Debug.Log(growthPotential);
            //Debug.Log(growPotentialLoseThreshold);
            OnLose();
        }
    }

    public void Blossom()
    {
        Debug.Log("blossom");
    }

    public void OnLose()
    {
        Debug.Log("you lose!");
    }
}
