using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Mirror;


public static class DataManager
{
    public static void SavePlayer(PlayerVariables player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream (Application.persistentDataPath + "/player.save", FileMode.Create);

        PlayerData data = new PlayerData(player);
        bf.Serialize(stream, data);

        stream.Close();
    }

    public static int LoadUnlocks()
    {
        if(File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream (Application.persistentDataPath + "/player.save", FileMode.Open);
            //Debug.Log(Application.persistentDataPath + "/player.save");

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();

            return data.unlocks;
        }

        else return 1000;
    }
    public static int LoadCheckpoint(int i)
    {
       if(File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream (Application.persistentDataPath + "/player.save", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();

            switch(i)
            {
                case 0:
                    // Debug.Log(data.TutorialCPS);
                    return data.TutorialCPS;  
                case 1:
                    return data.EasyCPS;  
                case 2:
                    return data.MediumCPS;  
                case 3:
                    return data.HardCPS;  
                default:
                    return 0;  
            }

        }

        else return 0;
    } 
    public static bool LoadTutorial()
    {
       if(File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream (Application.persistentDataPath + "/player.save", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();

            return data.HasDoneTutorial;

        }

        else return false;
    } 
}


[Serializable]
public class PlayerData
{
    public int unlocks = 0;
    public int TutorialCPS;
    public int EasyCPS;
    public int MediumCPS;
    public int HardCPS;
    public bool HasDoneTutorial;
    

    public PlayerData(PlayerVariables player)
    {
        unlocks = player.unlocks;
        
        TutorialCPS = player.TutorialCPS;
        EasyCPS = player.EasyCPS;
        MediumCPS = player.MediumCPS;
        HardCPS = player.HardCPS;

        HasDoneTutorial = player.HasDoneTutorial;


    }
}
