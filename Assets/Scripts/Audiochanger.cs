using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Audiochanger : MonoBehaviour
{
    public FileInfo[] songs;
    public int current = 0;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        var Info = new DirectoryInfo("Assets/Resources");
        songs = Info.GetFiles("*.mp3");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
            nextSong(); 

        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
            previousSong();
        else if (Input.GetKeyDown(KeyCode.M)) 
            PausePlay();

        else if (!GetComponent<AudioSource>().isPlaying && !isPaused && isActiveAndEnabled)
        {
            Debug.Log("switching to next song");
            nextSong();
        }
    }
    void nextSong()
    {
            current++;
            if (current == songs.Length)
                current = 0;
            GetComponent<AudioSource>().Stop();
            Debug.Log(Path.GetFileNameWithoutExtension(songs[current].Name));
            GetComponent<AudioSource>().clip = (AudioClip) Resources.Load<AudioClip>(Path.GetFileNameWithoutExtension(songs[current].Name));
            GetComponent<AudioSource>().Play(); 
    }
    
    void previousSong()
    {
            current--;
            if (current == -1)
                current = songs.Length-1;
            GetComponent<AudioSource>().Stop();
            Debug.Log(Path.GetFileNameWithoutExtension(songs[current].Name));
            GetComponent<AudioSource>().clip = (AudioClip) Resources.Load<AudioClip>(Path.GetFileNameWithoutExtension(songs[current].Name));
            GetComponent<AudioSource>().Play();
    }

    public void PausePlay()
    {
        if (!isPaused)
            {
                GetComponent<AudioSource>().Pause();
                isPaused = true;
            }
        else 
        {
            GetComponent<AudioSource>().Play();
            isPaused = false;

        }
    }

    public void Stop()
    {
        GetComponent<AudioSource>().Stop();
        isPaused = false;
    }

}
