using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLimbs : MonoBehaviour {
    public GameObject head;
    public GameObject torso;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject leftFoot;
    public GameObject rightFoot;

    public Image headButton;
    public Image torsoButton;
    public Image armsButton;
    public Image feetButton;

    public void Head() {
        if (head.activeSelf) {
            head.SetActive(false);
        } else {
            head.SetActive(true);
        }
        
    }

    public void Torso() {
        if (torso.activeSelf) {
            torso.SetActive(false);
        } else {
            torso.SetActive(true);
        }

    }

    public void Arms() {
        if (leftArm.activeSelf) {
            leftArm.SetActive(false);
            rightArm.SetActive(false);
        } else {
            leftArm.SetActive(true);
            rightArm.SetActive(true);
        }

    }

    public void Feet() {
        if (leftFoot.activeSelf) {
            leftFoot.SetActive(false);
            rightFoot.SetActive(false);
        } else {
            leftFoot.SetActive(true);
            rightFoot.SetActive(true);
        }

    }

}
