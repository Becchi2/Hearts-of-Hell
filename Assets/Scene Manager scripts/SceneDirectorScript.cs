using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class SceneDirectorScriptMorning : MonoBehaviour
{
    public Actor Demon;
    public Narrator Narrator;
    CharacterData RefAttraction;// gets the AttractionPoints from CharacterData file and DialogResponse file
    BattleSystem RefBattleSystem; // reference to the battle system script to get the win or lose state of a battle


    void Start()
    {
        StartCoroutine(StartDialogue()); // Start the scene after the narration        
    }

    public IEnumerator StartDialogue() // orders the sequence of events in the dialog scene for slitmouth
    {
        RefAttraction = FindAnyObjectByType<CharacterData>();
        RefBattleSystem = FindAnyObjectByType<BattleSystem>();
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
        Narrator.Narrate(4);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        Demon.SpeakTo(4);
        yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
        
        if (RefAttraction.Attraction < 3)// the interaction that happens if you say no
        {
            Narrator.Narrate(6);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(6);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            //enter the fight sequence
            Time.timeScale = 0;//pauses the game scene
            SceneManager.LoadScene("Battle Scene");//Loads the battle scene




        }
        else if (RefAttraction.Attraction >= 5) //the interaction that happens if you say ehh
        {
            Demon.SpeakTo(7);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            Narrator.Narrate(7);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(8);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            Narrator.Narrate(8);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(9);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            Narrator.Narrate(9);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(10);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            Narrator.Narrate(10);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            CharacterData.lovePoints += 1;

        }
        else if (RefAttraction.Attraction  == 4)// the interaction that happens if you say yes
        {
            Narrator.Narrate(6);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            Demon.SpeakTo(11);
            yield return new WaitUntil(() => !DialogueManager.Instance1.IsDialogueActive());
            //enter the fight sequence
            Time.timeScale = 0;//pauses the game scene
            SceneManager.LoadScene("Battle Scene");//Loads the battle scene





        }

    }
}