using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemsDetector : MonoBehaviour
{
    public GameObject crate;
    public TextMeshPro pointsText;
    public AudioSource introAudioSource;
    public AudioSource primerCristalAudioSource;
    public AudioSource quintoCristalAudioSource;
    public AudioSource novenoCristalAudioSource;

    private Dictionary<string, GameObject> foundCrystals = new Dictionary<string, GameObject>();

    void OnTriggerEnter(Collider collider)
    {
        GameObject o = collider.gameObject;
        if (!foundCrystals.ContainsKey(o.name)) {
            foundCrystals.Add(o.name, o);

            switch (foundCrystals.Count) {
                case 1:
                    introAudioSource.Stop();
                    primerCristalAudioSource.Play();
                    break;

                case 5:
                    introAudioSource.Stop();
                    primerCristalAudioSource.Stop();
                    quintoCristalAudioSource.Play();
                    break;

                case 9:
                    introAudioSource.Stop();
                    primerCristalAudioSource.Stop();
                    quintoCristalAudioSource.Stop();
                    novenoCristalAudioSource.Play();
                    break;
            }


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
