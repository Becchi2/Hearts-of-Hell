using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
             // Load the main game scene
      SceneManager.LoadScene("Office SlitMouth 1"); //Load the next scene in the build index 
    }

    public void QuitGame()
    {
        // Quit the game
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
