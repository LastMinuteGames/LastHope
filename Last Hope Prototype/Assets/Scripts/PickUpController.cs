using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType
{
    HP,
    ENERGY
}
public enum PickUpEffect
{
    CURRENT,
    MAX
}

public class PickUpController : MonoBehaviour {

    public PickUpEffect effect;
    public PickUpType type;

    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
