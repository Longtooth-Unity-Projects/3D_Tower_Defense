using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 25;

    //cached references
    private Bank bank;


    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public bool RewardGold()
    {
        if(bank == null) { return false; }

        bank.Deposit(goldReward);
        return true;
    }

    public bool TakeGold()
    {
        if (bank == null) { return false; }

        bank.Withdraw(goldPenalty);
        return true;
    }
}
