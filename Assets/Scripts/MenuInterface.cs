using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MenuInterface : MonoBehaviour
{
    public GameObject Player;
    public GameObject settingsMenuUI;
    public GameObject PauseMenuUI;
    public GameObject MultiplayerUI;
    public GameObject connectingUI;
    public GameObject MainMenu;
    public GameObject LevelEnd;
    public GameObject crosshair;
    public GameObject Checkpoint;
    public GameObject GlobalCheckpoint;
    public Text timeText;
    public float time;
    public string minutes;
    public string seconds;
    public string miliseconds;

    public bool attemptingConnection = false;
    public bool anySession = false;
    public bool timer = false;
    //public Audiochanger audioChanger;
    public int hostlevel = -1;
    public NetworkManagerSurfer networkManagerSurfer;

     public void QuitPlaying()
     {
        anySession = false;
        timer = false;
        minutes = seconds = miliseconds = "";
        Player.GetComponent<Movement>().unSetClient();
        transform.GetComponent<PlayerScripts>().SetToolTipsInactive();

        if(Player)
            Destroy(Player);
        //Debug.Log(NetworkClient.isConnected);
        if (NetworkServer.active && NetworkClient.isConnected)
            networkManagerSurfer.StopHost();
        else if (NetworkClient.isConnected)
            networkManagerSurfer.StopClient();
            
        transform.GetComponent<ClientSide>().closeMPMenu();
        LevelEnd.SetActive(false);
        PauseMenuUI.SetActive(false);
        crosshair.SetActive(false);
        Checkpoint.SetActive(false);
        MainMenu.SetActive(true);
        settingsMenuUI.SetActive(false);
        GlobalCheckpoint.SetActive(false);
        transform.GetComponent<ClientSide>().GlobalCheckpoint.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = "No Checkpoints";
        transform.GetComponent<ClientSide>().GlobalCheckpoint.transform.GetChild(1).GetComponent<CustomButton>().gameObject.SetActive(false);
        transform.GetComponent<ClientSide>().GlobalCheckpoint.transform.GetChild(2).GetComponent<CustomButton>().gameObject.SetActive(false); 
        
        Cursor.lockState = CursorLockMode.None;
        //audioChanger.Stop();
        time = 0;
        Time.timeScale = 1;
        transform.GetComponent<ClientSide>().islocal = true;

        ResetButtons();
        
     }
     public void HostAndJoin(bool local)
    {
        MainMenu.SetActive(false);
        transform.GetComponent<ClientSide>().levelSelect.SetActive(false);
        transform.GetComponent<ClientSide>().inLevelSelect = false;
        transform.GetComponent<ClientSide>().closeMPMenu();
        MultiplayerUI.SetActive(false);
        crosshair.SetActive(true);
        
        transform.GetComponent<ClientSide>().islocal = local;
        anySession = true;
        timer = true;
        // Player.GetComponent<PlayerScripts>().setLevel(networkManagerSurfer.level);
    }
    
    // public void setLevel()
    // {
    //     Player.GetComponent<PlayerScripts>().setLevel(networkManagerSurfer.level);
    // }
    public void praticeOrMultiplayer(int i)
    {
        if(transform.GetComponent<ClientSide>().inMPMenu && !anySession)
            {
                //networkManagerSurfer.level = i;
                hostlevel = i;
                networkManagerSurfer.StartHost();
                //  Debug.Log("option 2");

                // Player.GetComponent<PlayerScripts>().setLevel(i);
                //menuInterface.HostAndJoin(false);
            }
            else
            {
                transform.GetComponent<ClientSide>().practice(i);
                HostAndJoin(true);
                // Debug.Log("option 1");
            }
        }
 public void ResetButtons()
    {
        transform.GetComponent<ClientSide>().levelSelect.transform.GetChild(0).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
        transform.GetComponent<ClientSide>().levelSelect.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
        transform.GetComponent<ClientSide>().levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
        transform.GetComponent<ClientSide>().levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
        // levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
        // levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
            
        transform.GetComponent<ClientSide>().levelSelect.transform.GetChild(0).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{praticeOrMultiplayer(0);});
        transform.GetComponent<ClientSide>().levelSelect.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{praticeOrMultiplayer(1);});
        transform.GetComponent<ClientSide>().levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{praticeOrMultiplayer(2);});
        transform.GetComponent<ClientSide>().levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{praticeOrMultiplayer(3);});
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Player.GetComponent<PlayerVariables>().Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.SetActive(true);
        //audioChanger.PausePlay();
        // if(!isMP)
            Time.timeScale = 1;
    }

     public void Quit()
    {
       Application.Quit();
    }    


    void Update()
    {
        if(anySession && timer)
        {
            time += Time.deltaTime;
            minutes = Mathf.Floor(time / 60).ToString("00");
            seconds = Mathf.Floor(time % 60).ToString("00");
            miliseconds = Mathf.Floor(((time%1)*100)).ToString("00");
            
            timeText.text = string.Format("{0}:{1}:{2}", minutes, seconds, miliseconds);
        }
        
        if(NetworkClient.active && !NetworkClient.isConnected)
            {
                connectingUI.SetActive(true);
                MultiplayerUI.SetActive(false);
            }
        else if (!NetworkClient.active && !NetworkClient.isConnected && transform.GetComponent<ClientSide>().inMPMenu && !transform.GetComponent<ClientSide>().inSettings && !transform.GetComponent<ClientSide>().inLevelSelect)
            {
                connectingUI.SetActive(false);
                MultiplayerUI.SetActive(true);
            }
            //Debug.Log(NetworkClient.isConnected);
    }

}
