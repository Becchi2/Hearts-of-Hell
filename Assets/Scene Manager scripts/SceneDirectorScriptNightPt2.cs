using System.Collections;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class SceneDirectorScriptNightPt2 : MonoBehaviour
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
        
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
        if (RefAttraction.Attraction == 7)//say yes
        {
            actor.Say(1);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            transition.GetComponent<Animator>().Play("outro transition");
            yield return new WaitForSeconds(0.5f);
            //load good end scene
            SceneManager.LoadScene(12);
        }
        else if(RefAttraction.Attraction == 6)//say no
        {
            actor.Say(2);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());
            //start battle
            SceneManager.LoadScene(10);
        }
        

    }
        
        
    



}