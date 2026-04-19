using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenContract : MonoBehaviour
{
   public void AccepContract()
   {
      SceneManager.LoadScene(2);
    }
}
