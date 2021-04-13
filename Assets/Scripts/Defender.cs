using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int defenderCost = 75;

    private string containerName = "ContainerForRuntimeSpawns";

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
}
