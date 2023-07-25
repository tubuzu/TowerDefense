using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Managers;

[Serializable]
public class SaveData
{
    public bool[] isActive;
    public int[] stars;
}

public class GameData : MonoBehaviour
{
    public static GameData instance;
    public SaveData saveData;
    public int totalLevel;

    public bool firstSplashLaunch;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Load();
        firstSplashLaunch = true;
    }

    public void Save()
    {
        //Create a binary formatter which can read binary files
        BinaryFormatter formatter = new BinaryFormatter();

        //Create a route from the program to the file
        FileStream file = File.Create(Application.persistentDataPath + "/towerdefense-player.dat");

        //Create a copy of save data
        SaveData data = new SaveData();
        data = saveData;

        //Actually save the data in the file
        formatter.Serialize(file, data);

        //Close the data stream
        file.Close();
    }

    public void Load()
    {
        totalLevel = GetLevelsNumber();
        // Check if the save game file exists
        if (File.Exists(Application.persistentDataPath + "/towerdefense-player.dat"))
        {
            //Create a Binary Formatter
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/towerdefense-player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            if (saveData.isActive.Length != GetLevelsNumber())
            {
                InitialNewData();
            }
        }
        else
        {
            InitialNewData();
        }
    }

    private void InitialNewData()
    {
        saveData = new SaveData();
        saveData.isActive = new bool[totalLevel];
        saveData.stars = new int[totalLevel];
        saveData.isActive[0] = true;
    }

    public void UpdateLevelStar(int stars)
    {
        if (stars > saveData.stars[Preferences.GetCurrentLvl() - 1])
        {
            saveData.stars[Preferences.GetCurrentLvl() - 1] = stars;
            Save();
        }
    }

    public void SetActiveLevel(int level)
    {
        saveData.isActive[level] = true;
    }

    private int GetLevelsNumber() => Resources.LoadAll<World>("World")[0].levels.Length;

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }
}
