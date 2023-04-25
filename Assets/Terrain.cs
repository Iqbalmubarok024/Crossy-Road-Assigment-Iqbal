using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    private void Start()
    {
        Generate(9);
    }
    public virtual void Generate(int size)
    {
        if(size == 0)
            return;

        if((float) size % 2 == 0)
            size -= 1;

        int limit = Mathf.FloorToInt((float) size / 2);

        for (int i = -limit ; i <= limit ; i++)
        {
            var go = Instantiate(tilePrefab, transform);

            go.transform.localPosition = new Vector3(i,0,0);
        }

       
    }
}
