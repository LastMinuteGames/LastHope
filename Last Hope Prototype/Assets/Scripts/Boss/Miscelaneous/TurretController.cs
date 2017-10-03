using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : Interactable
{
    [SerializeField]
    private GameObject glow;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private Animator canon;
    [SerializeField]
	private Material baseMaterial;
    [SerializeField]
    private Texture emissiveOff;
    [SerializeField]
    private Texture emissiveOn;
    [SerializeField]
    private float turretRotation;
    [SerializeField]
	private Transform targetT;
    [SerializeField]
    private float speed = 0.5f;

    private Vector3 velocity;

    private bool doOnce = false;
    private bool working = false;
	private bool activated = false;
	private bool rotating = false;
	private float initialRot = 0;

	private bool enabled = false;
	private bool unlocked = false;
	private bool used = false;

	private Vector3 eulerAngles;
	private Vector3 initialEulerAngles;
	private Quaternion targetRotation;




    public void Start()
    {
		eulerAngles = targetT.rotation.eulerAngles;
		initialEulerAngles = eulerAngles;
		targetRotation = targetT.rotation;

		Restart ();


    }

    public void Restart()
    {
		enabled = false;
		unlocked = false;
		used = false;

        baseMaterial.SetTexture("_EmissionMap", emissiveOff);

		targetRotation = Quaternion.Euler (initialEulerAngles);
		targetT.rotation = targetRotation;
    }

    void FixedUpdate()
    {
        if (rotating)
        {
            RotateTurret();
        }
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.U)) {
			Unlock ();
		}
		if (Input.GetKeyDown(KeyCode.I)) {
			Run ();
		}
		if (Input.GetKeyDown(KeyCode.O)) {
			Restart ();
		}


	}

	public void Unlock()
	{
		if (!unlocked && !used) 
		{
			unlocked = true;
			enabled = true;
			baseMaterial.SetTexture("_EmissionMap", emissiveOn);
		}
	}

    public override void Run()
    {
        if (CanInteract())
        {
			enabled = false;
			used = true;
            AudioSources.instance.PlaySound((int)AudiosSoundFX.Boss_Turret1);

            StartCoroutine(Attack());
            glow.GetComponent<ParticleSystem>().Play();
            StartCoroutine(LaserParticles());
            rotating = true;
        }
    }
		
    public override bool CanInteract()
    {
        return enabled;
    }

    private void RotateTurret()
    {
        if (turretRotation >= 0)
        {
            if (targetT.transform.localEulerAngles.z >= initialRot + turretRotation)
            {
                rotating = false;
            }
        }
        else
        {
            if (!doOnce)
            {
                targetT.Rotate(new Vector3(0, 0, 1), -1);
                doOnce = true;
            }
            if (targetT.transform.localEulerAngles.z < initialRot + 360 - Math.Abs(turretRotation))
            {
                rotating = false;
            }
        }
        velocity = Vector3.Lerp(new Vector3(0, 0, initialRot), new Vector3(0, 0, turretRotation), speed * Time.deltaTime);
        targetT.Rotate(new Vector3(0, 0, 1), velocity.z);

    }

    IEnumerator LaserParticles()
    {
        yield return new WaitForSeconds(2.0f);
        laser.GetComponent<ParticleSystem>().Play();
        canon.SetTrigger("Shoot");
    }

    IEnumerator Attack()
    {
        //print(Time.time);
        yield return new WaitForSeconds(3.0f);
        BossManager.instance.TurretAttack();
        baseMaterial.SetTexture("_EmissionMap", emissiveOff);
    }
}