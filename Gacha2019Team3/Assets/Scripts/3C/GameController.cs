using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNum
{
    Player1 = 1,
    Player2 = 2
}

public class GameController
{
    public SnakeHead.Direction m_Direction = SnakeHead.Direction.NONE;

    public delegate void FirstAbilityEvent();
    public delegate void SecondAbilityEvent();
    public delegate void ThirdAbilityEvent();

    public event FirstAbilityEvent UseFirstAbility; 
    public event FirstAbilityEvent UseSecondAbility; 
    public event FirstAbilityEvent UseThirdAbility;

    public PlayerNum m_PlayerNum = PlayerNum.Player1;
    private string m_SuffixName = "";

    public GameController(PlayerNum _PlayerNum)
    {
        m_PlayerNum = _PlayerNum;

        switch (m_PlayerNum)
        {
            case PlayerNum.Player1:
                m_SuffixName = "J1";
                break;
            case PlayerNum.Player2:
                m_SuffixName = "J2";
                break;
            default:
                break;
        }
    }

    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal" + m_SuffixName) > 0 && m_Direction != SnakeHead.Direction.LEFT)
        {
            m_Direction = SnakeHead.Direction.RIGHT;
        }
        else if (Input.GetAxisRaw("Horizontal" + m_SuffixName) < 0 && m_Direction != SnakeHead.Direction.RIGHT)
        {
            m_Direction = SnakeHead.Direction.LEFT;
        }
        else if (Input.GetAxisRaw("Vertical" + m_SuffixName) > 0 && m_Direction != SnakeHead.Direction.DOWN)
        {
            m_Direction = SnakeHead.Direction.UP;
        }
        else if (Input.GetAxisRaw("Vertical" + m_SuffixName) < 0 && m_Direction != SnakeHead.Direction.UP)
        {
            m_Direction = SnakeHead.Direction.DOWN;
        }

        if (Input.GetAxisRaw("EatAbility" + m_SuffixName) > 0)
        {
            UseFirstAbility();
        }

         if (Input.GetAxisRaw("ShieldAbility" + m_SuffixName) > 0)
        {
            UseSecondAbility();
        }

        if (Input.GetAxisRaw("FreezeAbility" + m_SuffixName) > 0)
        {
            UseThirdAbility();
        }

    }
}
