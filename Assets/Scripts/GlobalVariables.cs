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

public class MapName
{
    public static int getId(string name)
    {
        switch (name)
        {
            case "TitleScreen": return 0;
            case "Introduction": return 1;
            case "MM_HeroVillage": return 2;
            case "WM_FirstMap": return 3;
            case "MM_FirstVillage": return 4;
            case "MM_Vilage2": return 5;
            case "WM_2": return 6;
            case "WM_3": return 7;
            case "WM_4": return 8;
            case "WM_Castles": return 9;
            case "MM_Village3": return 10;
            case "MM_Village4": return 11;
        }
        return -1;
    }
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
