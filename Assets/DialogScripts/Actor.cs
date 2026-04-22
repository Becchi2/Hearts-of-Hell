using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public List<Dialog> lines;
    Dialog dialog;


    //Triggers the dialogue with the character
    public void Say(int num)
    {


        dialog = lines[num];
        DialogManager.Instance1.StartDialog(dialog);

    }
}
