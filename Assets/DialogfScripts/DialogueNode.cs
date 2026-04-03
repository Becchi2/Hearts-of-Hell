using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogNode", menuName = "DialogNode")]
//this is what the character is saying to the player
[System.Serializable]
public class DialogueNode :ScriptableObject
{
    public string dialogueText;//the text they are saying
    public List<DialogueResponse> responses;//the responses the player can say


    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}
