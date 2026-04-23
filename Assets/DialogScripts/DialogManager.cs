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
    public Dialog dialog;// contains dialog lines
    int index;//keeps track of which line you are on

    private void Awake()
    {

        if (Instance1 == null)
            Instance1 = this;
        else
            Destroy(gameObject);

        HideDialog();
    }


    public void StartDialog() //begins the dialog by showing the first line and creating a button to continue
    {
        // start from first line
        index = 0;
        // show the dialog UI and clear previous buttons
        DialogParent.SetActive(true);
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
        }
        else
        {
            HideDialog();
        }

    }

    public IEnumerator TypeOut()
    {
        DialogText.text = "";
        foreach (char letter in dialog.SetDialogText(index).ToCharArray())
        {
            DialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

    }


    public void HideDialog()//hides the dialog UI
    {
        DialogParent.SetActive(false);
        foreach (Transform child in ContinueButtonContainer)
        {
            Destroy(obj: child.gameObject); // gets rid of "next" button
        }
        if (DialogText != null) DialogText.text = string.Empty;
        index = 0;

    }

}
