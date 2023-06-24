using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] Slot[] slots;


    public void SpinAllSlots()
    {
        foreach (Slot slot in slots)
        {
            slot.Spin();
        }
    }
}
