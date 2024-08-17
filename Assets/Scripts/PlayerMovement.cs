using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MapObject
{
    public static PlayerMovement _playerMovement;

    public static float speed = 0.075f;
    public static bool freeze = false;

    private SpriteAnimation _spriteAnimation;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (_playerMovement == null)
        {
            _playerMovement = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GridManager.addElementInGrid(this.gameObject, TileType.Hero, transform.position);
        nextPosition = new Vector2(transform.position.x, transform.position.y);
        _spriteAnimation = this.gameObject.GetComponent<SpriteAnimation>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(GridManager.checkTop(transform.position));
            Debug.Log(GridManager.checkRight(transform.position));
            Debug.Log(GridManager.checkBottom(transform.position));
            Debug.Log(GridManager.checkLeft(transform.position));
            
            /*
            GameObject o_tmp = GridManager.getObjectRight(transform.position);
            if(o_tmp != null)
            {
                Debug.Log(GridManager.checkLeft(o_tmp.transform.position));
            }
            o_tmp = GridManager.getObjectTop(transform.position);
            if (o_tmp != null)
            {
                Debug.Log(GridManager.checkBottom(o_tmp.transform.position));
            }
            */
        }
    }

    private bool goLeft = false;
    private bool goRight = false;
    private bool goUp = false;
    private bool goDown = false;
    public bool isMoving = false;

    private void resetAllDirections()
    {
        this.goLeft = false;
        this.goRight = false;
        this.goUp = false;
        this.goDown = false;
    }

    void FixedUpdate()
    {
        if (!freeze)
        {
            if (!isMoving)
            {
                float movX = Input.GetAxisRaw("Horizontal");
                float movY = Input.GetAxisRaw("Vertical");

                if (movX < 0) //gauche
                {
                    resetAllDirections();

                    if (GridManager.checkLeft(transform.position) == (int)TileType.Void)
                    {
                        goLeft = true;
                        isMoving = true;
                        nextPosition.x -= 1;
                        GridManager.MoveLeft(transform.position);
                    }
                    else if (GridManager.checkLeft(transform.position) == (int)TileType.Pushable)
                    {
                        spriteRenderer.sprite = _spriteAnimation.spritesLeft[0];
                        GameObject pushable = GridManager.getObjectLeft(transform.position);
                        if (GridManager.checkLeft(pushable.transform.position) == (int)TileType.Void)
                        {
                            pushable.GetComponent<Pushable>().moveLeft();
                        }
                    }
                    else
                    {
                        //Debug.Log(GridManager.checkLeft(transform.position));
                        spriteRenderer.sprite = _spriteAnimation.spritesLeft[0];
                    }

                }
                else if (movX > 0) //droite
                {
                    resetAllDirections();

                    if (GridManager.checkRight(transform.position) == (int)TileType.Void)
                    {
                        goRight = true;
                        isMoving = true;
                        nextPosition.x += 1;
                        GridManager.MoveRight(transform.position);
                    }
                    else if (GridManager.checkRight(transform.position) == (int)TileType.Pushable)
                    {
                        spriteRenderer.sprite = _spriteAnimation.spritesRight[0];
                        GameObject pushable = GridManager.getObjectRight(transform.position);
                        if (GridManager.checkRight(pushable.transform.position) == (int)TileType.Void)
                        {
                            pushable.GetComponent<Pushable>().moveRight();
                        }
                    }
                    else
                    {
                        //Debug.Log(GridManager.checkRight(transform.position));
                        spriteRenderer.sprite = _spriteAnimation.spritesRight[0];
                    }
                }
                else if (movY > 0) //haut
                {
                    resetAllDirections();

                    if (GridManager.checkTop(transform.position) == (int)TileType.Void)
                    {
                        goUp = true;
                        isMoving = true;
                        nextPosition.y += 1;
                        GridManager.MoveUp(transform.position);
                    }
                    else if (GridManager.checkTop(transform.position) == (int)TileType.Pushable)
                    {
                        spriteRenderer.sprite = _spriteAnimation.spritesUp[0];
                        GameObject pushable = GridManager.getObjectTop(transform.position);
                        if (GridManager.checkTop(pushable.transform.position) == (int)TileType.Void)
                        {
                            pushable.GetComponent<Pushable>().moveUp();
                        }
                    }
                    else
                    {
                        //Debug.Log(GridManager.checkTop(transform.position));
                        spriteRenderer.sprite = _spriteAnimation.spritesUp[0];
                    }
                }
                else if (movY < 0) //bas
                {
                    resetAllDirections();

                    if (GridManager.checkBottom(transform.position) == (int)TileType.Void)
                    {
                        goDown = true;
                        isMoving = true;
                        nextPosition.y -= 1;
                        GridManager.MoveDown(transform.position);
                    }
                    else if (GridManager.checkBottom(transform.position) == (int)TileType.Pushable)
                    {
                        spriteRenderer.sprite = _spriteAnimation.spritesDown[0];
                        GameObject pushable = GridManager.getObjectBottom(transform.position);
                        if (GridManager.checkBottom(pushable.transform.position) == (int)TileType.Void)
                        {
                            pushable.GetComponent<Pushable>().moveDown();
                        }
                    }
                    else
                    {
                        //Debug.Log(GridManager.checkBottom(transform.position));
                        spriteRenderer.sprite = _spriteAnimation.spritesDown[0];
                    }
                }
            }
            else
            {
                if (goLeft)
                {
                    transform.position -= new Vector3(PlayerMovement.speed, 0, 0);
                    if (transform.position.x <= nextPosition.x)
                    {
                        spriteRenderer.sprite = _spriteAnimation.spritesLeft[0];
                        transform.position = new Vector3(nextPosition.x, transform.position.y, transform.position.z);
                        isMoving = false;
                    }
                    else
                    {
                        //Sprite animation
                        if (Mathf.Abs(transform.position.x - nextPosition.x) > 0.666f)
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesLeft[1];
                        }
                        else if (Mathf.Abs(transform.position.x - nextPosition.x) > 0.333f)
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesLeft[0];
                        }
                        else
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesLeft[2];
                        }
                    }
                }
                else if (goRight)
                {
                    transform.position += new Vector3(PlayerMovement.speed, 0, 0);
                    if (transform.position.x >= nextPosition.x)
                    {
                        spriteRenderer.sprite = _spriteAnimation.spritesRight[0];
                        transform.position = new Vector3(nextPosition.x, transform.position.y, transform.position.z);
                        isMoving = false;
                    }
                    else
                    {
                        //Sprite animation
                        if (Mathf.Abs(transform.position.x - nextPosition.x) > 0.666f)
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesRight[1];
                        }
                        else if (Mathf.Abs(transform.position.x - nextPosition.x) > 0.333f)
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesRight[0];
                        }
                        else
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesRight[2];
                        }
                    }
                }
                else if (goUp)
                {
                    transform.position += new Vector3(0, PlayerMovement.speed, 0);
                    if (transform.position.y >= nextPosition.y)
                    {
                        spriteRenderer.sprite = _spriteAnimation.spritesUp[0];
                        transform.position = new Vector3(transform.position.x, nextPosition.y, transform.position.z);
                        isMoving = false;
                    }
                    else
                    {
                        //Sprite animation
                        if (Mathf.Abs(transform.position.y - nextPosition.y) > 0.666f)
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesUp[1];
                        }
                        else if (Mathf.Abs(transform.position.y - nextPosition.y) > 0.333f)
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesUp[0];
                        }
                        else
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesUp[2];
                        }
                    }
                }
                else if (goDown)
                {
                    transform.position -= new Vector3(0, PlayerMovement.speed, 0);
                    if (transform.position.y <= nextPosition.y)
                    {
                        spriteRenderer.sprite = _spriteAnimation.spritesDown[0];
                        transform.position = new Vector3(transform.position.x, nextPosition.y, transform.position.z);
                        isMoving = false;
                    }
                    else
                    {
                        //Sprite animation
                        if (Mathf.Abs(transform.position.y - nextPosition.y) > 0.666f)
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesDown[1];
                        }
                        else if (Mathf.Abs(transform.position.y - nextPosition.y) > 0.333f)
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesDown[0];
                        }
                        else
                        {
                            spriteRenderer.sprite = _spriteAnimation.spritesDown[2];
                        }
                    }
                }
            }
        }
    }
}
