using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogLines", menuName = "Dialog/DialogLines")]
[System.Serializable]
public class Dialog : ScriptableObject // where the dialog lines are stored and can be accessed by the DialogManager
{
    CharacterData refAttraction;
    public List<DialogLine> lines = new List<DialogLine>(); // list where the speaker and lines of dialog can be defined
    string speaker;
    string dialogText;
    public string SetSpeaker(int num) //gets the speaker of a line
    {
        speaker = lines[num].speaker;
        return speaker;
    }
    public string SetDialogText(int num) // gets the dialog of a line
    {
        dialogText = lines[num].line;
        return dialogText;
    }

    public int DialogAmmount() // gets the amount of lines in the dialog
    {
        return lines.Count;
    }

    public void setSprite(int num) // sets the sprite of the character for a line of dialog
    {
        refAttraction.Attraction = lines[num].SpriteRange;

    }

}

