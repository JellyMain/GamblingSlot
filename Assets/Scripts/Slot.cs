using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour
{
    public Action<SymbolSO> OnSpinEnded;

    [SerializeField] SymbolSO[] possibleSymbols;
    [SerializeField] float spinTime = 2f;
    [SerializeField] float timeBetweenSymbolChange = 0.1f;
    [SerializeField] Image slotSprite;
    private SymbolSO currentSymbol;
    private bool isSpinning = false;


    private int GetRandomArrayElement()
    {
        int randomNumber = UnityEngine.Random.Range(0, possibleSymbols.Length);
        return randomNumber;
    }


    private void ChangeSymbol()
    {
        SymbolSO randomSymbol = possibleSymbols[GetRandomArrayElement()];
        while (currentSymbol == randomSymbol)
        {
            randomSymbol = possibleSymbols[GetRandomArrayElement()];
        }
        slotSprite.sprite = randomSymbol.symbolSprite;
        currentSymbol = randomSymbol;
    }


    private void SetSymbol()
    {
        SymbolSO randomSymbol = possibleSymbols[GetRandomArrayElement()];
        slotSprite.sprite = randomSymbol.symbolSprite;
        currentSymbol = randomSymbol;
    }


    public void StartSpinning()
    {
        StartCoroutine(Spin());
    }


    IEnumerator Spin()
    {
        if (!isSpinning)
        {
            isSpinning = true;
            float endTime = Time.time + spinTime;

            while (endTime > Time.time)
            {
                ChangeSymbol();
                SoundManager.Instance.PlayChangeSymbolSound();
                yield return new WaitForSeconds(timeBetweenSymbolChange);
            }
            SetSymbol();
            OnSpinEnded?.Invoke(currentSymbol);
            isSpinning = false;
        }
    }
}
