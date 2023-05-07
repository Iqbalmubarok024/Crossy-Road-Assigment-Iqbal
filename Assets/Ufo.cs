using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField, Range(0,50)] float speed = 25;



    void Update()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        
    }


}
