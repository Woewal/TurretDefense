using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : BuildingComponent
{
    [SerializeField] float interval;

    private void Start()
    {
        StartCoroutine(Mine());
    }

    IEnumerator Mine()
    {
        while(true)
        {
            float currentTime = 0f;
            while (currentTime < interval)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }
            GlobalController.instance.levelController.AddScrap(10);
        }
    }
}
