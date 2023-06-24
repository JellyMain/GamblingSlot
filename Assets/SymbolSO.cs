using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Symbol", menuName = "Create Symbols")]
public class SymbolSO : ScriptableObject
{
    public Sprite symbolSprite;
    public int index;
}
