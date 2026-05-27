using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackstorySceneDirector : MonoBehaviour
{
    //public Actor Demon;
    public Narrator narrator;
    public GameObject cutsceneStills;
    public GameObject transition;

    public void Start()
    {
        StartCoroutine(StartTalking());
    }

    public IEnumerator StartTalking()
    {
        cutsceneStills.GetComponent<Animator>().Play("Backstory 1");
        transition.GetComponent<Animator>().Play("intro scene transition");
        yield return new WaitForSeconds(0.5f);
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        transition.GetComponent<Animator>().Play("outro transition");
        yield return new WaitForSeconds(0.5f);

        cutsceneStills.GetComponent<Animator>().Play("Backstory 2");
        transition.GetComponent<Animator>().Play("intro scene transition");
        yield return new WaitForSeconds(0.5f);
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        transition.GetComponent<Animator>().Play("outro transition");
        yield return new WaitForSeconds(0.5f);

        cutsceneStills.GetComponent<Animator>().Play("Backstory 3");
        transition.GetComponent<Animator>().Play("intro scene transition");
        yield return new WaitForSeconds(0.5f);
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        transition.GetComponent<Animator>().Play("outro transition");
        yield return new WaitForSeconds(0.5f);

        cutsceneStills.GetComponent<Animator>().Play("Backstory 4");
        transition.GetComponent<Animator>().Play("intro scene transition");
        yield return new WaitForSeconds(0.5f);
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        transition.GetComponent<Animator>().Play("outro transition");
        yield return new WaitForSeconds(0.5f);
    }
}
