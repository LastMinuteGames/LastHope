using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog
{
    private float time;
    private string text;
    private string from;

    public float Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
        }
    }

    public string Text
    {
        get
        {
            return text;
        }

        set
        {
            text = value;
        }
    }

    public string From
    {
        get
        {
            return from;
        }

        set
        {
            from = value;
        }
    }

    public Dialog(string text, string from, float time)
    {
        this.text = text;
        this.from = from;
        this.time = time;
    }
}

public class DialogueSystem : MonoBehaviour {
    public static DialogueSystem Instance { get; set; }
    public GameObject dialogueBox;
    public Text dialogueText;
    public Text dialogueFrom;
    private float timeLeft;
    private List<Dialog> dialogs;
    private Dialog currentDialog;
    public bool isOn = true;


    // Use this for initialization
    void Awake () {
		if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
        dialogueBox.SetActive(false);
        timeLeft = 0;
        dialogs = new List<Dialog>();
    }

    void Update()
    {
        if(currentDialog != null)
        {
            
            timeLeft += Time.deltaTime;
            if (currentDialog.Time > 0 && timeLeft > currentDialog.Time)
            {
                NextDialogue();
                if(currentDialog != null)
                {
                    ShowDialogue();
                }
                else
                {
                    dialogueBox.SetActive(false);
                }
            }
        }
    }
	
    public void AddDialogue(string line, string from, float timeDialog = 0)
    {
        if (!isOn)
        {
            return;
        }
        Dialog dialog = new Dialog(line, from, timeDialog);
        dialogs.Add(dialog);

        if(currentDialog == null)
        {
            currentDialog = dialog;
            ShowDialogue();
        }
    }
    


    //Use this when you can use next dialog or hide current dialog
    public void NextDialogue()
    {
        if(dialogs.Count > 1)
        {
            dialogs.RemoveAt(0);
            currentDialog = dialogs[0];
            ShowDialogue();
        }
        else if(dialogs.Count > 0)
        {
            dialogs.RemoveAt(0);
            currentDialog = null;
            HideDialogue();
        }
        timeLeft = 0;
    }

    private void HideDialogue()
    {
        dialogueBox.SetActive(false);
        if(currentDialog != null)
        {
            dialogs.Remove(currentDialog);
            currentDialog = null;
        }
    }

    private void ShowDialogue()
    {
        dialogueBox.SetActive(true);
        if (currentDialog != null)
        {
            dialogueText.text = currentDialog.Text;
            dialogueFrom.text = currentDialog.From;
        }
    }
}
