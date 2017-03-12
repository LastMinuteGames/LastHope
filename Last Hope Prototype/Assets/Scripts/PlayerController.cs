using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PickUpController script = other.GetComponent<PickUpController>();
            if (script.effect == PickUpEffect.CURRENT)
            {
                switch (script.type)
                {
                    case (PickUpType.HP):
                        GetComponentInParent<PlayerHealth>().Heal(5);
                        break;
                    case (PickUpType.ENERGY):
                        GetComponentInParent<PlayerEnergy>().GainEnergy(1);
                        break;
                }
            }
            if (script.effect == PickUpEffect.MAX)
            {
                switch (script.type)
                {
                    case (PickUpType.HP):
                        GetComponentInParent<PlayerHealth>().IncreaseMaxHealthAndHeal(20);
                        break;
                    case (PickUpType.ENERGY):
                        GetComponentInParent<PlayerEnergy>().IncreaseMaxEnergy(1);
                        break;
                }
            }
            other.gameObject.SetActive(false);
        }
    }
}
