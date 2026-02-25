using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public int maxCount = 3;
    public int currentCount = 0;
    public GameObject fx;

    public List<GameObject> trList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Treasure"))
        {
            if (!trList.Contains(other.gameObject))
            {
                currentCount++;
                trList.Add(other.gameObject);
                if(currentCount == maxCount)
                {
                    CheckTreasure();
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Treasure"))
        {
            if (trList.Contains(other.gameObject))
            {
                currentCount--;
                trList.Remove(other.gameObject);
            }
        }
    }


    void CheckTreasure()
    {
        fx.SetActive(true);
    }
}
