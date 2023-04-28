using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    static HashSet<Vector3> positionsSet = new HashSet<Vector3>();

    public static HashSet<Vector3> AllPositions { get => new HashSet<Vector3> (positionsSet); }

    private void OnEnable()
    {
        positionsSet.Add(this.transform.position);
    }

    private void OnDesable()
    {
        positionsSet.Remove(this.transform.position);
    }
}
