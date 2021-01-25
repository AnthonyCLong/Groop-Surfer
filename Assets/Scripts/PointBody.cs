using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBody : MonoBehaviour {
    public GameObject head;

    public void Update() {
        if (Mathf.Abs(transform.rotation.eulerAngles.y - head.transform.rotation.eulerAngles.y) >= 60) {
            transform.rotation = Quaternion.Euler(0, head.transform.eulerAngles.y, 0);
        }
    }
}
