using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour // makes dialog appear on the screen and handles the dialog between characters
{
    public static DialogManager Instance1 { get; private set; }

    public GameObject DialogParent;//this is what contians all the dialog UI
    public TextMeshProUGUI DialogText;//where the text goes
    public TextMeshProUGUI SpeakerText;//where the speaker's name goes
    public GameObject ContinueButtonPrefab; // Prefab for continue button
    public Transform ContinueButtonContainer; //container which holds buttons
    public Transform ResponseButtonContainer; // Container for response buttons
    public GameObject ResponseButtonPrefab; // Prefab for response buttons
    public CharacterData RefAttraction;
    public Dialog dialog;// contains dialog lines
    int index;//keeps track of which line you are on

    private void Awake()
    {

        if (Instance1 == null)
            Instance1 = this;
        else
            Destroy(gameObject);
        
        DialogParent.SetActive(false);
    }


    public void StartDialog() //begins the dialog by showing the first line and creating a button to continue
    {

        // start from first line
        index = 0;
        // show the dialog UI and clear previous buttons
        DialogParent.SetActive(true);

        DialogParent.GetComponent<Animator>().Play("DialogBoxAppear");//makes the dialog box appear with an animation
        foreach (Transform child in ContinueButtonContainer)
        {
            Destroy(child.gameObject);
        }

        ShowDialog();

        GameObject buttonObj = Instantiate(ContinueButtonPrefab, ContinueButtonContainer);
        buttonObj.GetComponent<Button>().onClick.AddListener(() => NextDialog());

    }

    public void StartDialog(Dialog stuff) //begins the dialog by showing the first line and creating a button to continue
    {
        dialog = stuff;
        StartDialog();
    }

    public void ShowDialog()//shows the current line of dialog
    {
        if (dialog == null || dialog.DialogAmmount() == 0)
        {
            DialogText.text = string.Empty;
            return;
        }

        if (index < 0 || index >= dialog.DialogAmmount())
        {
            DialogText.text = string.Empty;

            return;
        }
        SpeakerText.text = dialog.SetSpeaker(index);
                dialog.setSprite(index);
        StopAllCoroutines(); // Stop any ongoing typing coroutine
        StartCoroutine(TypeOut());
    }

    public void NextDialog()// goes to the next line of dialog
    {


        if (index < dialog.DialogAmmount() - 1)
        {
            index++;
            ShowDialog();
            if (index == dialog.DialogAmmount() - 1)
            {
                int index2 = -1;
                foreach (DialogResponseButton button in dialog.buttons)
                {
                    
                    index2++;
                    CreateButton(index2);
                }
            }
        }
        else
        {
            StartCoroutine(HideDialog());
        }

    }

    public IEnumerator TypeOut()
    {
        DialogText.text = "";
        foreach (char letter in dialog.SetDialogText(index).ToCharArray())
        {
            DialogText.text += letter;
            DialogParent.GetComponent<AudioSource>().PlayOneShot(DialogParent.GetComponent<AudioSource>().clip);//plays sound
            yield return new WaitForSeconds(0.02f);
        }


    }


    public IEnumerator HideDialog()//hides the dialog UI
    {
        DialogParent.GetComponent<Animator>().Play("DialogBoxDisapear",0,0f);//makes the dialog box disappear with an animation
        ContinueButtonPrefab.GetComponent<Animator>().Play("Disabled");
        yield return new WaitForSeconds(0.4f); // Wait for the animation to finish
        DialogParent.SetActive(false);
        foreach (Transform child in ContinueButtonContainer)
        {
            Destroy(obj: child.gameObject); // gets rid of "next" button
        }
        if (DialogText != null) DialogText.text = string.Empty;
        index = 0;

    }

    public void CreateButton(int num)//creates response buttons with the text and changes the attraction points 
    {
        //gets rid of continue button for dialog box
        foreach (Transform child in ContinueButtonContainer)
        {
            Destroy(child.gameObject);
        }
        //creates button
        GameObject buttonObj = Instantiate(ResponseButtonPrefab, ResponseButtonContainer);
        buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = dialog.ButtonText(num);
        RefAttraction = FindObjectOfType<CharacterData>();
        RefAttraction.Attraction = dialog.ButtonAttraction(num);

        //makes button trigger a response when clicked
        buttonObj.GetComponent<Button>().onClick.AddListener(() => RefAttraction.ChangeAttraction(dialog.ButtonAttraction(num)));
        buttonObj.GetComponent<Button>().onClick.AddListener(() => HideButton(buttonObj));
        buttonObj.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(HideDialog()));

    }
    public void HideButton(GameObject buttonObj)//hides the button after it is clicked
    {
        // Hides the button after it is clicked
        foreach (Transform child in ResponseButtonContainer)
        {
            Destroy(child.gameObject);
        }

    }

    public bool IsDialogActive() // checks if the narration is currently active
    {
        return DialogParent.activeSelf;
    }

}
