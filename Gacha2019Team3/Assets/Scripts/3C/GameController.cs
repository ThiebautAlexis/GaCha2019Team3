using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerID
{
    One,
    Two
}

public class GameController
{
    public SnakeHead.Direction m_Direction = SnakeHead.Direction.DOWN;
    public EPlayerID m_PlayerId = EPlayerID.One;

    public void Update()
    {
        if (m_PlayerId == EPlayerID.One)
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
        }
        else
        {
            //GNÉ
        }
    }
}
