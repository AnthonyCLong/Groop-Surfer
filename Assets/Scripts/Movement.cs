using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : NetworkBehaviour {
    //public Settings settings;
    
    public float movementSpeed = 1;
    public float mouseSensitivity = 1;
    public PlayerVariables playerVariables;
    public MenuInterface menuInterface;
    public ClientSide clientSide;
    public Rigidbody rigidBody;
    public GameObject head;
    public GameObject body;
    public Image left;
    public Image right;
    public CustomButton restartButton;
    public CustomButton MenuRestart;

    private Animator animator;
    private float xRot;
    private float yRot;
    public bool localSession;
    public int checkpointHold;
    
    public void Start() {
        if (isLocalPlayer || localSession)
        {
            playerVariables.startLocation = transform.position;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            animator = transform.GetComponent<Animator>();
            xRot = head.transform.eulerAngles.x;
            yRot = head.transform.eulerAngles.y;

            restartButton.onLeftClick.AddListener(delegate{transform.GetComponent<Movement>().Restart(-1);});
            restartButton.onLeftClick.AddListener(delegate{transform.GetComponent<PlayerScripts>().closeLevelComplete();});

            MenuRestart.onLeftClick.AddListener(delegate{transform.GetComponent<Movement>().Restart(-1);});
            //transform.GetComponent<PlayerScripts>().closeLevelComplete();
            
        }
    }

    public override void OnStartLocalPlayer()
    {
        menuInterface.Player = transform.gameObject;
        clientSide.settings.insFOV();
        clientSide.settings.insSensitivity();
        transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        transform.GetComponent<AudioSource>().enabled = true;
        transform.GetComponent<PlayerVariables>().Load();
        transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1,1,1,.6f);


        if(!isServer)
        {
            clientSide.setClient();
        }
        // else
        // {
        //     clientSide.setLevelButtons();
        // }
        // menuInterface.setLevel();
    }
    public void unSetClient()
    {
        if(!isServer && !clientSide.islocal)
        {
           clientSide.unSetClient(); 
        }
    }
    // [Command] 
    // public void cmdSeetPlayerPrefab()
    // {
    //     if(isLocalPlayer)
    //         transform.gameObject = 
    // }

    // [ClientRpc]
    // public void RpcSyncLevel(int i)
    // {
    //     Debug.Log("execudo somthiting everywhere!");
    //     foreach(NetworkConnection conn in )
    //     transform.GetComponent<PlayerVariables>().level = i; 

    // }

    // [ClientRpc]
    // public void RpcResetAll()
    // {
    //     Restart(-1, true);
    //     // Debug.Log("global");
    // }

    //PUT INPUT RELATED EVENTS HERE
    public void Update() 
    {
        if (isLocalPlayer || localSession)
        {
            if (playerVariables.leftRamp == true && Input.GetKey(KeyCode.A)) 
            {
                playerVariables.moveState = MoveState.LeftSurf;
            } 
            
            else if (playerVariables.rightRamp == true && Input.GetKey(KeyCode.D)) 
            {
                playerVariables.moveState = MoveState.RightSurf;
            } 
            
            else if (playerVariables.downPlatform == true) 
            {
                playerVariables.moveState = MoveState.Default;
            } 
            
            else 
            {
                playerVariables.moveState = MoveState.TopSurf;
            }

            if(!playerVariables.Paused && !playerVariables.betweenLevels && !clientSide.inGlobalCheckpoint && !clientSide.inMenuCheckpoint)
            {
                xRot = head.transform.eulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensitivity;
                yRot = head.transform.eulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
            }

            if (Input.GetKeyDown(KeyCode.R) && (!playerVariables.Paused && !playerVariables.betweenLevels && !clientSide.inGlobalCheckpoint)) 
            {
                Restart(playerVariables.lastCheckpoint);
            }

            else if (Input.GetKeyDown(KeyCode.C) && !playerVariables.Paused && !playerVariables.betweenLevels) 
            {
                if (!clientSide.inGlobalCheckpoint)
                    {
                        clientSide.openGlobalCheckpoint();
                        checkpointHold = playerVariables.lastCheckpoint;
                    }
                else 
                    {
                        clientSide.closeGlobalCheckpoint();
                        playerVariables.lastCheckpoint = checkpointHold;
                        transform.GetComponent<PlayerScripts>().setCheckpointButton();
                    }
            } 
            
            else if (Input.GetKeyDown(KeyCode.Period) && !playerVariables.Paused) 
            {
                Restart(-2);
            } 
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(!playerVariables.Paused && !playerVariables.betweenLevels && !clientSide.inGlobalCheckpoint && !clientSide.inMenuCheckpoint)
                    {
                        pause();
                    }
                
                else if(playerVariables.Paused && !clientSide.inSettings && !clientSide.inLevelSelect)
                {
                    play(); 
                }

                else if(playerVariables.Paused && clientSide.inSettings)
                {    
                    clientSide.closeSettings();
                }
                else if (clientSide.inGlobalCheckpoint)
                    {
                        clientSide.closeGlobalCheckpoint();
                        playerVariables.lastCheckpoint = checkpointHold;
                        transform.GetComponent<PlayerScripts>().setCheckpointButton();
                    }
                else if(clientSide.inMenuCheckpoint)
                    {
                        clientSide.closeMenuCheckpoint();
                    }
                else if(playerVariables.Paused && clientSide.inLevelSelect && !localSession)
                {
                    clientSide.closeMPLevelSelect();
                }

            }
            
            transform.eulerAngles = new Vector3(0, yRot, 0);
            head.transform.eulerAngles = new Vector3(xRot, yRot, 0);
        }
    }



    //PUT ANYTHING THAT MODIFIES PHYSICS HERE
    public void FixedUpdate() 
    {
        
        if ((isLocalPlayer || localSession) && (!playerVariables.Paused && !playerVariables.betweenLevels && !clientSide.inGlobalCheckpoint))
        {
            if (playerVariables.moveState == MoveState.Default) 
            {

                if (Input.GetKey(KeyCode.W)) 
                {
                    float xSpeed = movementSpeed * Mathf.Sin(Mathf.Deg2Rad * head.transform.eulerAngles.y);
                    float zSpeed = movementSpeed * Mathf.Cos(Mathf.Deg2Rad * head.transform.eulerAngles.y);
                    rigidBody.velocity = new Vector3(xSpeed, rigidBody.velocity.y, zSpeed);
                }

            } 
            
            if (playerVariables.moveState == MoveState.TopSurf)
            {

                float horizontalSpeed = Vector3.Magnitude(new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z));
                float xSpeed = horizontalSpeed * Mathf.Sin(Mathf.Deg2Rad * head.transform.eulerAngles.y);
                float zSpeed = horizontalSpeed * Mathf.Cos(Mathf.Deg2Rad * head.transform.eulerAngles.y);
                rigidBody.velocity = new Vector3(xSpeed, rigidBody.velocity.y, zSpeed);

            }
            
            else if (playerVariables.moveState == MoveState.LeftSurf || playerVariables.moveState == MoveState.RightSurf) 
            {
                float speed = rigidBody.velocity.magnitude;
                float horizontalSpeed = Vector3.Magnitude(new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z));
                rigidBody.velocity = head.transform.forward * speed;
                /*if (horizontalSpeed > 30) {
                    rigidBody.velocity = transform.forward * speed;
                }*/
                
            }
        }

    }

    public void Restart(int i)
    {
        
        //if (isLocalPlayer || localSession)
        { 
            if(i >= 0)
            {
                // Debug.Log(transform.GetComponent<PlayerScripts>().canvas.GetComponent<LevelRefrence>().levels[playerVariables.level].GetComponent<CheckpointScript>().Checkpoints[i]);
                transform.position = transform.GetComponent<PlayerScripts>().canvas.GetComponent<LevelRefrence>().levels[playerVariables.level].GetComponent<CheckpointScript>().Checkpoints[i];
                transform.rotation = Quaternion.identity;
                transform.eulerAngles = new Vector3();
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                
                if(playerVariables.betweenLevels)
                    playerVariables.lastCheckpoint = transform.GetComponent<PlayerScripts>().CheckpointNum;
                
                transform.GetComponent<PlayerScripts>().CheckpointNum = playerVariables.lastCheckpoint;
                transform.GetComponent<PlayerScripts>().setCheckpointButton();

            }
            
            else if(i == -1)
            {                
                // Debug.Log("restart called");
                if(playerVariables.checkpoints > playerVariables.permcheckpoints)
                    playerVariables.permcheckpoints = playerVariables.checkpoints;

                playerVariables.checkpoints = 0;

                Restart(0);

                transform.GetComponent<PlayerScripts>().setActiveArrows(false);
                transform.GetComponent<PlayerScripts>().GlobalCheckPointButton.onLeftClick.RemoveAllListeners();
                transform.GetComponent<PlayerScripts>().setButtonText("No Checkpoints");
                
                menuInterface.minutes = menuInterface.seconds = menuInterface.miliseconds = "";
                menuInterface.time = 0;
                playerVariables.maxSpeed = 0;
                menuInterface.timer = true;

                if(playerVariables.Paused)
                    play();
                else if(playerVariables.betweenLevels)
                    transform.GetComponent<PlayerScripts>().closeLevelComplete();
            }
            
            else
            {
                //transform.position = new Vector3(0, 1000, -500);
                transform.position = new Vector3(80545, 1846, 16291);
                transform.rotation = Quaternion.identity;
                transform.eulerAngles = new Vector3();
                transform.GetComponent<Rigidbody>().velocity = Vector3.zero; 
            }
        }
    }

    public void pause()
    {
        if(localSession)
            Time.timeScale = 0;
        menuInterface.PauseMenuUI.SetActive(true);
        transform.GetComponent<PlayerVariables>().Paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        menuInterface.crosshair.SetActive(false);
        //audioChanger.PausePlay();
    }

    public void play()
    {
        menuInterface.PauseMenuUI.SetActive(false);
        transform.GetComponent<PlayerVariables>().Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuInterface.crosshair.SetActive(true);
        //audioChanger.PausePlay();
        if(localSession)
            Time.timeScale = 1;
    }
