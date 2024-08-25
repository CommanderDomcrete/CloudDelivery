using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStorage : MonoBehaviour
{
    public GameObject[] holdSlots;
    private int slotCheckLoop;
    private bool matchFound;

    private void OnTriggerEnter(Collider col)
    {
        matchFound = false;
        slotCheckLoop = 0;
        
        while(slotCheckLoop < holdSlots.Length && !matchFound)
        {
            MatchSlot(col);
            slotCheckLoop++;
        }
    }
    void MatchSlot(Collider other)
    {
        int slotIndex = slotCheckLoop;
        GameObject currentHoldSlot = holdSlots[slotIndex];
        GameObject obj = other.gameObject;

        if (obj.CompareTag(currentHoldSlot.tag))
        {
            obj.transform.SetParent(currentHoldSlot.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            obj.GetComponent<Rigidbody>().isKinematic = true;
            matchFound = true;
        }
        else
        {
            matchFound = false;
        }
    }
}