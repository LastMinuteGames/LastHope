using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {
    public static DialogueSystem Instance { get; set; }
    public List<string> dialogueLines = new List<string>();
    public List<string> fromLines = new List<string>();
    public GameObject dialogueBox;
    public Text dialogueText;
    public Text dialogueFrom;
    private bool show;
    private bool next;

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
	}

    void Update()
    {
        if (show && next)
        {
            next = false;
            if (dialogueLines.Count > 0)
            {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogueLines[0];
                dialogueFrom.text = fromLines[0];
            }
            else
            {
                dialogueBox.SetActive(false);
                show = false;
                next = false;
            }

        }

        if (InputManager.LightAttack() || Input.GetKeyDown(KeyCode.Return) && show)
        {
            Debug.Log("ENTER!");
            NextDialogue();
        }
    }
	
	public void AddNewDialogue(string[] lines, string[] from)
    {
        dialogueLines = new List<string>(lines.Length);
        fromLines = new List<string>(from.Length);
        dialogueLines.AddRange(lines);
        fromLines.AddRange(from);
    }

    public void ShowDialogue()
    {
        show = true;
        next = true;
    }

    public void NextDialogue()
    {
        next = true;
        dialogueLines.RemoveAt(0);
        fromLines.RemoveAt(0);
    }
}
