using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public SnakeHead.Direction m_Direction = SnakeHead.Direction.DOWN;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_Direction = SnakeHead.Direction.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_Direction = SnakeHead.Direction.RIGHT;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Direction = SnakeHead.Direction.UP;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_Direction = SnakeHead.Direction.DOWN;
        }
    }
}
