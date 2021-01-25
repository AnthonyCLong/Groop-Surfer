using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace Mirror
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class NetworkManagerSurfer : NetworkManager
    {
     public GameObject localplayer;   
    public Image rampLeft;
    public Image rampRight;
    public Image Crosshair;
    public Text speed;
    public MenuInterface menuInterface;
    public ClientSide clientSide;
    public GameObject canvas;
    public GameObject Checkpoint;
    public GameObject GlobalCheckpoint;
    public GameObject levelSelect;

    public CustomButton LeftButton;
    public CustomButton RightButton;
    public CustomButton CheckPointButton;
    public AudioMixerGroup Mixer;
    public int level = -1;
    

    //public PlayerScripts playerScripts;
        
        // public override void OnStartClient()
        // {
            
        //     playerPrefab.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        // }
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            GameObject player = Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);

            player.GetComponent<PlayerVariables>().level = menuInterface.hostlevel;
            player.transform.position = clientSide.transform.GetComponent<LevelRefrence>().levels[player.GetComponent<PlayerVariables>().level].GetComponent<CheckpointScript>().Checkpoints[0];
            //player.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //player.GetComponent<Movement>().settings = settings;
            // player.GetComponent<Movement>().left = rampLeft;
            // player.GetComponent<Movement>().right = rampRight;
            // player.GetComponent<Movement>().menuInterface = menuInterface;
            // player.GetComponent<Movement>().clientSide = clientSide;

            // //player.GetComponent<Movement>().settings = settings;
            // player.GetComponent<VisualAssist>().crosshair = Crosshair;
            // player.GetComponent<VisualAssist>().rampLeft = rampLeft;
            // player.GetComponent<VisualAssist>().rampRight = rampRight;
            // player.GetComponent<VisualAssist>().speed = speed;

            // player.GetComponent<PlayerScripts>().menuInterface = menuInterface;
            // player.GetComponent<PlayerScripts>().canvas = canvas;
            // player.GetComponent<PlayerScripts>().Checkpoint = Checkpoint;

            NetworkServer.AddPlayerForConnection(conn, player);
            // player.GetComponent<Movement>().HostLevelSet(player.GetComponent<PlayerVariables>().level);
            // player.GetComponent<Movement>().RpcSyncLevel(player.GetComponent<PlayerVariables>().level);
        }

        public override void OnStartClient()
        {
            menuInterface.attemptingConnection = true;
            playerPrefab.GetComponent<AudioSource>().outputAudioMixerGroup = Mixer;

            playerPrefab.GetComponent<Movement>().left = rampLeft;
            playerPrefab.GetComponent<Movement>().right = rampRight;
            playerPrefab.GetComponent<Movement>().menuInterface = menuInterface;
            playerPrefab.GetComponent<Movement>().clientSide = clientSide;
            playerPrefab.GetComponent<Movement>().restartButton = canvas.transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<CustomButton>();
            playerPrefab.GetComponent<Movement>().MenuRestart = canvas.transform.GetChild(5).transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<CustomButton>();


            playerPrefab.GetComponent<VisualAssist>().crosshair = Crosshair;
            playerPrefab.GetComponent<VisualAssist>().rampLeft = rampLeft;
            playerPrefab.GetComponent<VisualAssist>().rampRight = rampRight;
            playerPrefab.GetComponent<VisualAssist>().speed = speed;
            
            playerPrefab.GetComponent<PlayerScripts>().menuInterface = menuInterface;
            playerPrefab.GetComponent<PlayerScripts>().canvas = canvas;
            playerPrefab.GetComponent<PlayerScripts>().Checkpoint = Checkpoint;
            playerPrefab.GetComponent<PlayerScripts>().levelSelect = levelSelect;
            
            playerPrefab.GetComponent<PlayerScripts>().LeftButton = LeftButton;
            playerPrefab.GetComponent<PlayerScripts>().RightButton = RightButton;
            playerPrefab.GetComponent<PlayerScripts>().CheckPointButton = CheckPointButton;
            playerPrefab.GetComponent<PlayerScripts>().GlobalCheckpoint = GlobalCheckpoint;
            clientSide.playerScripts = playerPrefab.GetComponent<PlayerScripts>();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            menuInterface.HostAndJoin(false);
            menuInterface.connectingUI.SetActive(false);
            menuInterface.attemptingConnection = false;
        }
        // public override void OnStopServer()
        // {
        //     Debug.Log("server stopped");
        //     //menuInterface.QuitPlaying();
        // }
        public override void OnStopClient()
        {
            if(menuInterface.anySession)
                menuInterface.QuitPlaying(); 

                menuInterface.attemptingConnection = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                
                //Debug.Log("stop client");
        }
        public void NextLevel()
        {
            if (clientSide.islocal)
            {
                int i = menuInterface.Player.GetComponent<PlayerVariables>().level + 1;
                if(i < canvas.GetComponent<LevelRefrence>().levels.Count)
                {
                    menuInterface.QuitPlaying();
                    clientSide.practice(i);
                    menuInterface.HostAndJoin(true);
                }
            }
            else
            {
                Debug.Log(clientSide.islocal);
            }
        }
    }
}