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
    public Material currentHpMat;
    public Material maxHpMat;
    public Material currentEnergyMat;
    public Material maxEnergyMat;

    private Renderer rend;

    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        switch (effect)
        {
            case PickUpEffect.CURRENT:
                switch (type)
                {
                    case (PickUpType.HP):
                        rend.sharedMaterial = currentHpMat;
                        break;
                    case (PickUpType.ENERGY):
                        rend.sharedMaterial = currentEnergyMat;
                        break;
                }
                break;
            case PickUpEffect.MAX:

                switch (type)
                {
                    case (PickUpType.HP):
                        rend.sharedMaterial = maxHpMat;
                        break;
                    case (PickUpType.ENERGY):
                        rend.sharedMaterial = maxEnergyMat;
                        break;
                }
                break;
        }
    }
    
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
