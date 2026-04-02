using TMPro;
using UnityEngine;
using UnityEngine.UI;
//this controls all the dialog. It goes on an object in the game's scene in unity
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    //References the UI stuff in the game
    public GameObject DialogParent; //this is what contians all the dialog UI
    public TextMeshProUGUI DemonName, DialogText; //contains text for name and dialog text
    public GameObject ResponseButtonPrefab; // Prefab that generates responses
    public Transform ResponseButtonContainer; //container which holds response buttons

    private void Awake()
    {
        //ensures thers only one instance of dialogue manager at a time
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //initially hides dialog UI
        HideDialogue();
    }

    //starts the dialog with a name and dialog node
    public void StartDialogue(string name, DialogueNode node)
    {
        //Display Dialog UI
        ShowDialogue();

        //shows name and dialog text
        DemonName.text = name;
        DialogText.text = node.dialogueText;

        //removes existing resonse buttons
        foreach (Transform child in ResponseButtonContainer)
        {
            Destroy(child.gameObject);
        }

        //assigns responses to buttons
        foreach (DialogueResponse response in node.responses)
        {
            GameObject buttonObj = Instantiate(ResponseButtonPrefab, ResponseButtonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

            //makes button trigger a response when clicked
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectResponse(response, name));

        }
    }
    //continues dialog based on chosen response
    public void SelectResponse(DialogueResponse response, string name)
    {
        //check if there's a next node
        if (response.nextNode.IsLastNode())
        {
            StartDialogue(name, response.nextNode);// checks for the next dialog
        }
        else
        {
            //if there's no next node
            HideDialogue();
        }
    }

    //hide dialog UI
    public void HideDialogue()
    {
        DialogParent.SetActive(false);
    }
    //show dialog UI
    public void ShowDialogue()
    {
        DialogParent.SetActive(true);
    }

    // Check if dialogue is currently active
    public bool IsDialogueActive()
    {
        return DialogParent.activeSelf;
    }
}

