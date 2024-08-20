using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tunnel : Teleportable
{
    public static Vector3 tunnelPosition = new Vector3(0,0);
    public static int tileLeft = 0;
    public static int tileRight = 0;
    public static int tileUp = 0;
    public static int tileDown = 0;

    public bool doorLeft = false;
    public bool doorRight = false;
    public bool doorUp = false;
    public bool doorDown = false;

    [System.Serializable]
    public class VillageSprites
    {
        [SerializeField] public Sprite left;
        [SerializeField] public Sprite right;
        [SerializeField] public Sprite down;
        [SerializeField] public Sprite left_down;
        [SerializeField] public Sprite right_down;
        [SerializeField] public Sprite left_right;
        [SerializeField] public Sprite left_right_down;
    }

    public SpriteRenderer spriteRenderer;
    [SerializeField] public VillageSprites _villageSprites;

    public Vector2 teleportCoordonneesDoorLeft = new Vector2(0,0);
    public Vector2 teleportCoordonneesDoorRight = new Vector2(0, 0);
    public Vector2 teleportCoordonneesDoorUp = new Vector2(0, 0);
    public Vector2 teleportCoordonneesDoorDown = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {

        if(_villageSprites == null)
        {
            Debug.Log("Objet null");
        } else
        {
            if (doorLeft && !doorRight && !doorUp && !doorDown)
            {
                spriteRenderer.sprite = _villageSprites.left;
            }
            else if (!doorLeft && doorRight && !doorUp && !doorDown)
            {
                spriteRenderer.sprite = _villageSprites.right;
            }
            else if (!doorLeft && !doorRight && !doorUp && doorDown)
            {
                spriteRenderer.sprite = _villageSprites.down;
            }
            else if (doorLeft && !doorRight && !doorUp && doorDown)
            {
                spriteRenderer.sprite = _villageSprites.left_down;
            }
            else if (!doorLeft && doorRight && !doorUp && doorDown)
            {
                spriteRenderer.sprite = _villageSprites.right_down;
            }
            else if (doorLeft && doorRight && !doorUp && !doorDown)
            {
                spriteRenderer.sprite = _villageSprites.left_right;
            }
            else if (doorLeft && doorRight && !doorUp && doorDown)
            {
                spriteRenderer.sprite = _villageSprites.left_right_down;
            }
        }
        
    }

    public void teleport(int direction)
    {
        bool canTeleport = false;
        if (Directions.isRight(direction) && doorLeft)
        {
            canTeleport = true;
        }
        else if (Directions.isLeft(direction) && doorRight)
        {
            canTeleport = true;
        }
        else if (Directions.isDown(direction) && doorUp)
        {
            canTeleport = true;
        }
        else if (Directions.isUp(direction) && doorDown)
        {
            canTeleport = true;
        }
        if (canTeleport)
        {
            GridManager.saveMap();

            if (direction == (int)Direction.Right)
            {
                PlayerTeleporter.teleportPosition = new Vector2(teleportCoordonneesDoorLeft.x, teleportCoordonneesDoorLeft.y);
            }
            else if (direction == (int)Direction.Left)
            {
                PlayerTeleporter.teleportPosition = new Vector2(teleportCoordonneesDoorRight.x, teleportCoordonneesDoorRight.y);
            }
            else if (direction == (int)Direction.Down)
            {
                PlayerTeleporter.teleportPosition = new Vector2(teleportCoordonneesDoorUp.x, teleportCoordonneesDoorUp.y);
            }
            else if (direction == (int)Direction.Up)
            {
                PlayerTeleporter.teleportPosition = new Vector2(teleportCoordonneesDoorDown.x, teleportCoordonneesDoorDown.y);
            }

            Tunnel.tileLeft = GridManager.checkLeft(transform.position);
            Tunnel.tileRight = GridManager.checkRight(transform.position);
            Tunnel.tileUp = GridManager.checkUp(transform.position);
            Tunnel.tileDown = GridManager.checkDown(transform.position);
            Tunnel.tunnelPosition = transform.position;

            Debug.Log(Tunnel.tileLeft + "-" + Tunnel.tileRight + "-" + Tunnel.tileUp + "-" + Tunnel.tileDown);
            teleporting();
        }
    }
}
