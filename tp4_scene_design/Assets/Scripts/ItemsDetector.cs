using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDetector : MonoBehaviour
{
    public GameObject crate;

    private Dictionary<string, GameObject> foundCrystals = new Dictionary<string, GameObject>();

    void OnTriggerEnter(Collider collider)
    {
        GameObject o = collider.gameObject;
        if (!foundCrystals.ContainsKey(o.name)) {
            Debug.Log("Added: " + o.name);
            foundCrystals.Add(o.name, o);
        } else {
            Debug.Log("Already in: " + o.name);
        }
    }
}
