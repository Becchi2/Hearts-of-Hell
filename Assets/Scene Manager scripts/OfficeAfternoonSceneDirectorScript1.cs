using System.Collections;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class OfficeAfternoonSceneDirectorScript : MonoBehaviour
{
    //public Actor Demon;
    public Narrator narrator;
    public Actor actor;


    public void Start()
    {
        StartCoroutine(StartTalking() );
    }

    public IEnumerator StartTalking()
    {
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        actor.Say(0);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());//wait for dialog to finish
        narrator.Narrate(1);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        //load afternoon scene
        SceneManager.LoadScene(5);
    }
        
        
    



}