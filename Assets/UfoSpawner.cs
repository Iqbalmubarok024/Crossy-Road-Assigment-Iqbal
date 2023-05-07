using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawner : MonoBehaviour
{
    [SerializeField] Ufo ufo;
    [SerializeField] Duck beaver;
    [SerializeField] float initialTimer;

    float timer;
    // Start is called before the first frame update


    void Start()
    {
        timer = initialTimer;
        ufo.gameObject.SetActive(false);
        beaver.SetMoveable(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer<=0 && ufo.gameObject.activeInHierarchy == false)
            {
                ufo.gameObject.SetActive(true);
                ufo.transform.position = beaver.transform.position + new Vector3(0,0,20);
            }
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = initialTimer;
    }
}
