using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ActivarPalanca : XRSimpleInteractable
{
    public AudioSource audioSource;
    public Animator transition;

    private bool activado = false;

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && isSelected)
        {
            if (!activado)
            {
                audioSource.Play();
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            }
            activado = true;
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(2f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }
}
