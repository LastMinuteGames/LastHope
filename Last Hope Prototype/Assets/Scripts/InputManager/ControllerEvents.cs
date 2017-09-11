using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

class Rumble
{
    public float timer;
    public float fadeTime;
    public Vector2 power;
    public void Update()
    {
        this.timer -= Time.deltaTime;
    }
}

public class ControllerEvents : MonoBehaviour
{
    private List<Rumble> rumbleEvents;
    PlayerIndex playerIndex;
    
    void Start()
    {
        rumbleEvents = new List<Rumble>();
    }
    
    void Update()
    {
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                playerIndex = testPlayerIndex;
            }
        }

        GamePadState state = GamePad.GetState(playerIndex);

        if (state.IsConnected)
        {
            HandleRumble();
        }
    }

    void HandleRumble()
    {
        if (rumbleEvents.Count > 0)
        {
            Vector2 currentPower = new Vector2(0, 0);

            for (int i = 0; i < rumbleEvents.Count; ++i)
            {
                rumbleEvents[i].Update();

                if (rumbleEvents[i].timer > 0)
                {
                    float timeLeft = Mathf.Clamp(rumbleEvents[i].timer / rumbleEvents[i].fadeTime, 0f, 1f);
                    currentPower = new Vector2(Mathf.Max(rumbleEvents[i].power.x * timeLeft, currentPower.x),
                                               Mathf.Max(rumbleEvents[i].power.y * timeLeft, currentPower.y));

                    GamePad.SetVibration(playerIndex, currentPower.x, currentPower.y);
                }
                else
                {
                    rumbleEvents.Remove(rumbleEvents[i]);
                }
            }
        } else
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
    }

    public void AddRumble(float timer, Vector2 power, float fadeTime)
    {
        Rumble rumble = new Rumble();

        rumble.timer = timer;
        rumble.power = power;
        rumble.fadeTime = fadeTime;

        rumbleEvents.Add(rumble);
    }
}
