using UnityEngine;

public enum Language
{
    French,
    English
}

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class Directions
{
    public static bool isLeft(int direction)
    {
        return direction == (int)Direction.Left;
    }

    public static bool isRight(int direction)
    {
        return direction == (int)Direction.Right;
    }

    public static bool isUp(int direction)
    {
        return direction == (int)Direction.Up;
    }

    public static bool isDown(int direction)
    {
        return direction == (int)Direction.Down;
    }
}

public class GlobalVariables
{
    //interrupteurs

    public static bool isVillage2Tunnel = false;

    //variables
    public static int language = (int)Language.French;
}
