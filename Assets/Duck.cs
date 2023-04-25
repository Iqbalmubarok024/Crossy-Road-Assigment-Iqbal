using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Duck : MonoBehaviour
{
    [SerializeField] bool isMoving;
    [SerializeField]  float durasi = 0.1f;
    void Start()
    {
        
    }

    void Update()
    {
        if (DOTween.IsTweening(transform))
        {
            return;
        }

        Vector3 direction = Vector3.zero;
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            direction += Vector3.back;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        { 
            direction += Vector3.right;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            direction += Vector3.left;
        }

        if (direction == Vector3.zero)
        {
            return;
        }
        Move(direction);
        
    }
    public void Move(Vector3 direction)
    {
         transform.DOMove(transform.position + direction, durasi);
    }
}
