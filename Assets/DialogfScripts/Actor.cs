using UnityEngine;

//This is the code for the character the player is speaking to
public class Actor : MonoBehaviour
{
    public string Name;
    public Dialogue Dialogue;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpeakTo();
        }
    }

    //Triggers the dialogue with the caracter
    public void SpeakTo()
    {
        DialogueManager.Instance.StartDialogue(Name, Dialogue.RootNode);
    }
}
