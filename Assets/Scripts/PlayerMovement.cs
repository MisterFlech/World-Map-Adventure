using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MapObject
{
    public static PlayerMovement _playerMovement;

    public static float speed = 0.075f;
    public static bool freeze = false;
    private bool freezeTalking = false;
    public static Vector2Int startPosition;
    private bool isPushing = false;

    private SpriteAnimation _spriteAnimation;
    private SpriteAnimationPush _spriteAnimationPush;
    public SpriteRenderer spriteRenderer;

    private CanvasGame canvas;

    void Awake()
    {
        if (_playerMovement == null)
        {
            startPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            _playerMovement = this;
            DontDestroyOnLoad(gameObject);

            _spriteAnimation = this.gameObject.GetComponent<SpriteAnimation>();
            _spriteAnimationPush = this.gameObject.GetComponent<SpriteAnimationPush>();
            PlayerTeleporter.teleportPosition = new Vector2(transform.position.x, transform.position.y);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = CanvasGame.instance;
        //initPlayer();
    }

    public void initPlayer()
    {
        Debug.Log("initPlayer " + transform.position);
        GridManager.addElementInGrid(this.gameObject, TileType.Hero, transform.position);
        nextPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {
            if (!freezeTalking)
            {
                if (!canvas.isTalking())
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        int direction = getLookDirection();

                        if (GridManager.checkDirection(direction, transform.position) == (int)TileType.PNJ)
                        {
                            GameObject _gameObject = GridManager.getObjectDirection(direction, transform.position);
                            int numDialog = _gameObject.GetComponent<PNJ>().numDialog;
                            _gameObject.GetComponent<PNJ>().changeDirection(direction);
                            canvas.startDialog(numDialog, -1);
                            freezeTalking = true;
                        }
                    }
                }
            }
            else
            {
                if (!canvas.isTalking())
                {
                    canvas.showWindowText(false);
                    freezeTalking = false;
                }
            }
        }
    }

    private bool goLeft = false;
    private bool goRight = false;
    private bool goUp = false;
    private bool goDown = false;

    private bool _lookLeft = false;
    private bool _lookRight = false;
    private bool _lookUp = false;
    private bool _lookDown = true;

    public bool isMoving = false;

    public SoundEffect soundEffect = new SoundEffect();
    [System.Serializable]
    public class SoundEffect
    {
        [SerializeField] public AudioClip sfx;
    }

    private void resetAllDirections()
    {
        this.goLeft = false;
        this.goRight = false;
        this.goUp = false;
        this.goDown = false;
    }

    private void resetAllLooks(int direction)
    {
        this._lookLeft = false;
        this._lookRight = false;
        this._lookUp = false;
        this._lookDown = false;

        if(Directions.isLeft(direction))
        {
            this._lookLeft = true;
        } else if (Directions.isRight(direction))
        {
            this._lookRight = true;
        }
        else if (Directions.isUp(direction))
        {
            this._lookUp = true;
        }
        else if (Directions.isDown(direction))
        {
            this._lookDown = true;
        }
    }

    public void teleportPlayer(Vector3 position)
    {
        transform.position = position;
    }

    public void teleportPlayer2D(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    /* Factorise des if; */
    private bool canDirection(int direction, int tiletype)
    {
        return GridManager.checkDirection(direction, transform.position) == tiletype;
    }

    private bool canGoDirection(int direction)
    {
        return canDirection(direction, (int)TileType.Void);
    }

    private bool canPushDirection(int direction)
    {
        return canDirection(direction, (int) TileType.Pushable);
    }

    /*
    if (isLeft(direction))
        {
            
        }
        else if (isRight(direction))
        {
            
        }
        else if (isUp(direction))
        {
            
        }
        else if (isDown(direction))
        {
            
        }
    */

    private int getLookDirection()
    {
        if (_lookLeft)
        {
            return (int)Direction.Left;
        }
        else if (_lookRight)
        {
            return (int)Direction.Right;
        }
        else if (_lookUp)
        {
            return (int)Direction.Up;
        }
        else if (_lookDown)
        {
            return (int)Direction.Down;
        }
        return -1;
    }

    public void lookAt()
    {
        if (_lookLeft)
        {
            spriteRenderer.sprite = _spriteAnimation.spritesLeft[0];
        }
        else if (_lookRight)
        {
            spriteRenderer.sprite = _spriteAnimation.spritesRight[0];
        }
        else if (_lookUp)
        {
            spriteRenderer.sprite = _spriteAnimation.spritesUp[0];
        }
        else if (_lookDown)
        {
            spriteRenderer.sprite = _spriteAnimation.spritesDown[0];
        }
    }

    public void lookTo(int direction)
    {
        resetAllLooks(direction);
        lookAt();
    }

    private void pushAt()
    {
        if (_lookLeft)
        {
            spriteRenderer.sprite = _spriteAnimationPush.spritesLeft[0];
        }
        else if (_lookRight)
        {
            spriteRenderer.sprite = _spriteAnimationPush.spritesRight[0];
        }
        else if (_lookUp)
        {
            spriteRenderer.sprite = _spriteAnimationPush.spritesUp[0];
        }
        else if (_lookDown)
        {
            spriteRenderer.sprite = _spriteAnimationPush.spritesDown[0];
        }
    }

    void FixedUpdate()
    {
        if (!freeze && !freezeTalking)
        {
            float movX = Input.GetAxisRaw("Horizontal");
            float movY = Input.GetAxisRaw("Vertical");
            if (!isMoving)
            {
                int direction = -1;
                if(movX < 0)
                {
                    direction = (int)Direction.Left;
                } else if(movX > 0)
                {
                    direction = (int)Direction.Right;
                }
                else if (movY > 0)
                {
                    direction = (int)Direction.Up;
                }
                else if (movY < 0)
                {
                    direction = (int)Direction.Down;
                }

                //S'il bouge
                if(direction != -1)
                {
                    resetAllDirections();
                    resetAllLooks(direction);

                    // S'il peut aller à côté
                    if (canGoDirection(direction))
                    {
                        isMoving = true;

                        if (Directions.isLeft(direction))
                        {
                            goLeft = true;
                            nextPosition.x -= 1;
                            GridManager.MoveLeft(transform.position);
                        }
                        else if (Directions.isRight(direction))
                        {
                            goRight = true;
                            nextPosition.x += 1;
                            GridManager.MoveRight(transform.position);
                        }
                        else if (Directions.isUp(direction))
                        {
                            goUp = true;
                            nextPosition.y += 1;
                            GridManager.MoveUp(transform.position);
                        }
                        else if (Directions.isDown(direction))
                        {
                            goDown = true;
                            nextPosition.y -= 1;
                            GridManager.MoveDown(transform.position);
                        }
                    //S'il peut pousser l'élément à côté
                    } else if(canPushDirection(direction)){
                        GameObject pushable = GridManager.getObjectDirection(direction, transform.position);
                        if (GridManager.checkDirection(direction, pushable.transform.position) == (int)TileType.Void)
                        {
                            pushAt();
                            isPushing = true;
                            pushable.GetComponent<Pushable>().moveDirection(direction);
                            AudioManager.instance.PlaySFX(soundEffect.sfx);
                        }
                        else
                        {
                            lookAt();
                            enterVillage(pushable, direction);
                        }
                    } else { //S'il ne peut rien faire
                        lookAt();
                    }
                } else {
                    //S'il ne bouge pas
                    lookAt();
                }
            }
            else //Il bouge
            {
                if (goLeft)
                {
                    transform.position -= new Vector3(PlayerMovement.speed, 0, 0);
                    bool hasToStop = transform.position.x <= nextPosition.x;
                    float distance = Mathf.Abs(transform.position.x - nextPosition.x);
                    spriteStopAnimation(_spriteAnimationPush.spritesLeft, _spriteAnimation.spritesLeft, hasToStop, distance);
                }
                else if (goRight)
                {
                    transform.position += new Vector3(PlayerMovement.speed, 0, 0);
                    bool hasToStop = transform.position.x >= nextPosition.x;
                    float distance = Mathf.Abs(transform.position.x - nextPosition.x);
                    spriteStopAnimation(_spriteAnimationPush.spritesRight, _spriteAnimation.spritesRight, hasToStop, distance);
                }
                else if (goUp)
                {
                    transform.position += new Vector3(0, PlayerMovement.speed, 0);
                    bool hasToStop = transform.position.y >= nextPosition.y;
                    float distance = Mathf.Abs(transform.position.y - nextPosition.y);
                    spriteStopAnimation(_spriteAnimationPush.spritesUp, _spriteAnimation.spritesUp, hasToStop, distance);
                }
                else if (goDown)
                {
                    transform.position -= new Vector3(0, PlayerMovement.speed, 0);
                    bool hasToStop = transform.position.y <= nextPosition.y;
                    float distance = Mathf.Abs(transform.position.y - nextPosition.y);
                    spriteStopAnimation(_spriteAnimationPush.spritesDown, _spriteAnimation.spritesDown, hasToStop, distance);
                }
            }
        }
    }

    private void spriteStopAnimation(Sprite[] spritesPushing, Sprite[] sprites, bool hasToStop, float distance)
    {
        if (hasToStop)
        {
            spriteStopAnimation(spritesPushing, sprites);
        }
        else
        {
            //Sprite animation
            if (distance > 0.666f)
            {
                spriteAnimation(spritesPushing, sprites, 1);
            }
            else if (distance > 0.333f)
            {
                spriteAnimation(spritesPushing, sprites, 0);
            }
            else
            {
                spriteAnimation(spritesPushing, sprites, 2);
            }
        }
    }

    private void spriteAnimation(Sprite[] spritesPushing, Sprite[] sprites, int i)
    {
        if (isPushing)
        {
            spriteRenderer.sprite = spritesPushing[i];
        }
        else
        {
            spriteRenderer.sprite = sprites[i];
        }
    }

    private void spriteStopAnimation(Sprite[] spritesPushing, Sprite[] sprites)
    {
        if (isPushing)
        {
            spriteRenderer.sprite = spritesPushing[0];
            isPushing = false;
        } else
        {
            spriteRenderer.sprite = sprites[0];
        }
       
        transform.position = new Vector3(nextPosition.x, nextPosition.y, transform.position.z);
        isMoving = false;
    }

    private void enterVillage(GameObject pushable, int direction)
    {
        if (pushable.tag == "Village" || pushable.tag == "WayOut" || pushable.tag == "Tunnel")
        {
            if (Directions.isRight(direction))
            {
                lookRight();
            }
            else if (Directions.isLeft(direction))
            {
                lookLeft();
            }
            else if (Directions.isDown(direction))
            {
                lookDown();
            }
            else if (Directions.isUp(direction))
            {
                lookUp();
            }

            if (pushable.tag == "Village")
            {
                Village _village = pushable.GetComponent<Village>();
                _village.teleport(direction);
            } else if (pushable.tag == "WayOut")
            {
                WayOut _wayouy = pushable.GetComponent<WayOut>();
                _wayouy.teleport(direction);
            }
            else if (pushable.tag == "Tunnel")
            {
                Tunnel _tunnel = pushable.GetComponent<Tunnel>();
                _tunnel.teleport(direction);
            }

        }
    }

    private void lookLeft()
    {
        spriteRenderer.sprite = _spriteAnimation.spritesLeft[0];
    }

    private void lookRight()
    {
        spriteRenderer.sprite = _spriteAnimation.spritesRight[0];
    }

    private void lookUp()
    {
        spriteRenderer.sprite = _spriteAnimation.spritesUp[0];
    }

    private void lookDown()
    {
        spriteRenderer.sprite = _spriteAnimation.spritesDown[0];
    }
}
