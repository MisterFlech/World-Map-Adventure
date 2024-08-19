using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MapObject
{

    private bool goLeft = false;
    private bool goRight = false;
    private bool goUp = false;
    private bool goDown = false;
    private bool isMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GridManager.addElementInGrid(this.gameObject, TileType.Pushable, transform.position);
        nextPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            if (goLeft)
            {
                transform.position -= new Vector3(PlayerMovement.speed, 0, 0);
                if (transform.position.x <= nextPosition.x)
                {
                    stopMoving();
                }
            }
            else if (goRight)
            {
                transform.position += new Vector3(PlayerMovement.speed, 0, 0);
                if (transform.position.x >= nextPosition.x)
                {
                    stopMoving();
                }
            }
            else if (goUp)
            {
                transform.position += new Vector3(0, PlayerMovement.speed, 0);
                if (transform.position.y >= nextPosition.y)
                {
                    stopMoving();
                }
            }
            else if (goDown)
            {
                transform.position -= new Vector3(0, PlayerMovement.speed, 0);
                if (transform.position.y <= nextPosition.y)
                {
                    stopMoving();
                }
            }
        }   
    }

    private void stopMoving()
    {
        transform.position = new Vector3(transform.position.x, nextPosition.y, transform.position.z);
        isMoving = false;
    }

    public void moveDirection(int direction)
    {
        if (Directions.isLeft(direction))
        {
            moveLeft();
        }
        else if (Directions.isRight(direction))
        {
            moveRight();
        }
        else if (Directions.isUp(direction))
        {
            moveUp();
        }
        else if (Directions.isDown(direction))
        {
            moveDown();
        }
    }

    public void moveLeft()
    {
        if (!isMoving)
        {
            resetAllDirections();

            if (GridManager.checkLeft(transform.position) == (int)TileType.Void)
            {
                goLeft = true;
                isMoving = true;
                nextPosition.x -= 1;
                GridManager.MoveLeft(transform.position);
            }
        }
    }

    public void moveRight()
    {
        if (!isMoving)
        {
            resetAllDirections();

            if (GridManager.checkRight(transform.position) == 0)
            {
                goRight = true;
                isMoving = true;
                nextPosition.x += 1;
                GridManager.MoveRight(transform.position);
            }
        }
    }

    public void moveUp()
    {
        if (!isMoving)
        {
            resetAllDirections();

            if (GridManager.checkUp(transform.position) == 0)
            {
                goUp = true;
                isMoving = true;
                nextPosition.y += 1;
                GridManager.MoveUp(transform.position);
            }
        }
    }

    public void moveDown()
    {
        if (!isMoving)
        {
            resetAllDirections();

            if (GridManager.checkDown(transform.position) == 0)
            {
                goDown = true;
                isMoving = true;
                nextPosition.y -= 1;
                GridManager.MoveDown(transform.position);
            }
        }
    }

    private void resetAllDirections()
    {
        this.goLeft = false;
        this.goRight = false;
        this.goUp = false;
        this.goDown = false;
    }
}
