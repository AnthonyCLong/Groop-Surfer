using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Vector3> Checkpoints = new List<Vector3>();
    void Start()
    {
        foreach(Transform child in transform)
            if (child.tag == "Checkpoint")
            {
                Vector3 pos = child.transform.GetComponent<Renderer>().bounds.center;
                Vector3 adjustedPos = new Vector3(pos.x, pos.y + (child.transform.GetComponent<Renderer>().bounds.size.y/2 + 15), pos.z);
                Checkpoints.Add(adjustedPos);
            }

    }
}