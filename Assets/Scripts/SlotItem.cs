using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    // Pour utiliser ce script, modifier votre classe pour hériter de SlotItem plutôt que MonoBehaviour
    // Une fois cela fait, redéfinissez les procédures OnInsert et OnRemove avec le code d'activation et de désactivation de votre équipement

    // Le slot index correspond au type d'équipement pour le joueur,
    // Il est public et peut donc être changer dans l'inspecteur une fois que votre classe hérite de SlotItem
    // 0 correspond aux armes
    // 1 correspond aux véhicules/montures
    // 2 est à définir
    public int slotIndex = 0;
    
    public virtual void OnInsert()
    {
        Debug.Log("OnSlotItemInstantiation base");
    }

    public virtual void OnRemove()
    {
        Debug.Log("OnSlotItemDestroy base");
    }
}
