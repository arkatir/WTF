using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    public int slotIndex;
    // Start is called before the first frame update

    public virtual void OnSlotItemInstantiation()
    {
        Debug.Log("OnSlotItemInstantiation base");
    }

    public virtual void OnSlotItemDestroy()
    {
        Debug.Log("OnSlotItemDestroy base");
    }
}
