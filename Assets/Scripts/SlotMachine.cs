using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SlotMachine : MonoBehaviour
{
    public static Action OnSlotsSpinned;
    public static Action<int> OnWinCombinationFreeSpins;
    public static Action<float> OnWinCombinationBalance;

    [SerializeField] Slot[] slots;
    [SerializeField] WinningCombinationsSO[] winningCombinations;
    private SymbolSO[] currentCombination;
    private int slotsSpinned = 0;
    private bool isSpinning = false;


    private void Start()
    {
        currentCombination = new SymbolSO[slots.Length];
    }


    private void OnEnable()
    {
        foreach (Slot slot in slots)
        {
            slot.OnSpinEnded += HandleSpinEnded;
        }
    }


    private void OnDisable()
    {
        foreach (Slot slot in slots)
        {
            slot.OnSpinEnded -= HandleSpinEnded;
        }
    }


    public void SpinAllSlots()
    {
        if (!isSpinning)
        {
            isSpinning = true;
            foreach (Slot slot in slots)
            {
                slot.StartSpinning();
            }
            OnSlotsSpinned?.Invoke();
        }
    }


    private void HandleSpinEnded(SymbolSO symbol)
    {
        currentCombination[slotsSpinned] = symbol;
        slotsSpinned++;
        if (slotsSpinned == slots.Length)
        {
            CheckCombinations();
            slotsSpinned = 0;
            isSpinning = false;
        }

    }


    private void CheckCombinations()
    {
        foreach (WinningCombinationsSO combination in winningCombinations)
        {
            if (currentCombination.SequenceEqual(combination.winningCombiantion))
            {
                if (combination.freeSpins > 0)
                {
                    OnWinCombinationFreeSpins?.Invoke(combination.freeSpins);
                    SoundManager.Instance.PlayWinSound();
                }
                else
                {
                    OnWinCombinationBalance?.Invoke(combination.coefficient);
                    SoundManager.Instance.PlayWinSound();
                }
            }

        }
    }

}
