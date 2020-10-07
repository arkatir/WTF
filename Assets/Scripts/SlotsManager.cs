using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    // Ce script est a attacher au joueur pour activer le fonctionnement des slots, aucun code n'est à ajouter ici

    public SlotItem[] slotItemList;
    public float coolDownTimeInSeconds = 5f;

    private float coolDownTimer;
    // Start is called before the first frame update

    public void PickUpSlotItem(SlotItem go)
    {
        if (coolDownTimer <= 0)
        {
            coolDownTimer = coolDownTimeInSeconds;
            int newSlotIndex = go.slotIndex;
            Debug.Log("newSlotIndex " + newSlotIndex);
            if (newSlotIndex < 3)
            {
                if (slotItemList[newSlotIndex] != null)
                    slotItemList[newSlotIndex].OnRemove();

                slotItemList[newSlotIndex] = go;
                slotItemList[newSlotIndex].OnInsert();
            }
        }
    }

    void Start()
    {
        slotItemList = new SlotItem[3];
        coolDownTimer = 0;
    }

    void Update()
    {
        if (coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Alpha1) && slotItemList[0] != null)
        {
            slotItemList[0].OnRemove();
            slotItemList[0] = null;
        }

        if (Input.GetKey(KeyCode.Alpha2) && slotItemList[1] != null)
        {
            slotItemList[1].OnRemove();
            slotItemList[1] = null;
        }

        if (Input.GetKey(KeyCode.Alpha3) && slotItemList[2] != null)
        {
            slotItemList[2].OnRemove();
            slotItemList[2] = null;
        }
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