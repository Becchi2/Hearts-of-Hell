using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogNode", menuName = "DialogNode")]
//this is what the character is saying to the player
[System.Serializable]
public class DialogueNode :ScriptableObject
{
    public string dialogueText;//the text they are saying
    public string speaker;//shows who is speaking the line of dialog
    public List<DialogueResponse> responses;//the responses the player can say

    //checks for if there are no responses left
    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}
