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

public class PickUpController : MonoBehaviour
{

    public PickUpEffect effect;
    public PickUpType type;
    public int value;
    private int audioToPlay;

    void Update()
    {
        transform.Rotate(new Vector3(0, 80, 0) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        bool destroy = false;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (effect == PickUpEffect.CURRENT)
            {
                switch (type)
                {
                    case (PickUpType.HP):
                        destroy = other.gameObject.GetComponent<PlayerController>().Heal(value);
                        audioToPlay = (int)AudiosSoundFX.Environment_PickUps_HP;
                        break;
                    case (PickUpType.ENERGY):
                        destroy = other.gameObject.GetComponent<PlayerController>().GainEnergy(value);
                        audioToPlay = (int)AudiosSoundFX.Environment_PickUps_Energy;
                        break;
                }
            }
            if (effect == PickUpEffect.MAX)
            {
                destroy = true;
                switch (type)
                {
                    case (PickUpType.HP):
                        other.gameObject.GetComponent<PlayerController>().IncreaseMaxHealthAndHeal(value);
                        audioToPlay = (int)AudiosSoundFX.Environment_Unclassified_PowerUp;
                        break;
                    case (PickUpType.ENERGY):
                        other.gameObject.GetComponent<PlayerController>().IncreaseMaxEnergy(value);
                        audioToPlay = (int)AudiosSoundFX.Environment_Unclassified_PowerUp;
                        break;
                }
            }
            if (destroy)
            {
                AudioSources.instance.PlaySound(audioToPlay);
                Destroy(gameObject);
            }
        }
    }
}
