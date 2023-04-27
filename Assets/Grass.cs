using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Terrain
{

    [SerializeField] GameObject treePrefab;

    public override void Generate(int size)
    {
        base.Generate(size);

        var limit = Mathf.FloorToInt((float)size / 2);
        var treeCount = Mathf.FloorToInt((float)size / 3);

        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);
        }

        for (int i = 0; i < treeCount; i++)
        {
            var randomIndex = Random.Range(0,emptyPosition.Count-1);
            var pos = emptyPosition[randomIndex];
            emptyPosition.RemoveAt(randomIndex);

            var tree = Instantiate(treePrefab,transform);
            
            tree.transform.localPosition = new Vector3(pos,0,0);
        }
    }
}
