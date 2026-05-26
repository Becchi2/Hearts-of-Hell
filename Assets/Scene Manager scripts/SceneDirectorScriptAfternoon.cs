using System.Collections;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class SceneDirectorScriptAfternoon : MonoBehaviour
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
        transition.GetComponent<Animator>().Play("intro scene transition");
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        actor.Say(0);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
        if (RefAttraction.Attraction == 4)//say no
        {
            actor.Say(1);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(1);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            actor.Say(2);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            if (RefAttraction.Attraction == 3)
            {
                actor.Say(3);
                yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                //start battle
                SceneManager.LoadScene(10);
            }
            else if (RefAttraction.Attraction == 5)
            {
                actor.Say(4);
                yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                narrator.Narrate(2);
                yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
                actor.Say(5);
                yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                narrator.Narrate(3);
                yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
                actor.Say(6);
                yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                if (RefAttraction.Attraction == 3)//say no
                {
                    actor.Say(7);
                    yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                    //enter battle sequence
                    SceneManager.LoadScene(10);
                    
                }
                else if (RefAttraction.Attraction == 6)//say mehh
                {
                    actor.Say(8);
                    yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                    transition.GetComponent<Animator>().Play("outro transition");
                    yield return new WaitForSeconds(0.5f);
                    SceneManager.LoadScene(6);

                }
                else if (RefAttraction.Attraction == 9)//say yes
                {
                    actor.Say(9);
                    yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                    transition.GetComponent<Animator>().Play("outro transition");
                    yield return new WaitForSeconds(0.5f);
                    SceneManager.LoadScene(6);
                }


            }
            }
        else if (RefAttraction.Attraction == 7)//say yes
        {
            actor.Say(10);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            narrator.Narrate(1);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
            actor.Say(2);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            if (RefAttraction.Attraction == 3)
            {
                actor.Say(3);
                yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                //start battle
                SceneManager.LoadScene(10);

            }
            else if (RefAttraction.Attraction == 5)
            {
                actor.Say(4);
                yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                narrator.Narrate(2);
                yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
                actor.Say(5);
                yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                narrator.Narrate(3);
                yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
                actor.Say(6);
                yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                if (RefAttraction.Attraction == 3)//say no
                {
                    actor.Say(7);
                    yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                    //enter battle sequence
                    SceneManager.LoadScene(10);
                }
                else if (RefAttraction.Attraction == 6)//say mehh
                {
                    actor.Say(8);
                    yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                    transition.GetComponent<Animator>().Play("outro transition");
                    yield return new WaitForSeconds(0.5f);
                    SceneManager.LoadScene(6);
                }
                else if (RefAttraction.Attraction == 9)//say yes
                {
                    actor.Say(9);
                    yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
                    transition.GetComponent<Animator>().Play("outro transition");
                    yield return new WaitForSeconds(0.5f);
                    SceneManager.LoadScene(6);
                }
            }

            }

    }
        
        
    



}