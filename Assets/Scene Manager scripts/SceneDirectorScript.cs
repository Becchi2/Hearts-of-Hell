using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class SceneDirectorScriptMorning : MonoBehaviour
{
    //public Actor Demon;
    public Narrator narrator;
    public Actor actor;
    public Transform ResponseButtonContainer; // Container for response buttons
    public GameObject ResponseButtonPrefab; // Prefab for response buttons
    CharacterData RefAttraction;// gets the AttractionPoints from CharacterData file and DialogResponse file
    BattleSystem RefBattleSystem; // reference to the battle system script to get the win or lose state of a battle


    public void Start()
    {
        
    }

    public void CreateButton(string buttonText, int nextDialogNum)//creates response buttons with the text and the next dialog number to trigger when clicked
    {
        //creates button
        GameObject buttonObj = Instantiate(ResponseButtonPrefab, ResponseButtonContainer);
        buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

        //makes button trigger a response when clicked
        buttonObj.GetComponent<Button>().onClick.AddListener(() => actor.Say(nextDialogNum));
        buttonObj.GetComponent <Button>().onClick.AddListener(() => HideButton(buttonObj));

    }

    public void HideButton(GameObject buttonObj)//hides the button after it is clicked
    {
        // Hides the button after it is clicked
        buttonObj.SetActive(false);
    }
}