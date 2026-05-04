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
 StartTalking();
     
    }

    public void StartTalking()
    {

        actor.Say(0);
        if (RefAttraction.Attraction == 1)
        {
            actor.Say(0);

        }
        else
        {
            narrator.Narrate(0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            narrator.Narrate(0);
        }
    }
        
        
    



}