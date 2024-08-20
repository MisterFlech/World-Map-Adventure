using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    GameObject player;
    public static Vector2 teleportPosition = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        //player.transform.position = new Vector3(teleportPosition.x, teleportPosition.y, transform.position.z);
        //Debug.Log("player teleporter");
        //player.GetComponent<PlayerMovement>().initPlayer();
    }

    public static void teleport()
    {
        PlayerMovement._playerMovement.teleportPlayer(new Vector3(teleportPosition.x, teleportPosition.y, 0));
    }
}
