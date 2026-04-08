using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public List<NarrationLines> lines;
    NarrationLines Narration;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Narrate(0);
        }
    }

    //Triggers the dialogue with the character
    public void Narrate(int num)
    {


        Narration = lines[num];
        NarrationManager.Instance.StartNarration(Narration);

    }

}
