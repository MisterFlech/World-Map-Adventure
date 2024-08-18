using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MapObject
{
    public static PlayerMovement _playerMovement;

    public static float speed = 0.075f;
    public static bool freeze = false;
    public static Vector2Int startPosition;
    private bool isPushing = false;

    private SpriteAnimation _spriteAnimation;
    private SpriteAnimationPush _spriteAnimationPush;
    public SpriteRenderer spriteRenderer;

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
        initPlayer();
    }

    public void initPlayer()
    {
        GridManager.addElementInGrid(this.gameObject, TileType.Hero, transform.position);
        nextPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {


    }

    private bool goLeft = false;
    private bool goRight = false;
    private bool goUp = false;
    private bool goDown = false;

    private bool _lookLeft = true;
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

    private void lookAt()
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
        if (!freeze)
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
                            AudioManager.PlaySFX(soundEffect.sfx);
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
            else
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
       
        transform.position = new Vector3(nextPosition.x, transform.position.y, transform.position.z);
        isMoving = false;
    }

    private void enterVillage(GameObject pushable, int direction)
    {
        Debug.Log("enter ?");
        if (pushable.tag == "Village")
        {
            Debug.Log("village ?");
            Village village = pushable.GetComponent<Village>();
            bool canTeleport = false;
            if (Directions.isRight(direction) && village.doorLeft)
            {
                canTeleport = true;
                lookRight();
            } else if (Directions.isLeft(direction) && village.doorRight)
            {
                canTeleport = true;
                lookLeft();
            }
            else if (Directions.isDown(direction) && village.doorUp)
            {
                canTeleport = true;
                lookDown();
            }
            else if (Directions.isUp(direction) && village.doorDown)
            {
                canTeleport = true;
                lookUp();
            }

            if (canTeleport)
            {
                Debug.Log("Enter village");
                string map_name = village.teleport;

                if (direction == (int)Direction.Right)
                {
                    PlayerTeleporter.teleportPosition = new Vector2(village.teleportCoordonneesDoorLeft.x, village.teleportCoordonneesDoorLeft.y);
                } else if (direction == (int)Direction.Left)
                {
                    PlayerTeleporter.teleportPosition = new Vector2(village.teleportCoordonneesDoorRight.x, village.teleportCoordonneesDoorRight.y);
                } else if (direction == (int)Direction.Down)
                {
                    PlayerTeleporter.teleportPosition = new Vector2(village.teleportCoordonneesDoorUp.x, village.teleportCoordonneesDoorUp.y);
                } else if (direction == (int)Direction.Up)
                {
                    PlayerTeleporter.teleportPosition = new Vector2(village.teleportCoordonneesDoorDown.x, village.teleportCoordonneesDoorDown.y);
                }

                SceneManager.LoadScene(map_name);
                
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
