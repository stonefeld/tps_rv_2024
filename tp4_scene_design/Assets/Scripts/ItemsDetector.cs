using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsDetector : MonoBehaviour
{
    public GameObject crate;
    public TextMeshPro pointsText;

    private Dictionary<string, GameObject> foundCrystals = new Dictionary<string, GameObject>();

    void OnTriggerEnter(Collider collider)
    {
        GameObject o = collider.gameObject;
        if (!foundCrystals.ContainsKey(o.name)) {
            foundCrystals.Add(o.name, o);

            pointsText.text = "";
            foreach (string crystal in foundCrystals.Keys) {
                pointsText.text += crystal + ", ";
            }
            pointsText.text = pointsText.text.Remove(pointsText.text.Length - 2);

            if (foundCrystals.Count == 10) {
                pointsText.text += "\nYOU WON!";
            }
        }
    }
}
