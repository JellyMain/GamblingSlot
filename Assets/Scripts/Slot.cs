using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private SpriteRenderer slotSprite;
    [SerializeField] SymbolSO[] possibleSymbols;


    private void Awake()
    {
        slotSprite = GetComponent<SpriteRenderer>();
    }

    private int GetRandomNumber()
    {
        int randomNumber = Random.Range(0, possibleSymbols.Length);
        return randomNumber;
    }

    public void Spin()
    {
        slotSprite.sprite = possibleSymbols[GetRandomNumber()].symbolSprite;
    }
}
