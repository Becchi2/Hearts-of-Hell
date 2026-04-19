using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class SceneDirectorScriptEvening : MonoBehaviour
{
    public Actor Demon;
    public Narrator Narrator;
    CharacterData RefAttraction;// gets the AttractionPoints from CharacterData file and DialogResponse file

    void Start()
    {
        StartCoroutine(StartEvevningDialog());
    }

    public IEnumerator StartEvevningDialog()
    {
        RefAttraction = FindAnyObjectByType<CharacterData>();
        // Get the CharacterData component from the Demon GameObject
        Narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive()); // wait for Narrate to finish
        Demon.SpeakTo(0); // starts the dialogue at position num in the list of dialogue routes for the demon
        yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive()); // wait for SpeakTo to finish
        Narrator.Narrate(1);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        Demon.SpeakTo(1);
        yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
        Narrator.Narrate(2);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        Demon.SpeakTo(2);
        yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
        Narrator.Narrate(3);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        Demon.SpeakTo(3);
        yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
        if (RefAttraction.Attraction < 5)
        {
            Demon.SpeakTo(4);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
        }
        else
        {
            Narrator.Narrate(4);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(5);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            Narrator.Narrate(5);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(6);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            Narrator.Narrate(6);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());

        }
    }
    }


    
