using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerSpecialAttack : MonoBehaviour {

    public GameObject neutralSphere;
    public GameObject redSpehre;
    public float attackDuration = 0.3f;
    public float thrust = 30;

    private bool isDashing = false;
    private Rigidbody rigidBody;
    private float specialAttackTimer = 0f;
    private Vector3 movement;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private PlayerEnergy playerEnergy;
    private Vector3 impulse;

    void Start ()
    {
        neutralSphere.gameObject.SetActive(false);
        redSpehre.gameObject.SetActive(false);
        rigidBody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerMovement = GetComponent<PlayerMovement>();
        playerEnergy = GetComponent<PlayerEnergy>();
    }
	
	void Update ()
    {
        if ((InputManager.SpecialAttack() && !isDashing) || (isDashing))
        {
            SpecialAttack();
        }
	}


    void SpecialAttack()
    {
        if (!isDashing)
        {
            if (playerEnergy.LoseEnergy(1))
            {
                specialAttackTimer = 0;
                isDashing = true;
                playerMovement.enabled = false;
                if (playerController.stance == PlayerStance.STANCE_GREY)
                {
                    neutralSphere.gameObject.SetActive(true);
                }
                else if (playerController.stance == PlayerStance.STANCE_RED)
                {
                    redSpehre.gameObject.SetActive(true);
                }
                movement = rigidBody.velocity;
                impulse = playerMovement.targetDirection.normalized * thrust;
                movement += impulse;
                rigidBody.velocity = movement;
            }
        }
        else
        {
            specialAttackTimer += Time.deltaTime;
            if (specialAttackTimer >= attackDuration)
            {
                isDashing = false;
                playerMovement.enabled = true;
                if (playerController.stance == PlayerStance.STANCE_GREY)
                {
                    neutralSphere.gameObject.SetActive(false);
                }
                else if (playerController.stance == PlayerStance.STANCE_RED)
                {
                    redSpehre.gameObject.SetActive(false);
                }
            }
        }
    }
}
