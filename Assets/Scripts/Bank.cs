using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldDisplayTMP;
    [SerializeField] private int startingBalance = 500;
    [SerializeField] private int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateGoldDisplay();
    }

    public void Deposit(int amountToDeposit)
    {
        currentBalance += Mathf.Abs(amountToDeposit);
        UpdateGoldDisplay();
    }

    public void Withdraw(int amountToDeposit)
    {
        currentBalance -= Mathf.Abs(amountToDeposit);
        UpdateGoldDisplay();

        if(currentBalance < 0)
        {
            //TODO loose condition, add win condition
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateGoldDisplay()
    {
        goldDisplayTMP.text = $"Gold: {currentBalance}";
    }
}
