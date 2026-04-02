using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public static DialogManager Instance;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogArea;

    private Queue<DialogLine> lines;

    public bool isDialogActive = false;

    private void Start() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartDialog(Dialog dialog)
    {
        isDialogActive = true;

        lines.Clear();

        foreach (DialogLine dialogLine in dialog.dialogLines)
        {
            lines.Enqueue(dialogLine);
        }

        DisplayNextDialogLine();
    }

    public void DisplayNextDialogLine()
    {

        if(lines.Count ==0)
        {
            EndDialog();
            return;
        }
        else 
        {

            DialogLine currentLine = lines.Dequeue();

            characterName.text = currentLine.character.name;
        }

    }

    void EndDialog()
    {
        isDialogActive = false;
    }

}
            

