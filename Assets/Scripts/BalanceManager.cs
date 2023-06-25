using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceManager : MonoBehaviour
{
    [SerializeField] TMP_Text balanceTextField;
    [SerializeField] TMP_Text betSizeTextField;
    [SerializeField] float startingBalance = 100f;
    [SerializeField] float startingBetSize = 10;
    private float betSize = 0f;
    private float balance = 0f;


    private const string BALANCE_KEY = "Balance";

    private void Start()
    {
        balance = PlayerPrefs.HasKey(BALANCE_KEY) ? PlayerPrefs.GetFloat(BALANCE_KEY) : startingBalance;
        betSize = startingBetSize;
        balanceTextField.text = "Balance:" + balance;
        betSizeTextField.text = "Bet Size:" + betSize;
    }


    private void OnEnable()
    {
        SlotMachine.OnSlotsSpinned += SubtractBalance;
    }


    private void OnDisable()
    {
        SlotMachine.OnSlotsSpinned -= SubtractBalance;
    }


    public void AddBalance(float balanceToAdd)
    {
        if (balanceToAdd <= 0) return;

        balance += balanceToAdd;
        balanceTextField.text = "Balance:" + balance;
        SaveBalance();
    }


    public void SubtractBalance()
    {
        if (betSize <= 0) return;

        balance -= betSize;
        balanceTextField.text = "Balance:" + balance;
        SaveBalance();
    }


    public void AddBetSize(float betSizeToAdd)
    {
        if (betSizeToAdd <= 0) return;

        betSize += betSizeToAdd;
        betSizeTextField.text = "Bet Size:" + betSize;
    }


    public void SubtractBetSize(float betSizeToSubtract)
    {
        if (betSizeToSubtract <= 0) return;

        betSize -= betSizeToSubtract;
        betSizeTextField.text = "Bet Size:" + betSize;
    }


    private void SaveBalance()
    {
        PlayerPrefs.SetFloat(BALANCE_KEY, balance);
        PlayerPrefs.Save();
    }
}
