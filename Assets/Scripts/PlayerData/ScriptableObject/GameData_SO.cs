using UnityEngine;

[CreateAssetMenu(fileName = "New GameData", menuName = "SO/GameData")]
public class GameData_SO : ScriptableObject
{
    [Header("Game Data")]
    public float masterVolume;
    public float masterBrightness;
    public int masterResolutionIdx;
}

