using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    public int slotIndex = 0;
    // Start is called before the first frame update

    public virtual void OnInsert()
    {
        Debug.Log("OnSlotItemInstantiation base");
    }

    public virtual void OnRemove()
    {
        Debug.Log("OnSlotItemDestroy base");
    }
}
