using System.Collections.Generic;
using System.Threading;
using UnityEngine;

static public class GlobalProperties
{
    public static int currentScore;
    public static int totalTrash;
    public static int remainingTrash;
    public static Dictionary<TrashObject, bool> foundTrash = new Dictionary<TrashObject, bool>();
    public static bool isEasy = true;
    public static bool isInMuseum = false;

    static GlobalProperties()
    {
        currentScore = 0;
        foreach (TrashObject value in System.Enum.GetValues(typeof(TrashObject)))
        {
            foundTrash[value] = false;
        }
    }

    public static int AddScore(int score)
    {
        GlobalProperties.currentScore = Mathf.Max(GlobalProperties.currentScore + score, 0);
        return GlobalProperties.currentScore;
    }


    public static bool AddFoundTrash(TrashObject trash)
    {
        bool firstFound = !foundTrash[trash];
        foundTrash[trash] = true;
        return firstFound;
    }

    public static void Reset()
    {
        currentScore = 0;
    }

    public static void ChangeDifficulty()
    {
        isEasy = !isEasy;
    }
}
