using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayOutTunnel : WayOut
{
    public override void teleport(int direction)
    {
        Vector2 coordonneesTunnel = Tunnel.tunnelPosition;

        Debug.Log(direction);
        if (direction == (int)Direction.Right)
        {
            if(Tunnel.tileRight == (int)TileType.Void || Tunnel.tileRight == (int)TileType.Hero)
            {
                coordonneesTunnel.x = coordonneesTunnel.x + 1;
            }
        }
        else if (direction == (int)Direction.Left)
        {
            if (Tunnel.tileLeft == (int)TileType.Void || Tunnel.tileLeft == (int)TileType.Hero)
            {
                coordonneesTunnel.x = coordonneesTunnel.x - 1;
            }
        }
        else if (direction == (int)Direction.Up)
        {
            if (Tunnel.tileUp == (int)TileType.Void || Tunnel.tileUp == (int)TileType.Hero)
            {
                coordonneesTunnel.y = coordonneesTunnel.y + 1;
            }
        }
        else if (direction == (int)Direction.Down)
        {
            if (Tunnel.tileDown == (int)TileType.Void || Tunnel.tileDown == (int)TileType.Hero)
            {
                coordonneesTunnel.y = coordonneesTunnel.y - 1;
            }
        }

        PlayerTeleporter.teleportPosition = coordonneesTunnel;
        teleporting();
    }
}
