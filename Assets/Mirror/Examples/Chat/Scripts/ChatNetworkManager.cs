using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


namespace Mirror.Examples.Chat
{
    [AddComponentMenu("")]
    public class ChatNetworkManager : NetworkManager
    {
        public string PlayerName { get; set; }

        public Button btn1;
        public Button btn2;


        public void SetHostname(string hostname)
        {
            networkAddress = hostname;
        }

        public ChatWindow chatWindow;

        public class CreatePlayerMessage : MessageBase
        {
            public string name;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreatePlayer);
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            // tell the server to create a player with this name
            conn.Send(new CreatePlayerMessage { name = PlayerName });
        }

        void OnCreatePlayer(NetworkConnection connection, CreatePlayerMessage createPlayerMessage)
        {
            // create a gameobject using the name supplied by client
            GameObject playergo = Instantiate(playerPrefab);
            playergo.GetComponent<Player>().playerName = createPlayerMessage.name;
            playergo.GetComponent<Player>().btn1 = btn1;
            playergo.GetComponent<Player>().btn2 = btn2;

            // set it as the player
            NetworkServer.AddPlayerForConnection(connection, playergo);

            chatWindow.gameObject.SetActive(true);
        }
    }
}