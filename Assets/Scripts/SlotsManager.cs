using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    public SlotItem[] slotItemList;
    // Start is called before the first frame update

    public void PickUpSlotItem(SlotItem go)
    {
        int newSlotIndex = go.slotIndex;
        if (slotItemList[newSlotIndex])
        {
            slotItemList[newSlotIndex].OnSlotItemDestroy();
        }

        slotItemList[newSlotIndex].OnSlotItemInstantiation();
    }

    void OnUpdate()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            slotItemList[1].OnSlotItemDestroy();

        if (Input.GetKey(KeyCode.Alpha2))
            slotItemList[2].OnSlotItemDestroy();
    }
    public void OnTriggerEnter(Collider other)
    {
        SlotItem s = other.GetComponent<SlotItem>();
        if(s != null)
        {
            PickUpSlotItem(s);
        }
    }
}
