using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
   [SerializeField] Grass grassPrefab;
   [SerializeField] Road roadPrefab;
   [SerializeField] int initialGrassCount = 5;
   [SerializeField] int horizontalSize;
   [SerializeField] int backViewDistance = -4;
   [SerializeField] int forwardViewDistance = 15;
   [SerializeField, Range(0,1)] float treeProbability;

   private void Start()
   {
    // create initial Grass
    for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
    {
        var grass = Instantiate(grassPrefab);

        grass.transform.localPosition = new Vector3(0,0,zPos);

        grass.SetTreePercentage(zPos < -1 ? 1 : 0);

        grass.Generate(horizontalSize);
    }

    // 
    for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
    {
        var terrain = Instantiate(roadPrefab);

        terrain.transform.localPosition = new Vector3(0,0,zPos);

        terrain.Generate(horizontalSize);
    }
   }
}
