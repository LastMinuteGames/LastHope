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
    public int value;

    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (effect == PickUpEffect.CURRENT)
            {
                switch (type)
                {
                    case (PickUpType.HP):

                        other.gameObject.GetComponent<PlayerHealth>().Heal(value);
                        break;
                    case (PickUpType.ENERGY):
                        other.gameObject.GetComponent<PlayerEnergy>().GainEnergy(value);
                        break;
                }
            }
            if (effect == PickUpEffect.MAX)
            {
                switch (type)
                {
                    case (PickUpType.HP):
                        other.gameObject.GetComponent<PlayerHealth>().IncreaseMaxHealthAndHeal(value);
                        break;
                    case (PickUpType.ENERGY):
                        other.gameObject.GetComponent<PlayerEnergy>().IncreaseMaxEnergy(value);
                        break;
                }
            }
            gameObject.SetActive(false);
        }
    }
}
