using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{

    PlantGrower plantGrowthManager;

    public static PlantManager instance;

    float waterLoseThreshold;
    float growPotentialLoseThreshold;
    float waterWinThreshold;
    float growPotentialWinThreshold;

    float numberLeaves;

    
    //plant stats
    float water;  //between 0 and 1
    float growthPotential; //between 0 and 1

    [HideInInspector] public List<PlantPart> plantParts;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        plantGrowthManager = GetComponent<PlantGrower>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    Tip randomTip = RandomPlantPart().RandomTip();
        //    if (randomTip)
        //        plantGrowthManager.Grow(randomTip, 0);
        //}

        CheckForWinLoseConditions();
    }


    //public PlantPart RandomPlantPart()
    //{
    //    return plantParts[Random.Range(0, plantParts.Count)];
    //}

  

    public void NextTimeStep()
    {
        UpdatePlantStats();

        WeatherManager.instance.SelectRandomWeather();
      
    }


  

    public void UpdatePlantStats()
    {
        foreach( PlantPart part in plantParts)
        {
            growthPotential -= part.metabolism;
            growthPotential += part.fotosynthesis;
            growthPotential += part.mineralAbsorption;
            growthPotential = Mathf.Clamp(growthPotential, 0, 1);


            water -= part.waterEvaporation * WeatherManager.instance.luminosity;
            water += part.waterAbsorption * WeatherManager.instance.soilHumidity;
        }
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

        else if (water < waterLoseThreshold || growthPotential < growPotentialWinThreshold)
            OnLose();
    }

    public void Blossom()
    {

    }

    public void OnLose()
    {

    }
}
