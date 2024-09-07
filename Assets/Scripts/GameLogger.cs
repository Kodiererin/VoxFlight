using UnityEngine;


/// <summary>
/// GameLogger
/// </summary>
public static class GameLogger
{
    public static void LogAction(string action)
    {
        string timeStamp = System.DateTime.Now.ToString();
        Debug.Log($"[{timeStamp}] {action}");
    }
}