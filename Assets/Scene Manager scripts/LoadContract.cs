using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadContract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
