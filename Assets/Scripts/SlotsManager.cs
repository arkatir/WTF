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
        Debug.Log("newSlotIndex " + newSlotIndex);
        if (newSlotIndex < 3)
        {
            if (slotItemList[newSlotIndex])
                slotItemList[newSlotIndex].OnRemove();

            slotItemList[newSlotIndex] = go;
            slotItemList[newSlotIndex].OnInsert();
        }
    }

    void Start()
    {
        slotItemList = new SlotItem[3];
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            slotItemList[0].OnRemove();

        if (Input.GetKey(KeyCode.Alpha2))
            slotItemList[1].OnRemove();

        if (Input.GetKey(KeyCode.Alpha3))
            slotItemList[2].OnRemove();
    }

    void OnTriggerEnter(Collider other)
    {
        SlotItem s = other.GetComponent<SlotItem>();
        if(s != null)
        {
            PickUpSlotItem(s);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        SlotItem s = coll.transform.GetComponent<SlotItem>();
        if (s != null)
        {
            PickUpSlotItem(s);
        }
    }
}