[ClientRpc]
    public void RpcRestart(int i)
    {
        
        // if(isServer)
        {if(i >= 0)
        {
            transform.position = transform.GetComponent<PlayerScripts>().canvas.GetComponent<LevelRefrence>().levels[playerVariables.level].GetComponent<CheckpointScript>().Checkpoints[i];
            transform.rotation = Quaternion.identity;
            transform.eulerAngles = new Vector3();
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                
            if(playerVariables.betweenLevels)
                playerVariables.lastCheckpoint = transform.GetComponent<PlayerScripts>().CheckpointNum;
                
            transform.GetComponent<PlayerScripts>().CheckpointNum = playerVariables.lastCheckpoint;
            //transform.GetComponent<PlayerScripts>().setCheckpointButton();

        }
            
        else if(i == -1)
        {                

            playerVariables.lastCheckpoint = -1;
            playerVariables.checkpoints = 0;
            // playerVariables.checkpoints = 0;

            RpcRestart(0);

            transform.GetComponent<PlayerScripts>().setActiveArrows(false);
            transform.GetComponent<PlayerScripts>().GlobalCheckPointButton.onLeftClick.RemoveAllListeners();
            transform.GetComponent<PlayerScripts>().setButtonText("No Checkpoints");
                
            menuInterface.minutes = menuInterface.seconds = menuInterface.miliseconds = "";
            menuInterface.time = 0;
            playerVariables.maxSpeed = 0;
            menuInterface.timer = true;

            if(playerVariables.Paused)
                play();
            else if(playerVariables.betweenLevels)
                transform.GetComponent<PlayerScripts>().closeLevelComplete();
        }
            
        else
        {
            //transform.position = new Vector3(0, 1000, -500);
            transform.position = new Vector3(5000, 5050, 5000);
            transform.rotation = Quaternion.identity;
            transform.eulerAngles = new Vector3();
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero; 
        }}
    }

}
