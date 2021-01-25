using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ClientSide : MonoBehaviour
{
    
    public GameObject MainButtons;
    public GameObject MultiplayerUI;
    public GameObject settingsMenuUI;
    public GameObject CustomizeMenuUI;
    public GameObject BasicCustomizeUI;
    public GameObject pauseMenuUI;
    public GameObject canvas;
    public GameObject levelEnd;
    public GameObject Checkpoint;
    public GameObject TutorialTooltip_0;
    public GameObject TutorialTooltip_1;
    public GameObject TutorialTooltip_2;
    public GameObject TutorialTooltip_3;
    public GameObject TutorialTooltip_4;
    public GameObject TutorialTooltip_5;
    public GameObject TutorialTooltip_6;

    public GameObject GlobalCheckpoint;
    public GameObject levelSelect;

    public Image rampLeft;
    public Image rampRight;
    public Image Crosshair;
    public Text speed;

    public GameObject player;
    public GameObject MenuPlayer;

    public CustomButton LeftButton;
    public CustomButton RightButton;
    public CustomButton CheckPointButton;
    
    public MenuInterface menuInterface;
    public PlayerScripts playerScripts;
    public Settings settings;
    public bool islocal = true;
    public bool inSettings = false;
    public bool inMPMenu = false;
    public bool inCstomizeMenu = false;
    public bool inBasicCustomization = false;
    public bool inGlobalCheckpoint = false;
    public bool inMenuCheckpoint = false;
    public bool inLevelSelect = false;

    public AudioMixerGroup Mixer;

    public void Start()
    {
        GameObject menuplayer = Instantiate(MenuPlayer, MenuPlayer.transform.position, MenuPlayer.transform.rotation);
        menuplayer.transform.SetParent(transform.GetChild(0).transform, false);
        settings.LoadVolume();
        settings.LoadFOV();
        settings.LoadSensitivity();

        levelSelect.transform.GetChild(0).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{menuInterface.praticeOrMultiplayer(0);});
        levelSelect.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{menuInterface.praticeOrMultiplayer(1);});
        levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{menuInterface.praticeOrMultiplayer(2);});
        levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{menuInterface.praticeOrMultiplayer(3);});

        //Invoke("levelpractice", .25f);

        // menuplayer.transform.position = MenuPlayer.transform.position;
        // Debug.Log(menuplayer.transform.position);
    }

    public void levelpractice()
    {
        practice(3);
        // menuInterface.Player.GetComponent<Movement>().Restart(-1);
    }
    // public void setLevelButtons()
    // {
    //     levelSelect.transform.GetChild(0).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
    //     levelSelect.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
    //     // levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
    //     // levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.RemoveAllListeners();
        
    //     levelSelect.transform.GetChild(0).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{playerScripts.inSessionMPstart(0);});
    //     levelSelect.transform.GetChild(1).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{playerScripts.inSessionMPstart(1);});
    //     // levelSelect.transform.GetChild(2).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{menuInterface.Player.GetComponent<PlayerScripts>().inSessionMPstart(2);});
    //     // levelSelect.transform.GetChild(3).GetComponent<CustomButton>().onLeftClick.AddListener(delegate{menuInterface.Player.GetComponent<PlayerScripts>().inSessionMPstart(3);});
    // }
    public void Quit()
    {
       Application.Quit();
    }
    public void OpenMPMenu()
    {
        inMPMenu = true;
        MainButtons.SetActive(false);
        MultiplayerUI.SetActive(true);
    }
    public void closeMPMenu()
    {
        inMPMenu = false;
        MultiplayerUI.SetActive(false);
        MainButtons.SetActive(true);
    }
    public void OpenCustomizeMenu()
    {
        inCstomizeMenu = true;
        MainButtons.SetActive(false);
        CustomizeMenuUI.SetActive(true);
    }
    public void closeCustomizeMenu()
    {
        inCstomizeMenu = false;
        CustomizeMenuUI.SetActive(false);
        MainButtons.SetActive(true);
    }
    public void openSettings()
    {
        if(menuInterface.Player)
        {pauseMenuUI.SetActive(false);}
        else 
        {
            if(inMPMenu)
                {MultiplayerUI.SetActive(false);}
            else {MainButtons.SetActive(false);}
        }
        settingsMenuUI.SetActive(true);
        inSettings = true;
    }
    public void closeSettings()
    {
        settingsMenuUI.SetActive(false);
        if(menuInterface.Player)
            {pauseMenuUI.SetActive(true);}
        else 
        {
            if(inMPMenu)
                {MultiplayerUI.SetActive(true);}
            else {MainButtons.SetActive(true);}
        }
        inSettings = false;
    }  
    public void openLevelSelect()
    {
        if(inMPMenu)
        {
            MultiplayerUI.SetActive(false);
            levelSelect.SetActive(true);
            inLevelSelect = true;
        }
        else
        {
            MainButtons.SetActive(false);
            levelSelect.SetActive(true);
            inLevelSelect = true;
        }
    }
    public void closeLevelSelect()
    {
        if(inLevelSelect && inMPMenu)
        {
            inLevelSelect = false;
            MultiplayerUI.SetActive(true);
            levelSelect.SetActive(false);
        }
        else
        {
            MainButtons.SetActive(true);
            levelSelect.SetActive(false);
            inLevelSelect = false;
        }
    }

    public void openBasicCustomize()
    {
        CustomizeMenuUI.SetActive(false);
        BasicCustomizeUI.SetActive(true);
        inBasicCustomization = true;
        inCstomizeMenu = false;
    }
    public void closeBasicCustomize()
    {
        BasicCustomizeUI.SetActive(false);
        CustomizeMenuUI.SetActive(true);
        inBasicCustomization = false;
        inCstomizeMenu = true;
    }  
    public void openGlobalCheckpoint()
    {
        if(islocal)
            Time.timeScale = 0;
        GlobalCheckpoint.SetActive(true);
        inGlobalCheckpoint = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        menuInterface.crosshair.GetComponent<Image>().enabled = false;
    }

    public void closeGlobalCheckpoint()
    {  
        inGlobalCheckpoint = false;
        Cursor.visible = false;
        inMenuCheckpoint = false;
        menuInterface.Player.GetComponent<Movement>().play();
        Cursor.lockState = CursorLockMode.Locked;
        menuInterface.crosshair.GetComponent<Image>().enabled = true;
        GlobalCheckpoint.SetActive(false);
        if(islocal)
            Time.timeScale = 1;
    }

    public void openMenuCheckpoint()
    {
        menuInterface.Player.GetComponent<PlayerVariables>().Paused = false;
        //inGlobalCheckpoint = false;
        inMenuCheckpoint = true;
        pauseMenuUI.SetActive(false);
        GlobalCheckpoint.SetActive(true);
    }
    public void closeMenuCheckpoint()
    {
        GlobalCheckpoint.SetActive(false);
        pauseMenuUI.SetActive(true);
        menuInterface.Player.GetComponent<PlayerVariables>().Paused = true;
        inMenuCheckpoint = false;
    }

    public void closeMPLevelSelect()
    {
        // Debug.Log(menuInterface.Player.GetComponent<PlayerVariables>().Paused);
        if(menuInterface.Player.GetComponent<PlayerVariables>().Paused)
        {
            levelSelect.SetActive(false);
            pauseMenuUI.SetActive(true);
            inLevelSelect = false;
        }
        else
        {
            levelSelect.SetActive(false);
            inLevelSelect = false; 
        }
    }

    public void LevelSelect()
    {
        if(islocal)
        {
            menuInterface.QuitPlaying();
            MainButtons.SetActive(false);
            openLevelSelect();
        }        
        
        else
        {
            pauseMenuUI.SetActive(false);
            levelSelect.SetActive(true);
            inLevelSelect = true;
            //Debug.Log(islocal);
        }
    }

    public void setClient()
    {
        menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false);
        Vector3 button4 = menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.localPosition;
        Vector3 button5 = menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.transform.localPosition;
        Vector3 button6 = menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.localPosition;
            
        menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.localPosition = new Vector3 (button4.x, button4.y + 35, button4.z);
        menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.transform.localPosition = new Vector3 (button5.x, button5.y + 35, button5.z);
        menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.localPosition = new Vector3 (button6.x, button6.y + 35, button6.z);

        levelEnd.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(false);
        levelEnd.transform.GetChild(0).gameObject.transform.GetChild(9).gameObject.SetActive(true);
    }

    public void unSetClient()
    {
        menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(true);
        Vector3 button4 = menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.localPosition;
        Vector3 button5 = menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.transform.localPosition;
        Vector3 button6 = menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.localPosition;
            
        menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.localPosition = new Vector3 (button4.x, button4.y - 35, button4.z);
        menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.transform.localPosition = new Vector3 (button5.x, button5.y - 35, button5.z);
        menuInterface.PauseMenuUI.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.localPosition = new Vector3 (button6.x, button6.y - 35, button6.z);

        levelEnd.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(true);
        levelEnd.transform.GetChild(0).gameObject.transform.GetChild(9).gameObject.SetActive(false);
    }

    public void practice(int i)
    {
        GameObject insPlayer = Instantiate(player, player.transform.position, player.transform.rotation);
        playerScripts = insPlayer.GetComponent<PlayerScripts>();
        insPlayer.transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1,1,1,.6f);

        insPlayer.GetComponent<Movement>().localSession = true;
        insPlayer.GetComponent<PlayerScripts>().localSession = true;
        insPlayer.GetComponent<VisualAssist>().localSession = true;
        
        insPlayer.GetComponent<PlayerScripts>().setLevel(i);
        insPlayer.GetComponent<PlayerScripts>().GlobalCheckpoint = GlobalCheckpoint;

        insPlayer.transform.position = transform.GetComponent<LevelRefrence>().levels[i].GetComponent<CheckpointScript>().Checkpoints[0];
        insPlayer.GetComponent<Movement>().clientSide = this; 

        insPlayer.GetComponent<PlayerVariables>().Load();
        
        switch(i)
                {
                    case 0:
                        insPlayer.GetComponent<PlayerVariables>().checkpoints = insPlayer.GetComponent<PlayerVariables>().TutorialCPS;
                        if(insPlayer.GetComponent<PlayerVariables>().checkpoints != 0)    
                            insPlayer.GetComponent<PlayerVariables>().lastCheckpoint = insPlayer.GetComponent<PlayerVariables>().TutorialCPS - 1;
                        break;
                    case 1:
                        insPlayer.GetComponent<PlayerVariables>().checkpoints = insPlayer.GetComponent<PlayerVariables>().EasyCPS;
                        if(insPlayer.GetComponent<PlayerVariables>().checkpoints != 0)
                            insPlayer.GetComponent<PlayerVariables>().lastCheckpoint = insPlayer.GetComponent<PlayerVariables>().EasyCPS - 1;
                        break;
                    case 2:
                        insPlayer.GetComponent<PlayerVariables>().checkpoints = insPlayer.GetComponent<PlayerVariables>().MediumCPS;
                        if(insPlayer.GetComponent<PlayerVariables>().checkpoints != 0)
                            insPlayer.GetComponent<PlayerVariables>().lastCheckpoint = insPlayer.GetComponent<PlayerVariables>().MediumCPS - 1;
                        break;
                    case 3:
                        insPlayer.GetComponent<PlayerVariables>().checkpoints = insPlayer.GetComponent<PlayerVariables>().HardCPS;
                        if(insPlayer.GetComponent<PlayerVariables>().checkpoints != 0)
                            insPlayer.GetComponent<PlayerVariables>().lastCheckpoint = insPlayer.GetComponent<PlayerVariables>().HardCPS - 1;
                        break;
                }
        
        insPlayer.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        menuInterface.Player = insPlayer;
        settings.insFOV();
        settings.insSensitivity();

        insPlayer.GetComponent<AudioSource>().enabled = true;
        insPlayer.GetComponent<AudioSource>().outputAudioMixerGroup = Mixer;

        insPlayer.GetComponent<Movement>().menuInterface = menuInterface; 
        insPlayer.GetComponent<Movement>().restartButton = transform.GetChild(4).transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<CustomButton>();
        insPlayer.GetComponent<Movement>().MenuRestart = transform.GetChild(5).transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<CustomButton>();
        
        insPlayer.GetComponent<VisualAssist>().crosshair = Crosshair;
        insPlayer.GetComponent<VisualAssist>().rampLeft = rampLeft;
        insPlayer.GetComponent<VisualAssist>().rampRight = rampRight;
        insPlayer.GetComponent<VisualAssist>().speed = speed;

        insPlayer.GetComponent<PlayerScripts>().menuInterface = menuInterface;
        insPlayer.GetComponent<PlayerScripts>().canvas = canvas;
        insPlayer.GetComponent<PlayerScripts>().Checkpoint = Checkpoint;
        
        if(insPlayer.GetComponent<PlayerVariables>().checkpoints != 0)
            insPlayer.GetComponent<PlayerScripts>().setActiveArrows(true);

        if(insPlayer.GetComponent<PlayerVariables>().HasDoneTutorial == false && insPlayer.GetComponent<PlayerVariables>().level == 0)
        {
            if(insPlayer.GetComponent<PlayerVariables>().lastCheckpoint != -1)
                playerScripts.setToolTip(insPlayer.GetComponent<PlayerVariables>().lastCheckpoint);

        }

        insPlayer.GetComponent<PlayerScripts>().LeftButton = LeftButton;
        insPlayer.GetComponent<PlayerScripts>().RightButton = RightButton;
        insPlayer.GetComponent<PlayerScripts>().CheckPointButton = CheckPointButton;
        insPlayer.GetComponent<PlayerScripts>().GlobalCheckPointButton = GlobalCheckpoint.transform.GetChild(0).GetComponent<CustomButton>();

        //insPlayer.GetComponent<PlayerScripts>().GlobalCheckpoint = GlobalCheckpoint;
        insPlayer.GetComponent<Movement>().Restart(insPlayer.GetComponent<PlayerVariables>().lastCheckpoint);
        // insPlayer.GetComponent<PlayerScripts>().setCheckpointButton();
        
        //insPlayer.GetComponent<Movement>().left = rampLeft;
        //insPlayer.GetComponent<Movement>().right = rampRight;
        
    }  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(inSettings && !menuInterface.Player)
            {
                closeSettings();
            }
            
            else if(inMPMenu && !menuInterface.attemptingConnection && !inLevelSelect)
            {
                closeMPMenu();
            }
            
            else if(inCstomizeMenu)
            {
                closeCustomizeMenu();
            }
            
            else if(inBasicCustomization)
            {
                closeBasicCustomize();
            }
            else if(menuInterface.attemptingConnection)
            {
                transform.GetComponent<MenuInterface>().networkManagerSurfer.StopClient();
            }
            else if (levelSelect && islocal)
            {
                closeLevelSelect();
            }
        }

    }

    public void loadGS()
    {
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Customization Scene"));
        SceneManager.LoadScene("Customization Scene");
        //SceneManager.UnloadSceneAsync("Main");
        //SceneManager.SetActiveScene(GSScene);
    }
}
