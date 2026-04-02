using NUnit.Framework;
using UnityEngine;
[System.Serializable]
public class DialogCharacter
{

    public string name;

}

[System.Serializable]
public class DialogLine
{
    public DialogCharacter character;
    [TextArea(3,10)]
    public string line;
}

[System.Serializable]
public class Dialog
{
    
    public System.Collections.Generic.List<DialogLine> dialogLines = new System.Collections.Generic.List<DialogLine>();


}

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public void TriggerDialog()
    {
        DialogManager.Instance.StartDialog(dialog);
    }

    public void Start()
    {
        TriggerDialog();
    }

}

