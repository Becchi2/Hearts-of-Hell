using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirectorScriptAfternoon : MonoBehaviour
{
    public Actor Demon;
    public Narrator Narrator;
    CharacterData RefAttraction;// gets the AttractionPoints from CharacterData file and DialogResponse file

    void Start()
    {
        if (CharacterData.lovePoints > 0)
        {
            StartCoroutine(StartDialogueGood()); // Start the scene after the narration
        }
        else
        {
            StartCoroutine(StartDialogueBad()); // Start the scene after the narration
        }
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
            SceneManager.LoadScene("Battle Scene");//Loads the battle scene

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
                SceneManager.LoadScene("Battle Scene");//Loads the battle scene

            }
            else if (RefAttraction.Attraction > 5 && RefAttraction.Attraction < 7)
            {
                Demon.SpeakTo(8);
                yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
                Narrator.Narrate(4);
                yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
                SceneManager.LoadScene("Office SlitMouth 3");

            }
            else if (RefAttraction.Attraction > 7)
            {
                Demon.SpeakTo(9);
                yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
                SceneManager.LoadScene("Office SlitMouth 3");
            }

        }
    }

    public IEnumerator StartDialogueBad() // dialogue for the bad route of the game, where the player has a low attraction score with the demon
    {
        RefAttraction = FindAnyObjectByType<CharacterData>();
        Narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive()); // wait for Narrate to finish*/
        Demon.SpeakTo(12);
        yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive()); // wait for SpeakTo to finish
        if (RefAttraction.Attraction < 5)//if player chooses "just doing my job" choice
        {
            Demon.SpeakTo(13);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            SceneManager.LoadScene("Battle Scene");//Loads the battle scene
        }
        else //if player chooses "not looking for you" choice
        {
            Demon.SpeakTo(14);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            Narrator.Narrate(5);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(15);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            if (RefAttraction.Attraction < 5)
            {
                Demon.SpeakTo(16);
                yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
                SceneManager.LoadScene("Battle Scene");//Loads the battle scene

            }
            else
            {
                Demon.SpeakTo(17);
                yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
                Narrator.Narrate(6);
                SceneManager.LoadScene("Office SlitMouth 3");
            }
        }


    }
}


    
