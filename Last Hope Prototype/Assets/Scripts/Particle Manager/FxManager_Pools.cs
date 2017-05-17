using System.Collections.Generic;
using UnityEngine;

public class FxManager_Pools : MonoBehaviour
{
	public static FxManager_Pools Instance;

	public Transform FxDefaultParent;

	private Dictionary<GameObject, List<GameObject>> mPoolsDictionary;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		mPoolsDictionary = new Dictionary<GameObject, List<GameObject>>();
	}

	public GameObject GetFx(GameObject aPrefab, Vector3 aPosition)
	{
		return GetFxFromPool(aPrefab, aPosition);
	}

	public GameObject GetFx(GameObject aPrefab, Transform aParent)
	{
		return GetFxFromPool(aPrefab, aParent.position, aParent);
	}

	public GameObject GetFxFromPool(GameObject aPrefabType, Vector3 aPositionToSpawn, Transform aParent = null)
	{
		if (aPrefabType == null)
		{
			return null;
		}

		GameObject lFx = null;
		List<GameObject> lFxList;

		if (mPoolsDictionary.TryGetValue(aPrefabType, out lFxList))
		{
			if (lFxList != null)
			{
				for (int i=0; i<lFxList.Count; ++i)
				{
					if (lFxList[i] != null && !lFxList[i].gameObject.activeInHierarchy)
					{
						//nos vale esta instancia, la reusamos!
						lFx = lFxList[i];

						break;
					}
				}
			}
		}

		//no se encontró instancia válida, hay que instanciar
		if (lFx == null)
		{
			lFx = Instantiate(aPrefabType) as GameObject;

			if (mPoolsDictionary.TryGetValue(aPrefabType, out lFxList))
			{
				mPoolsDictionary[aPrefabType].Add(lFx);
			}
			else
			{
				lFxList = new List<GameObject>();
				lFxList.Add(lFx);
				mPoolsDictionary.Add(aPrefabType, lFxList);
			}
		}

		lFx.SetActive(true);

		lFx.transform.position = aPositionToSpawn;

		if (aParent != null)
		{
			lFx.transform.parent = aParent;
		}
		else
		{
			lFx.transform.parent = FxDefaultParent;
		}

		return lFx;
	}
}
