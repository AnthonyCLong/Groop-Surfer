using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRefrence : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public GameObject Tutorial;
    public GameObject Easy;
    public GameObject Medium;
    public GameObject Hard;
    void Start()
    {
        levels.Add(Tutorial);
        levels.Add(Easy);
        levels.Add(Medium);
        levels.Add(Hard);
    }
}
