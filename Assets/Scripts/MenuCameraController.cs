using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    public float speed;
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
