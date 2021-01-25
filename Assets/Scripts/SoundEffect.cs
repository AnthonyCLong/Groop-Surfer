using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SoundEffect : MonoBehaviour {
    public string path;
    public FileInfo[] sounds;

    public void Start() {
        var Info = new DirectoryInfo("Assets/Resources/Checkpoint Sounds");
        sounds = Info.GetFiles("*.mp3");
    }

    public void Fanfare() {
        GetComponent<AudioSource>().volume = .25f;
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Fanfare");
        GetComponent<AudioSource>().Play();
    }

    public void RandomSound() {
        int i = Random.Range(0, sounds.Length);
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Checkpoint Sounds/" + Path.GetFileNameWithoutExtension(sounds[i].Name));
        //GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("HOME - Come Back Down (SwaggerSouls Outro 2018");
        //GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Animal Crossing - K.K. Cruisin' True Remix");
        GetComponent<AudioSource>().Play();
    }

}
