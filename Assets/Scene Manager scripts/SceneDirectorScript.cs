using System.Collections;
using TMPro;
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

    CharacterData RefAttraction;// gets the AttractionPoints from CharacterData file and DialogResponse file
    BattleSystem RefBattleSystem; // reference to the battle system script to get the win or lose state of a battle


    public void Start()
    {
        RefAttraction = FindObjectOfType<CharacterData>();
        StartCoroutine(StartTalking());
     
    }

    public IEnumerator StartTalking()
    {

        actor.Say(0);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());

        if (RefAttraction.Attraction == 1)
        {
            actor.Say(1);
            yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());

        }
        else
        {
            narrator.Narrate(0);
            yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            narrator.Narrate(0);
        }
    }
        
        
    



}