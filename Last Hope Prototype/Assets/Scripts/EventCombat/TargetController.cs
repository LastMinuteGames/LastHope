
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    public float maxHp = 100;
    public float currentHp;
    [HideInInspector]
    public bool alive = true;
    public GameObject deadExplosion;
    public GameObject deadDecal;
    public Slider hpSlider;

    virtual public void InitData()
    {
        currentHp = maxHp;
        hpSlider.maxValue = maxHp;
        UpdateHpBar();
        alive = true;
    }

    void Start()
    {
        InitData();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack") && alive)
        {
            EnemyTrash trashScript = other.gameObject.GetComponentInParent<EnemyTrash>();
            Attack currentAttackReceived = trashScript.GetAttack();
            if (currentAttackReceived != null)
            {
                TakeDamage(currentAttackReceived.damage);
            }
        }
    }

    virtual protected void TakeDamage(int damage)
    {
        currentHp -= damage;
        UpdateHpBar();
        if (currentHp <= 0)
        {
            SpawnExplosion();
            SpawnDecal();
            alive = false;
        }
    }

    virtual protected void SpawnExplosion()
    {
        Instantiate(deadExplosion, transform.position, transform.rotation);
    }

    virtual protected void SpawnDecal()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);
        Quaternion hitRotation = Quaternion.Euler(90, Random.Range(0, 360), 0);
        GameObject spawnedDecal = Instantiate(deadDecal, hit.point + new Vector3(0, 0.001f, 0), hitRotation);
        spawnedDecal.transform.localScale *= 5;
    }

    virtual protected void UpdateHpBar()
    {
        hpSlider.value = currentHp;
    }
}
