using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int defenderCost = 75;
    [SerializeField] float buildDelay = 1f;

    private string containerName = "ContainerForRuntimeSpawns";


    private void Start()
    {
        StartCoroutine(BuildDefender());
    }



    public bool SpawnDefender(Defender defenderPrefab, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null) return false;

        if(bank.CurrentBalance >= defenderCost)
        {
            bank.Withdraw(defenderCost);
            Instantiate(defenderPrefab.gameObject, position, Quaternion.identity, GameObject.Find(containerName).transform);
            return true;
        }
        return false;
    }

    private IEnumerator BuildDefender()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }
}
