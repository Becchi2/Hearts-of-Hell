using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

//This is the code for the character the player is speaking to
public class Actor : MonoBehaviour
{
    public string Name;
    Dialogue Dialogue;
    public List<Dialogue> DialogRoutes;//the ammount of dialog paths the player can choose


    //Triggers the dialogue with the character
    public void SpeakTo(int num)
    {

        Dialogue = DialogRoutes[num];
        DialogueManager.Instance1.StartDialogue(Name, Dialogue.RootNode);
    }

}
