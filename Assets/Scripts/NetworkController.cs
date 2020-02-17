using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public Button joinChat;
    public InputField nameField;
    void JoinChatListener()
    {
        PhotonNetwork.NickName = nameField.text;
        SceneManager.LoadScene("ChatScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        joinChat.onClick.AddListener(JoinChatListener);
    }

    // Update is called once per frame
    void Update()
    {
    }
}