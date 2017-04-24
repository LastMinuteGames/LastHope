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
    
    void Update () {
        transform.Rotate(new Vector3(0, 80, 0) * Time.deltaTime);
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

                        other.gameObject.GetComponent<PlayerController>().Heal(value);
                        break;
                    case (PickUpType.ENERGY):
                        other.gameObject.GetComponent<PlayerController>().GainEnergy(value);
                        break;
                }
            }
            if (effect == PickUpEffect.MAX)
            {
                switch (type)
                {
                    case (PickUpType.HP):
                        other.gameObject.GetComponent<PlayerController>().IncreaseMaxHealthAndHeal(value);
                        break;
                    case (PickUpType.ENERGY):
                        other.gameObject.GetComponent<PlayerController>().IncreaseMaxEnergy(value);
                        break;
                }
            }
            gameObject.SetActive(false);
        }
    }
}
