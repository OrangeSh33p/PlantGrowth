using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{

    PlantGrower plantGrowthManager;

    public static PlantManager instance;

    float waterLoseThreshold = 0.01f;
    float growPotentialLoseThreshold = 0.01f;
    float waterWinThreshold = 0.3f;
    float growPotentialWinThreshold = 0.95f;

    float numberLeaves;


    [HideInInspector] public bool isGameOver;

    [Header("End game UI elements")]
    public GameObject endGamePanel;
    public Text endText;

    //plant stats
    [HideInInspector] public float water = 0.5f;  //between 0 and 1
    [HideInInspector] public float growthPotential = 0.3f; //between 0 and 1

    [HideInInspector] public List<PlantPart> plantParts;
    [HideInInspector] public List<PlantPart> unshriveledLeaves;

    [Header("Plant part prefabs")]
    public GameObject leafPrefab;
    public GameObject rootPrefab;
    public GameObject stemPrefab;
    public GameObject stemWithLeafPrefab;
    public GameObject stemWithBlossomPrefab;


    string winText = "You have blossomed!";
    string waterLoseText = "You could not get enough water... Try again!";
    string energyLoseText = "You could not survive... Try again!";


    BulbGraphics bulb;

    private void Awake()
    {
        instance = this;
        endGamePanel.SetActive(false);
        bulb = GetComponentInChildren<BulbGraphics>();
        isGameOver = false;

    }

    void Start()
    {
        plantGrowthManager = GetComponent<PlantGrower>();

       

        water = 0.5f;
        growthPotential = 0.5f;

        //water = 1f;
        //growthPotential = 1f;


        NextTimeStep();
    }

    void Update()
    {

        CheckForLoseConditions();
    }


    public void NextTimeStep()
    {
        UpdatePlantStats();

        bulb.UpdateGraphics();

        WeatherManager.instance.SelectNewWeather();
      
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
                else if (growthPotential > growPotentialWinThreshold && water >= waterWinThreshold)
                    return stemWithBlossomPrefab;
                else
                    return stemWithLeafPrefab;
            default:
                return null;
        }
    }


    public PlantPart ChooseRandomUnshriveledLeaf()
    {
        if (unshriveledLeaves.Count > 0)
        {
            PlantPart leaf = unshriveledLeaves[Random.Range(0, unshriveledLeaves.Count)];           
            return leaf;
        }
        else
            return null;
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


        if (water < 0.3)
        {
            PlantPart leaf = ChooseRandomUnshriveledLeaf();
            if (leaf != null)
            {
                leaf.IsSchriveled = true;
                unshriveledLeaves.Remove(leaf);
            }
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

    public void CheckForLoseConditions()
    {
        //if (water >= waterWinThreshold && growthPotential >= growPotentialWinThreshold)
        //    Blossom();


        if (water <= waterLoseThreshold || growthPotential <= growPotentialLoseThreshold)
        {
            //Debug.Log(water);s
            //Debug.Log(waterLoseThreshold);
            //Debug.Log(growthPotential);
            //Debug.Log(growPotentialLoseThreshold);
            OnLose();
        }
    }

  

    public void OnLose()
    {
        isGameOver = true;
       // Debug.Log("you lose!");
        if (growthPotential <= growPotentialLoseThreshold)
            endText.text = energyLoseText;
        else
            endText.text = waterLoseText;
        endGamePanel.SetActive(true);
    }

    public void Win()
    {
        endText.text = winText;
        endGamePanel.SetActive(true);
       
    }

 
}
