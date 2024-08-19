using UnityEngine;

[CreateAssetMenu(fileName = "Level",menuName = "Level Data")]
public class GameLevelData : ScriptableObject
{
    public GameObject levelPrefab;
    public Material skyboxMat;
    public Color fogColor;
}
