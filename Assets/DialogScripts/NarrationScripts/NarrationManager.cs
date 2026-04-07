using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class NarrationManager : MonoBehaviour
{
    public GameObject NarrationDialogParent; //this is what contians all the dialog UI
    public TextMeshProUGUI textComponent;//where the text goes
    public GameObject NarrationButtonPrefab; // Prefab for continue button
    public Transform NarrationButtonContainer; //container which holds buttons
    public NarrationLines lines;// contains narration lines
    private int index;//keeps track of which line you are on

    public void Start()
    {
        
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartNarration();
        }
    }
    public void StartNarration()
    {
        TypeLine();
        index = 0;
        GameObject buttonObj = Instantiate(NarrationButtonPrefab, NarrationButtonContainer);
        buttonObj.GetComponent<Button>().onClick.AddListener(() => NextLine());

    }

    public void TypeLine()
    {
        textComponent.text = lines.SetLine(index);
    }

    public void NextLine()
    {
        if (index < lines.LineAmmount()-1)
        {
            index++;
            TypeLine();
        }
        else
        {
            foreach (Transform child in NarrationButtonContainer)
            {
                Destroy(obj: child.gameObject); // gets rid of "next" button
            }
            NarrationDialogParent.SetActive(false); // gets rid of narration box
        }
    }
}
