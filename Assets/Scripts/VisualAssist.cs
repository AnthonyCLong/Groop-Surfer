using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class VisualAssist : NetworkBehaviour {
    public GameObject player;
    public PlayerVariables playerVariables;
    public Image crosshair;
    public Sprite crosshairD;
    public Sprite crosshairL;
    public Sprite crosshairR;
    public Image rampLeft;
    public Image rampRight;
    public Text speed;
    public bool localSession;

    public void Update() {
        //Speed indicator
       if (isLocalPlayer || localSession)
       { 
            speed.text = player.GetComponent<Rigidbody>().velocity.magnitude.ToString();
            //Ramp indicators
            if (playerVariables.leftRamp) {
                rampLeft.gameObject.SetActive(true);
            } else {
                rampLeft.gameObject.SetActive(false);
            }

            if (playerVariables.rightRamp) {
                rampRight.gameObject.SetActive(true);
            } else {
                rampRight.gameObject.SetActive(false);
            }

            //Surf indicators
            if (playerVariables.moveState == MoveState.Default || playerVariables.moveState == MoveState.TopSurf) {
                crosshair.sprite = crosshairD;
                crosshair.color = Color.magenta;
                rampRight.color = Color.magenta;
                rampLeft.color = Color.magenta;

            } else if (playerVariables.moveState == MoveState.LeftSurf) {
                crosshair.sprite = crosshairL;
                float angleBetween = AngleBetween(playerVariables.leftRampRot, player.transform.eulerAngles.y);
                crosshair.color = FloatColor(angleBetween);
                rampLeft.color = crosshair.color;

            } else if (playerVariables.moveState == MoveState.RightSurf) {
                crosshair.sprite = crosshairR;
                float angleBetween = AngleBetween(playerVariables.rightRampRot, player.transform.eulerAngles.y);
                crosshair.color = FloatColor(angleBetween);
                rampRight.color = crosshair.color;
            }
        }
    }

    public float AngleBetween(float angle1, float angle2) {
        if (angle1 > 180) {
            angle1 = angle1 - 360;
        }

        if (angle2 > 180) {
            angle2 = angle2 - 360;
        }

        float ret = Mathf.Abs(angle1 - angle2);

        if (ret > 90) {
            ret = Mathf.Abs(ret - 180);
        }

        return ret;
    }

    public Color FloatColor(float value) {
        float h = Mathf.Clamp((135 - value * 30), 0, 135) / 360f;
        return Color.HSVToRGB(h, 1, 1);
    }
}
