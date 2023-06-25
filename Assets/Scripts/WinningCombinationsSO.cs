using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinningCombiantion", menuName = "Create Winning Combination")]
public class WinningCombinationsSO : ScriptableObject
{
    public SymbolSO[] winningCombiantion;
    public float coefficient;
}
