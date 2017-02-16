using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int starthp = 100;
    public int currenthp;

    void Awake()
    {
        currenthp = starthp;
    }

    public void TakeDmg(int nasusQ)
    {
        currenthp -= nasusQ;
        if (currenthp <= 0)
        {
            Destroy(gameObject);
        }
    }
}