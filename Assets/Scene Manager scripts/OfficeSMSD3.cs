using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeSMSD3 : MonoBehaviour
{
    public Narrator Narrator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartNarration()); // Start the talking
    }

    public IEnumerator StartNarration()
    {
        Narrator.Narrate(2);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive()); // wait for Narrate to finish
        SceneManager.LoadScene("SlitMouth Evening scene");

    }

}
