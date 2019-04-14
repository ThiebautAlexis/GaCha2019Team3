using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public SnakeHead.Direction m_Direction = SnakeHead.Direction.NONE;

    public delegate void FirstAbilityEvent();
    public delegate void SecondAbilityEvent();
    public delegate void ThirdAbilityEvent();

    public event FirstAbilityEvent UseFirstAbility; 
    public event FirstAbilityEvent UseSecondAbility; 
    public event FirstAbilityEvent UseThirdAbility;


    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && m_Direction != SnakeHead.Direction.LEFT)
        {
            m_Direction = SnakeHead.Direction.RIGHT;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && m_Direction != SnakeHead.Direction.RIGHT)
        {
            m_Direction = SnakeHead.Direction.LEFT;
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && m_Direction != SnakeHead.Direction.DOWN)
        {
            m_Direction = SnakeHead.Direction.UP;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && m_Direction != SnakeHead.Direction.UP)
        {
            m_Direction = SnakeHead.Direction.DOWN;
        }

        if (Input.GetAxisRaw("FirstAbility") > 0)
        {
            UseFirstAbility();
        }

         if (Input.GetAxisRaw("SecondAbility") > 0)
        {
            UseSecondAbility();
        }

        if (Input.GetAxisRaw("ThirdAbility") > 0)
        {
            UseThirdAbility();
        }

    }
}
