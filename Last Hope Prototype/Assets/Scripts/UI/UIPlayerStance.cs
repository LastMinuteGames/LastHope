using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStance : MonoBehaviour
{
    public Sprite stance0Sprite;
    public Sprite stance1Sprite;
    public Sprite stance2Sprite;

    private Image stanceImage;

    void Awake()
    {
        stanceImage = GetComponent<Image>();
		stanceImage.enabled = false;
    }
		
    public void UpdatePlayerStance(PlayerStance playerStance)
    {
		stanceImage.enabled = true;

        switch (playerStance.type)
        {
            case (PlayerStanceType.STANCE_BLUE):
                stanceImage.sprite = stance1Sprite;
                break;

            case (PlayerStanceType.STANCE_RED):
                stanceImage.sprite = stance2Sprite;
                break;
            default:
                stanceImage.sprite = stance0Sprite;
				stanceImage.enabled = false;
                break;
        }
    }
}
