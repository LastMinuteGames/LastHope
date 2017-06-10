using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour {
    public static DialogueSystem Instance { get; set; }
    public List<string> dialogueLines = new List<string>();
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
	}
	
	public void AddNewDialogue(string[] lines)
    {
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
    }
}
