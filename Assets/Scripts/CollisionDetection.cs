using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    public PlayerVariables playerVariables;
    public PlayerScripts playerScripts;
    public Direction collisionDirection;
    public int numEnters = 0;

    public void OnTriggerEnter(Collider other) 
    {

        if (other.tag == "Ramp") 
        {
            numEnters++;

            if (collisionDirection == Direction.Left) 
                {
                    playerVariables.leftRamp = true;
                    playerVariables.leftRampRot = other.transform.eulerAngles.y;
                } 
            else if (collisionDirection == Direction.Right) 
                {
                    playerVariables.rightRamp = true;
                    playerVariables.rightRampRot = other.transform.eulerAngles.y;
                }

        } 
        
        else if (other.tag == "Floor")
        {
            playerVariables.GetComponentInParent<Movement>().Restart(playerVariables.lastCheckpoint);
        }
                
        else if (other.tag == "Finish")
        {
            playerVariables.GetComponentInParent<PlayerScripts>().completeLevel(10);
        }

        // else if (other.tag == "Player")
        // {
        //    Debug.Log("1"); 
        // }
        
        else if (collisionDirection == Direction.Down) 
        {
            if (other.tag == "Checkpoint")
            {
                playerScripts.checkpoint(other);
            }
            playerVariables.downPlatform = true;
        }
    }

    public void OnTriggerExit(Collider other) {

        if (other.tag == "Ramp") 
        {  
            numEnters--;

            if (numEnters == 0) {
                if (collisionDirection == Direction.Left)
                    playerVariables.leftRamp = false;
                else if (collisionDirection == Direction.Right)
                    playerVariables.rightRamp = false;
            }

        } 
        else 
        {
            if (collisionDirection == Direction.Down && other.tag != "Player" && other.tag != "Right Collider" && other.tag != "Left Collider" ) 
            {
                playerVariables.downPlatform = false;
            }
        }
    }
}
