using UnityEngine;
//this is the code for player responses
[System.Serializable]
public class DialogueResponse
{
    public int AttractionPoints; // this sets attraction points for the response
    public string responseText; //this is the responses
    public DialogueNode nextNode; //this takes the player to the next part of dialog based on response

    public int getAttractionPoints()
    {
        return AttractionPoints;
    }

}
