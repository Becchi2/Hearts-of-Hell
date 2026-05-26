using System.Collections;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class OfficeBadEndSceneDirectorScript : MonoBehaviour
{
    //public Actor Demon;
    public Narrator narrator;
    public Actor actor;
    public GameObject transition;

    public void Start()
    {
        StartCoroutine(StartTalking() );
    }

    public IEnumerator StartTalking()
    {
        transition.GetComponent<Animator>().Play("intro scene transition");
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        actor.Say(0);
        yield return new WaitUntil(() => !DialogManager.Instance1.IsDialogActive());//wait for dialog to finish
        //load night scene
        narrator.Narrate(1);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        transition.GetComponent<Animator>().Play("outro transition");
        yield return new WaitForSeconds(0.5f);

    }
        
        
    



}