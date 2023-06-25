using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SlotMachine : MonoBehaviour
{
    public static Action OnSlotsSpinned;
    public static Action<float> OnWinCombination;
    [SerializeField] Slot[] slots;
    [SerializeField] WinningCombinationsSO[] winningCombinations;
    private SymbolSO[] currentCombination;
    private int slotsSpinned = 0;


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
        foreach (Slot slot in slots)
        {
            slot.StartSpinning();
        }
        OnSlotsSpinned?.Invoke();
    }


    private void HandleSpinEnded(SymbolSO symbol)
    {
        currentCombination[slotsSpinned] = symbol;
        slotsSpinned++;
        if (slotsSpinned == slots.Length)
        {
            CheckCombinations();
            slotsSpinned = 0;
        }

    }

    private void CheckCombinations()
    {
        foreach (WinningCombinationsSO combination in winningCombinations)
        {
            if (currentCombination.SequenceEqual(combination.winningCombiantion))
            {
                OnWinCombination?.Invoke(combination.coefficient);
                Debug.Log("WOOOOOOOOOOOOOOOOOON");
            }

        }
    }

}
