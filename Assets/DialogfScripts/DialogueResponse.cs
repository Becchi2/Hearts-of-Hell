using UnityEngine;
//this is the code for player responses
[System.Serializable]
public class DialogueResponse
{
    public string responseText; //this is the response
    public DialogueNode nextNode; //this takes the player to the next part of dialog based on response

}
