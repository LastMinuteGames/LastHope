using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStance : MonoBehaviour
{
    public Sprite stance1Sprite;
    public Sprite stance2Sprite;

    private Image stanceImage;
    //TODO:: change this way to call sprite setting
    private PlayerController playerControl;


    void Awake()
    {
        stanceImage = GetComponent<Image>();
    }

    //private void Start()
    //{
    //    playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //    stanceImage = GetComponent<Image>();
    //}


    //void Update()
    //{
    //    //TODO::change this way to call onstanceupdate
    //    OnStanceUpdate();
    //}


    //public void OnStanceUpdate()
    //{
    //    switch (playerControl.stance)
    //    {
    //        case (PlayerStance.STANCE_BLUE):
    //            stanceImage.sprite = stance1Sprite;
    //            break;

    //        case (PlayerStance.STANCE_RED):
    //            stanceImage.sprite = stance2Sprite;
    //            break;

    //    }
    //}


    public void UpdatePlayerStance(PlayerStance playerStance)
    {
        switch (playerStance.type)
        {
            case (PlayerStanceType.STANCE_BLUE):
                stanceImage.sprite = stance1Sprite;
                break;

            case (PlayerStanceType.STANCE_RED):
                stanceImage.sprite = stance2Sprite;
                break;
        }
    }
}
