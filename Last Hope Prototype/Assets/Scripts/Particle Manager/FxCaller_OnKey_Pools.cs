using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simula una llamada a la instanciación de un FX.
// Esta clase no tiene sentido más que para testing.
// Esta llamada se debería hacer desde la lógica del juego, siendo el receptor un objeto definido por las acciones del propio juego, y no un objeto predefinido.
public class FxCaller_OnKey_Pools : MonoBehaviour
{
	public FxReceiver_Basic Receiver_Predefined;
	public GameObject FxPrefab;

	void Update ()
	{
		/*if (Input.GetKeyDown(KeyCode.Space))
		{
			FxManager_Pools.Instance.GetFx(FxPrefab, Receiver_Predefined.HitEmitter.position);
		}*/
	}

    public void ThrowFX()
    {
        FxManager_Pools.Instance.GetFx(FxPrefab, Receiver_Predefined.HitEmitter.position);
    }
    
}
