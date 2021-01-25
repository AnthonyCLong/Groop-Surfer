using System;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace Mirror.Examples.Chat
{
    public class Player : NetworkBehaviour
    {
        [SyncVar]
        public string playerName;
        [SyncVar]
        public int level = -1;
        
        public Button btn1;
        public Button btn2;

        public static event Action<Player, string> OnMessage;

        void Start()
        {
            btn1.onClick.AddListener(delegate{call("test message 1");});
            btn2.onClick.AddListener(delegate{call("test message 2");});
        }

        [Command]
        public void CmdSend(string message)
        {
            if (message.Trim() != "")
                RpcReceive(message.Trim());

            // doOnAll();
        }
        public void call(string message)
        {
            RpcReceive(message);
        }

        [ClientRpc]
        public void RpcReceive(string message)
        {
            Debug.Log("here");
            OnMessage?.Invoke(this, message);
            //level =5;
            Debug.Log("shuld be exe everywhere");
            this.level = 5;
            //Debug.Log(playerName + "-level: " + level);
            //CmdUpdate();
            //level = 5;
        }

        [Command]
        public void CmdUpdate()
        {
            // foreach (NetworkConnection conn in NetworkServer.connections.Values)
            
            level = 5;
            //doOnAll();
        }
       void Update()
       {
           if(Input.GetKeyDown(KeyCode.Keypad0) && isServer)
           {
              doOnAll(0); 
           }
           if(Input.GetKeyDown(KeyCode.Keypad1)&& isServer)
           {
              doOnAll(1); 
           }
           if(Input.GetKeyDown(KeyCode.Keypad2)&& isServer)
           {
              doOnAll(2); 
           }
           if(Input.GetKeyDown(KeyCode.Keypad3)&& isServer)
           {
              doOnAll(3); 
           }
       }
    //    public void button(int i)
    //    {
    //        if (isServer)
    //        {
    //            doOnAll(i);
    //        }
    //    }


        public void doOnAll(int i)
        {
        //   if(isLocalPlayer)
             level = i;
             Debug.Log(playerName + "-level: " + level);
         
        }
    }
}
