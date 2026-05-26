using System.Collections;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class SceneDirectorScriptMorning : MonoBehaviour
{
    //public Actor Demon;
    public Narrator narrator;
    public Actor actor;
    public GameObject transition;
    CharacterData RefAttraction;// gets the AttractionPoints from CharacterData file and DialogResponse file
    BattleSystem RefBattleSystem; // reference to the battle system script to get the win or lose state of a battle


    public void Start()
    {
        RefAttraction = FindObjectOfType<CharacterData>();
        StartCoroutine(StartTalking());
     
    }

    public IEnumerator StartTalking()
    {
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        actor.Say(0);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
        narrator.Narrate(1);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        actor.Say(1);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
        narrator.Narrate(2);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            actor.Say(2);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
        narrator.Narrate(3);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        actor.Say(3);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
        narrator.Narrate(4);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        actor.Say(4);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
        Debug.Log(RefAttraction.Attraction);
        if (RefAttraction.Attraction == 2)
        {
            actor.Say(5);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(5);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            actor.Say(6);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(6);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            actor.Say(7);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(7);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            //start nbattle scene
            SceneManager.LoadScene(10);
        }
        else if(RefAttraction.Attraction == 7)
        {
            actor.Say(8);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(8);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            actor.Say(9);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(9);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            actor.Say(10);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(10);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            actor.Say(11);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(11);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            transition.GetComponent<Animator>().Play("outro transition");
            yield return new WaitForSeconds(0.5f);
            //load office scene
            SceneManager.LoadScene(4);


        }
        else if (RefAttraction.Attraction == 4)
        {
            actor.Say(12);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(7);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            //start battle scene
            SceneManager.LoadScene(10);
        }

    }
        
        
    



}