using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAttackTrigger : MonoBehaviour {

	public Material mat1;
	public Material mat2;
	public MeshRenderer meshRenderer;

	public bool isIn = false;


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			isIn = true;
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			isIn = false;
		}
	}


	void Update()
	{
		if (isIn) {
			meshRenderer.material = mat2;
			BossManager.instance.SetFistTrigger (true);

		} else {
			meshRenderer.material = mat1;
			BossManager.instance.SetFistTrigger (false);

		}
	}
}
