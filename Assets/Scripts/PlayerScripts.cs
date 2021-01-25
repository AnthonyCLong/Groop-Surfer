using Mirror;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScripts : NetworkBehaviour
{
    public MenuInterface menuInterface;
    public PlayerVariables playerVariables;
    public Movement movement;
    public SoundEffect soundEffect;

    public GameObject canvas;
    public GameObject levelSelect;
    public GameObject Checkpoint;
    public GameObject TutorialTooltip;
    public int CheckpointNum = 0;
    // public int ChangeLevel = -1;
    public bool localSession;

    public CustomButton LeftButton;
    public CustomButton RightButton;
    public CustomButton CheckPointButton;
    public CustomButton  GlobalCheckPointButton;
    public GameObject GlobalCheckpoint;

    // public static event Action<PlayerScripts, string> testhing;
    
    public void Start()
    {
        // Debug.Log(localSession);
        LeftButton.onLeftClick.AddListener(delegate{setButtonLeft();});
        RightButton.onLeftClick.AddListener(delegate{setButtonRight();});

        GlobalCheckpoint.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{setButtonLeft();});
        GlobalCheckpoint.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{setButtonRight();});
        
        if(!menuInterface.GetComponent<ClientSide>().islocal)
            GlobalCheckPointButton = GlobalCheckpoint.transform.GetChild(0).GetComponent<CustomButton>();

         if(isLocalPlayer)
        {
            levelSelect.transform.GetChild(0).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
            levelSelect.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
            levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
            levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
            // levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
            // levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
            
            levelSelect.transform.GetChild(0).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{inSession(0);});
            // levelSelect.transform.GetChild(0).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{transform.GetComponent<Movement>().Restart(0);});
            levelSelect.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{inSession(1);});
            levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{inSession(2);});
            levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{inSession(3);});
            // levelSelect.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{transform.GetComponent<Movement>().Restart(1);});

            // levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{menuInterface.Player.GetComponent<PlayerScripts>().inSessionMPstart(2);});
            // levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{menuInterface.Player.GetComponent<PlayerScripts>().inSessionMPstart(3);});
        }
    }
    
    // [Server]
    public void inSession(int i)
    {
        // Debug.Log("isserver" + isServer);
        // Debug.Log("isclient" + isClient);
        // Debug.Log("isClientOnly" + isClientOnly);
        // Debug.Log("hasAuthority" + hasAuthority);
        // Debug.Log("isLocalPlayer" + isLocalPlayer);
        //  playerVariables.level  = i;
        if(isServer)
         
         {
             RpcinSessionMPstart(i);
            movement.clientSide.closeMPLevelSelect();
            //  Debug.Log(isServer);
         }
            // movement.Restart(-1);
    }

    [Command]
    public void CmdinSessionMPstart(int i)
    {
        RpcinSessionMPstart(i);  
        
    }
   
   [ClientRpc]
    public void RpcinSessionMPstart(int i)
    {
      //if(hasAuthority || !hasAuthority)
    //   Debug.Log("option 3");
      
    //   if(!isServer)
    // return;
        { 
            playerVariables.level  = i;
            SetToolTipsInactive();
            movement.Restart(-1);
            // transform.GetComponent<Movement>().Restart(-1);
            // if()
            //     transform.GetComponent<Movement>().Restart(-1);
        }
    //   Debug.Log(ChangeLevel); 

    }
    

    public void setLevel(int level)
    {
        playerVariables.level = level;
    }
    public void checkpoint(Collider platform)
    {
        if(isLocalPlayer || transform.GetComponent<Movement>().localSession)
        {
            Vector3 pos = platform.transform.GetComponent<Renderer>().bounds.center;
            Vector3 adjustedPos = new Vector3(pos.x, pos.y + (platform.transform.GetComponent<Renderer>().bounds.size.y/2 + 15), pos.z);

            // playerVariables.lastCheckpoint = playerVariables.checkpoints++;
            setActiveArrows(true);
            transform.GetComponent<Movement>().checkpointHold = playerVariables.lastCheckpoint;
            //Debug.Log(adjustedPos);
            // Debug.Log(playerVariables.lastCheckpoint);
            // Debug.Log(playerVariables.checkpoints);
            int CPCheck = canvas.GetComponent<LevelRefrence>().levels[playerVariables.level].GetComponent<CheckpointScript>().Checkpoints.IndexOf(adjustedPos);
            if(playerVariables.checkpoints < CPCheck + 1)
            {
                playerVariables.lastCheckpoint = CPCheck;
                playerVariables.checkpoints = CPCheck+1;
                if(playerVariables.lastCheckpoint!=0)
                {
                    soundEffect.RandomSound();
                    Checkpoint.SetActive(true);
                    Invoke("checkpointDone", 2);
                }
                if(canvas.GetComponent<ClientSide>().islocal)
                {
                    switch(playerVariables.level)
                    {
                        case 0:
                            playerVariables.TutorialCPS = playerVariables.checkpoints;
                            break;
                        case 1:
                            playerVariables.EasyCPS = playerVariables.checkpoints;
                            break;
                        case 2:
                            playerVariables.MediumCPS = playerVariables.checkpoints;
                            break;
                        case 3:
                            playerVariables.HardCPS = playerVariables.checkpoints;
                            break;
                    }
                    if(playerVariables.permcheckpoints < canvas.GetComponent<LevelRefrence>().levels[playerVariables.level].GetComponent<CheckpointScript>().Checkpoints.IndexOf(adjustedPos) + 1)
                    playerVariables.Save();
                }
            }
            else
            {
                playerVariables.lastCheckpoint = CPCheck;
                transform.GetComponent<Movement>().checkpointHold = playerVariables.lastCheckpoint;
            }

            setCheckpointButton();
            if(playerVariables.level==0 && !playerVariables.HasDoneTutorial)
                setToolTip(CPCheck);

        }
    }

    public void setActiveArrows(bool flag)
    {
        if(isLocalPlayer || transform.GetComponent<Movement>().localSession)
        {
            GlobalCheckpoint.transform.GetChild(1).GetComponent<CustomButton>().gameObject.SetActive(flag);
            GlobalCheckpoint.transform.GetChild(2).GetComponent<CustomButton>().gameObject.SetActive(flag); 
      
        }
    }

    public void setButtonText(string text)
    {
       if(isLocalPlayer || transform.GetComponent<Movement>().localSession)
        { 
            GlobalCheckPointButton.transform.GetChild(0).transform.GetComponent<Text>().text = text; 
        }
    }

    public void checkpointDone()
    {
        if(isLocalPlayer || transform.GetComponent<Movement>().localSession)
        {
            Checkpoint.SetActive(false);
        }
    }
    public void completeLevel(int amount)
    {
        if (isLocalPlayer || localSession)
        {  
            transform.GetComponent<Movement>().clientSide.closeGlobalCheckpoint();
            transform.GetComponent<PlayerVariables>().betweenLevels = true;
            soundEffect.Fanfare();
            menuInterface.timer = false;
            if(localSession)
                incrimentUnlocks(amount);
            
            CheckpointNum = 0;
            setCheckpointButton();
            
            canvas.transform.GetChild(4).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().text = menuInterface.timeText.text;
            canvas.transform.GetChild(4).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>().text = transform.GetComponent<PlayerVariables>().maxSpeed.ToString();
            canvas.transform.GetChild(4).gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            menuInterface.crosshair.SetActive(false);

            if(playerVariables.HasDoneTutorial == false && playerVariables.level == 0)
            playerVariables.HasDoneTutorial = true;
            playerVariables.Save();
            SetToolTipsInactive();
        }
    }

    public void closeLevelComplete()
    {
        if (isLocalPlayer || localSession)
        { 
            transform.GetComponent<PlayerVariables>().betweenLevels = false;
            canvas.transform.GetChild(4).gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            menuInterface.crosshair.SetActive(true);
        }
    }

    public void updateMaxSpeed()
    {
        if (isLocalPlayer || localSession)
        { 
            if(transform.GetComponent<Rigidbody>().velocity.magnitude > transform.GetComponent<PlayerVariables>().maxSpeed)
                transform.GetComponent<PlayerVariables>().maxSpeed = transform.GetComponent<Rigidbody>().velocity.magnitude;
        }
    }

    public void incrimentUnlocks(int amount)
    {
        if (isLocalPlayer || localSession)
        {
            transform.GetComponent<PlayerVariables>().unlocks += amount;
            transform.GetComponent<PlayerVariables>().Save();
        }    
    }

    public void setButtonLeft()
    {
        if(isLocalPlayer || transform.GetComponent<Movement>().localSession)
        {
            if(playerVariables.betweenLevels)
                {
                    if(CheckpointNum == 0)
                        CheckpointNum = playerVariables.checkpoints - 1;
                    else CheckpointNum--;

                    setCheckpointButton();
                }
            
            else
                {
                    if(playerVariables.lastCheckpoint == 0)
                        playerVariables.lastCheckpoint = playerVariables.checkpoints - 1;
                    else playerVariables.lastCheckpoint--;
                    
                    setCheckpointButton();
                }
        }
    }
    
    public void setButtonRight()
    {
        if(isLocalPlayer || transform.GetComponent<Movement>().localSession)
        {
            if(playerVariables.betweenLevels)
                {
                    if(CheckpointNum == playerVariables.checkpoints - 1)
                        CheckpointNum = 0;
                    else CheckpointNum++;
                    
                    setCheckpointButton();
                }
            
            else
                {
                    if(playerVariables.lastCheckpoint == playerVariables.checkpoints - 1)
                        playerVariables.lastCheckpoint = 0;
                    else playerVariables.lastCheckpoint++;
                    
                    setCheckpointButton();
                }
        }
    }

    public void setCheckpointButton()
    {        
        if(isLocalPlayer || transform.GetComponent<Movement>().localSession)
        {
            if(playerVariables.betweenLevels)
                {
                    CheckPointButton.onLeftClick.RemoveAllListeners();
                    CheckPointButton.onLeftClick.AddListener(delegate{transform.GetComponent<Movement>().Restart(CheckpointNum);});
                    CheckPointButton.onLeftClick.AddListener(delegate{closeLevelComplete();});
                    CheckPointButton.transform.GetChild(0).transform.GetComponent<Text>().text = "Checkpoint: " + (CheckpointNum + 1).ToString();
                }

                    GlobalCheckPointButton.onLeftClick.RemoveAllListeners();
                    GlobalCheckPointButton.onLeftClick.AddListener(delegate{transform.GetComponent<Movement>().clientSide.closeGlobalCheckpoint();});
                    GlobalCheckPointButton.onLeftClick.AddListener(delegate{transform.GetComponent<Movement>().Restart(playerVariables.lastCheckpoint);});
                    setButtonText("Checkpoint: " + (playerVariables.lastCheckpoint + 1).ToString());
        }
    }

    public void hostLevelChange(int i)
    {
        if(isServer)
        {
            playerVariables.level = i;
        }
    }

    // [Command]
    // public void CmdSetMPLevel(int i)
    // {
    //     //  if(isClient)
    //     // // {
    //         Debug.Log("command");
    //         //playerVariables.level = i;
    //         transform.GetComponent<Movement>().RpcSyncLevel(i);
    //         //menuInterface.Player.GetComponent<Movement>().CmdSyncLevel(i);
    //         // menuInterface.Player.GetComponent<Movement>().Restart(-1, true);
    //         //menuInterface.Player.GetComponent<Movement>().RpcResetAll();
    //         //Debug.Log(menuInterface.Player.GetComponent<PlayerVariables>().level);
    //     // }
    //     // menuInterface.Player.GetComponent<Movement>().Restart(-1, true);
    //     // transform.GetComponent<Movement>().clientSide.levelSelect.SetActive(false);
    //     // transform.GetComponent<Movement>().clientSide.inLevelSelect = false;
    // }

    void Update()
    {
       updateMaxSpeed(); 
    //    if(ChangeLevel != -1)
    //    {
    //       inSessionMPstart(ChangeLevel);
    //       ChangeLevel = -1;
    //    }
       if (Input.GetKeyDown(KeyCode.Keypad0))
       {
        //   if(isLocalPlayer)
        //   {
        //     Debug.Log("keypress");
            inSession(0);
        //   }
        //   if (isServer)
        //   Debug.Log("server");
        //   else Debug.Log("client");
       }
       if (Input.GetKeyDown(KeyCode.Keypad1))
       {
          inSession(1);
       }
       if (Input.GetKeyDown(KeyCode.Keypad2))
       {
          RpcinSessionMPstart(2);
       }
       if (Input.GetKeyDown(KeyCode.Keypad3))
       {
          RpcinSessionMPstart(3);
       }
    }

    public void setToolTip(int i)
    {
       switch(i)
        { 
            case 0:
                SetToolTipsInactive();
                canvas.GetComponent<ClientSide>().TutorialTooltip_0.SetActive(true);
                break;
            case 1:
                SetToolTipsInactive();
                canvas.GetComponent<ClientSide>().TutorialTooltip_1.SetActive(true);
                break;
            case 2:
                SetToolTipsInactive();
                canvas.GetComponent<ClientSide>().TutorialTooltip_2.SetActive(true);
                break;
            case 3:
                SetToolTipsInactive();
                canvas.GetComponent<ClientSide>().TutorialTooltip_3.SetActive(true);
                break;
            case 4:
                SetToolTipsInactive();
                canvas.GetComponent<ClientSide>().TutorialTooltip_4.SetActive(true);
                break;
            case 5:
                SetToolTipsInactive();
                canvas.GetComponent<ClientSide>().TutorialTooltip_5.SetActive(true);
                break;
            case 6:
                SetToolTipsInactive();
                canvas.GetComponent<ClientSide>().TutorialTooltip_6.SetActive(true);
                break;            
        }
    }

    public void SetToolTipsInactive()
    {
        canvas.GetComponent<ClientSide>().TutorialTooltip_0.SetActive(false);
        canvas.GetComponent<ClientSide>().TutorialTooltip_1.SetActive(false);
        canvas.GetComponent<ClientSide>().TutorialTooltip_2.SetActive(false);
        canvas.GetComponent<ClientSide>().TutorialTooltip_3.SetActive(false);
        canvas.GetComponent<ClientSide>().TutorialTooltip_4.SetActive(false);
        canvas.GetComponent<ClientSide>().TutorialTooltip_5.SetActive(false);
        canvas.GetComponent<ClientSide>().TutorialTooltip_6.SetActive(false);
    }

}
