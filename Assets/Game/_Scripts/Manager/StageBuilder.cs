using UnityEngine;

public class StageBuilder : InstanceManager<StageBuilder>
{


    [SerializeField] GameLevelData[] levelDatas;
    GameLevelData currentLevelData;

    public void SpawnLevel(int index)
    {
        if (index > levelDatas.Length - 1)
            index = GetRandomLevel();

        currentLevelData = levelDatas[index];
        RenderSettings.fogColor = currentLevelData.fogColor;
        RenderSettings.skybox = currentLevelData.skyboxMat;
        Instantiate(currentLevelData.levelPrefab);
        PlayerPrefs.SetInt("LevelIndex", index);

    }

    int GetRandomLevel()
    {
        int index = Random.Range(0, levelDatas.Length);

        int lastLevel = PlayerPrefs.GetInt("LevelIndex");
        if (index == lastLevel)
            return GetRandomLevel();
        else
            return index;
    }

}
