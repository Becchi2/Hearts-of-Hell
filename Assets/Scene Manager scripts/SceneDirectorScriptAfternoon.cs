using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class SceneDirectorScriptAfternoon : MonoBehaviour
{
    public Actor Demon;
    public Narrator Narrator;
    CharacterData RefAttraction;// gets the AttractionPoints from CharacterData file and DialogResponse file

    void Start()
    {
        StartCoroutine(StartDialogueGood()); // Start the scene after the narration        
    }

    public IEnumerator StartDialogueGood() // dialogue for the good route of the game, where the player has a high attraction score with the demon
    {
        RefAttraction = FindAnyObjectByType<CharacterData>();
        Narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive()); // wait for Narrate to finish*/
        Demon.SpeakTo(0);
        yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive()); // wait for SpeakTo to finish
        if (RefAttraction.Attraction < 5)//if player chooses "no" choice
        {
            Demon.SpeakTo(1);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
        }
        else //if player chooses "you got me" choice
        {
            Demon.SpeakTo(10);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            Demon.SpeakTo(11);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());

        }
            Narrator.Narrate(1);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(2);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());

            if (RefAttraction.Attraction < 5)
            {
                Demon.SpeakTo(3);
                yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());

            }
            else if (RefAttraction.Attraction > 5)
            {
                Demon.SpeakTo(4);
                yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
                Narrator.Narrate(2);
                yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
                Demon.SpeakTo(5);
                yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
                Narrator.Narrate(3);
                yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
                Demon.SpeakTo(6);
                yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
                if (RefAttraction.Attraction < 5)
                {
                    Demon.SpeakTo(7);
                    yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());

                }
                else if (RefAttraction.Attraction > 5 && RefAttraction.Attraction < 7)
                {
                    Demon.SpeakTo(8);
                    yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
                    Narrator.Narrate(4);
                    yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());

                }
                else if (RefAttraction.Attraction > 7)
                {
                    Demon.SpeakTo(9);
                    yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());

                }

            }
        }
        


    }


    
