using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerVariables : NetworkBehaviour
{
    public void Save()
    {
        DataManager.SavePlayer(this);
    }  

    public void Load()
    {
        unlocks = DataManager.LoadUnlocks();
        HasDoneTutorial = DataManager.LoadTutorial();
        if(transform.GetComponent<Movement>().clientSide.islocal)
        {
            TutorialCPS = DataManager.LoadCheckpoint(0);
            EasyCPS = DataManager.LoadCheckpoint(1);
            MediumCPS = DataManager.LoadCheckpoint(2);
            HardCPS = DataManager.LoadCheckpoint(3);
        }
    } 


    
    public bool downPlatform = false;
    public bool leftRamp = false;
    public float leftRampRot = 0;
    public bool rightRamp = false;
    public float rightRampRot = 0;
    public MoveState moveState = MoveState.TopSurf;
    [SyncVar] public int level = -1;
    public Vector3 startLocation;
    
    public  bool inSettings = false;
    public  bool Paused = false;
    public bool betweenLevels = false;
    
    public int lastCheckpoint = 0;
    public int checkpoints = 0;
    public int permcheckpoints = 0;
    public int unlocks = 0;
    public int TutorialCPS = 0;
    public int EasyCPS = 0;
    public int MediumCPS = 0;
    public int HardCPS = 0;
    public float maxSpeed = 0;

    public bool HasDoneTutorial;
}
