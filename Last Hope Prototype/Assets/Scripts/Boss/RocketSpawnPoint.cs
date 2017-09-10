using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawnPoint : MonoBehaviour {
    public float initialDelay;
    public float delay;
    public bool done = false;
    public bool incoming = false;
    public GameObject incomingObject;
}
