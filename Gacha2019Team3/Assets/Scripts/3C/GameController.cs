using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public SnakeHead.Direction m_Direction = SnakeHead.Direction.NONE;

    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            m_Direction = SnakeHead.Direction.RIGHT;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            m_Direction = SnakeHead.Direction.LEFT;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            m_Direction = SnakeHead.Direction.UP;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            m_Direction = SnakeHead.Direction.DOWN;
        }

        if (Input.GetAxisRaw("FirstAbility") > 0)
        {

        }
    }
}
