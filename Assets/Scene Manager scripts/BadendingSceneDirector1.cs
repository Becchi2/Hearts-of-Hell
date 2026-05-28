using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadendingSceneDirector : MonoBehaviour
{
    //public Actor Demon;
    public Narrator narrator;
    public GameObject transition;

    public void Start()
    {
        StartCoroutine(StartTalking());
    }

    public IEnumerator StartTalking()
    {
        transition.GetComponent<Animator>().Play("intro scene transition");
        yield return new WaitForSeconds(0.5f);
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());//wait for narration to finish
        transition.GetComponent<Animator>().Play("outro transition");
        SceneManager.LoadScene(8);
    }
}
