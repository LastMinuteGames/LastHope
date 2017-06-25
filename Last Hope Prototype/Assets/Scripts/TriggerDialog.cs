using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TriggerDialog : MonoBehaviour {
    public List<string> lines;
    public List<string> froms;
    public List<float> timeDialogs;
    public List<float> delays;

    private bool spawned = false;

    void Start()
    {
        Assert.IsTrue(lines.Count == froms.Count);
        Assert.IsTrue(lines.Count == timeDialogs.Count);
        Assert.IsTrue(lines.Count == delays.Count);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !spawned)
        {
            for (int i = 0; i < lines.Count; ++i)
            {
                StartCoroutine(SpawnDialog(lines[i], froms[i], timeDialogs[i], delays[i]));
            }
            spawned = true;
        }
    }

    IEnumerator SpawnDialog(string line, string from, float timeDialog, float delay)
    {
        yield return new WaitForSeconds(delay);
        DialogueSystem.Instance.AddDialogue(line, from, timeDialog);
    }
}
