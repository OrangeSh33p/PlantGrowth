using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
  

    float waterLoseThreshold;
    float growPotentialLoseThreshold;
    float waterWinThreshold;
    float growPotentialWinThreshold;

    float numberLeaves;

    
    //plant stats
    float water;  //between 0 and 1
    float growthPotential; //between 0 and 1

    List<PlantPart> plantParts;

    void Start()
    {
        
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
