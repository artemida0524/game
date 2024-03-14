using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCraftSelectList : MonoBehaviour
{
    public SlotCraftSelect[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<SlotCraftSelect>();
    }

}
