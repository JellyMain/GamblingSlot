using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Slot : MonoBehaviour
{
    public Action<SymbolSO> OnSpinEnded;

    [SerializeField] SymbolSO[] possibleSymbols;
    [SerializeField] float minSpinTime = 2f;
    [SerializeField] float maxSpinTime = 5f;
    [SerializeField] float timeBetweenSymbolChange = 0.1f;
    private Image slotSprite;
    private SymbolSO currentSymbol;
    private bool isSpinning = false;


    private void Awake()
    {
        slotSprite = GetComponent<Image>();
    }

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
            float endTime = Time.time + UnityEngine.Random.Range(minSpinTime, maxSpinTime);

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
