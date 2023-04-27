using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Duck beaver;
    [SerializeField] List<Terrain> terrainsList;
   [SerializeField] int initialGrassCount = 5;
   [SerializeField] int horizontalSize;
   [SerializeField] int backViewDistance = -4;
   [SerializeField] int forwardViewDistance = 15;
   [SerializeField, Range(0,1)] float treeProbability;
   
   Dictionary<int,Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    
    [SerializeField] int travelDistance;
   private void Start()
   {
    // create initial Grass
    for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
    {
        var terrain = Instantiate(terrainsList[0]);

        terrain.transform.localPosition = new Vector3(0,0,zPos);

        if(terrain is Grass grass)
        grass.SetTreePercentage(zPos < -1 ? 1 : 0);

        terrain.Generate(horizontalSize);
        activeTerrainDict[zPos] = terrain;
    }

    // 
    for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
    {
        var terrain = SpawnRandomTerrain(zPos);


        terrain.Generate(horizontalSize);

        activeTerrainDict[zPos] = terrain;
    }

    SpawnRandomTerrain(0);
   }

    private Terrain SpawnRandomTerrain(int zPos)
   {
        Terrain terrainCheck = null;
        int randomIndex;
        Terrain terrain = null;
        
        for (int z = -1; z >= -3; z--)
        {
            var checkPos = zPos + z;
            if (terrainCheck == null)
            {
                terrainCheck = activeTerrainDict[checkPos];
                continue;
            }
            else if(terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
            {
                randomIndex = Random.Range(0,terrainsList.Count);
                terrain = Instantiate(terrainsList[randomIndex]);
                terrain.transform.localPosition = new Vector3(0,0,zPos);
                return terrain;

            }
            else
            {
                continue;
            }
        }

        var CandidateTerrain = new List<Terrain>(terrainsList);
        for (int i = 0; i < CandidateTerrain.Count; i++)
        {
            if(terrainCheck.GetType() == CandidateTerrain[i].GetType())
            {
                CandidateTerrain.Remove(CandidateTerrain[i]);
                break;
            }
        }

         randomIndex = Random.Range(0,CandidateTerrain.Count);
         terrain = Instantiate(CandidateTerrain[randomIndex]);
         terrain.transform.position = new Vector3(0,0,zPos);
            return terrain;
   }

   private void Update()
   {
        if(beaver.transform.position.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(beaver.transform.position.z);
        }
   }
   public void UpdateTravelDistance(Vector3 targetPosition)
   {
    if(targetPosition.z > travelDistance)
    {
        travelDistance = Mathf.CeilToInt(targetPosition.z);
    }
   }
}
