using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceManager : MonoBehaviour
{
    [SerializeField] TMP_Text balanceTextField;
    [SerializeField] TMP_Text betSizeTextField;
    [SerializeField] TMP_Text freeSpinsTextField;
    [SerializeField] TMP_Text winTextField;
    [SerializeField] float startingBalance = 10000f;
    [SerializeField] float startingBetSize = 10;
    private float betSize = 0f;
    private int freeSpins = 0;
    private float balance = 0f;

    private const string BALANCE_KEY = "Balance";
    private const string FREESPINS_KEY = "FreeSpins";


    private void Start()
    {
        balance = PlayerPrefs.HasKey(BALANCE_KEY) ? PlayerPrefs.GetFloat(BALANCE_KEY) : startingBalance;
        freeSpins = PlayerPrefs.HasKey(FREESPINS_KEY) ? PlayerPrefs.GetInt(FREESPINS_KEY) : 0;
        betSize = startingBetSize;
        balanceTextField.text = "Balance:" + balance;
        betSizeTextField.text = "Bet Size:" + betSize;
        freeSpinsTextField.text = "Free Spins:" + freeSpins;
    }


    private void OnEnable()
    {
        SlotMachine.OnSlotsSpinned += SubtractBalance;
        SlotMachine.OnWinCombinationBalance += AddBalance;
        SlotMachine.OnWinCombinationFreeSpins += AddFreeSpins;
    }


    private void OnDisable()
    {
        SlotMachine.OnSlotsSpinned -= SubtractBalance;
        SlotMachine.OnWinCombinationBalance -= AddBalance;
        SlotMachine.OnWinCombinationFreeSpins -= AddFreeSpins;
    }


    public void AddBalance(float coefficient)
    {
        if (coefficient <= 0 || betSize * coefficient > balance) return;

        float prize = betSize * coefficient;
        balance += prize;
        balanceTextField.text = "Balance:" + balance;
        winTextField.text = "You won:" + prize;

        SaveBalance();
    }


    public void SubtractBalance()
    {
        if (betSize <= 0 || balance <= 0 || betSize > balance) return;

        if (freeSpins > 0)
        {
            SubtractFreeSpins();
        }
        else
        {
            balance -= betSize;
            balanceTextField.text = "Balance:" + balance;

            SaveBalance();

        }
    }


    public void AddFreeSpins(int freeSpinsToAdd)
    {
        if (freeSpinsToAdd <= 0) return;

        freeSpins += freeSpinsToAdd;
        freeSpinsTextField.text = "Free Spins:" + freeSpins;
        winTextField.text = "You won:" + freeSpinsToAdd + "free spins";

        SaveFreeSpins();
    }


    public void SubtractFreeSpins()
    {
        if (freeSpins <= 0) return;
        freeSpins--;
        freeSpinsTextField.text = "Free Spins:" + freeSpins;

        SaveFreeSpins();
    }


    public void AddBetSize(float betSizeToAdd)
    {
        if (betSizeToAdd <= 0 || freeSpins > 0 || betSize + betSizeToAdd < 0) return;

        betSize += betSizeToAdd;
        betSizeTextField.text = "Bet Size:" + betSize;
    }


    public void SubtractBetSize(float betSizeToSubtract)
    {
        if (betSizeToSubtract <= 0 || betSize <= 0 || betSize - betSizeToSubtract < 0 || freeSpins > 0) return;

        betSize -= betSizeToSubtract;
        betSizeTextField.text = "Bet Size:" + betSize;
    }


    private void SaveFreeSpins()
    {
        PlayerPrefs.SetInt(FREESPINS_KEY, freeSpins);
        PlayerPrefs.Save();
    }


    private void SaveBalance()
    {
        PlayerPrefs.SetFloat(BALANCE_KEY, balance);
        PlayerPrefs.Save();
    }
}
