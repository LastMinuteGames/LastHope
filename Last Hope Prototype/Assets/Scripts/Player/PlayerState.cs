using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PlayerStateType
{
    PLAYER_STATE_IDLE,
    PLAYER_STATE_MOVE,
    PLAYER_STATE_BLOCK,
    PLAYER_STATE_MOVE_BLOCKING,
    PLAYER_STATE_DODGE,
    PLAYER_STATE_CHANGE_STANCE,
    PLAYER_STATE_INTERACT,
    PLAYER_STATE_DAMAGED,
    PLAYER_STATE_DEAD,
    PLAYER_STATE_RESPAWN,
    PLAYER_STATE_ATTACK,
    PLAYER_STATE_SPECIAL_ATTACK,
    PLAYER_STATE_UNDEFINED
}


public class PlayerState
{
    protected PlayerStateType type;
    protected int numberOfFrames = 0;

    public PlayerState(PlayerStateType type)
    {
        this.type = type;
    }
}
