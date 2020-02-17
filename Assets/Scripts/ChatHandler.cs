using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


public class ChatHandler : MonoBehaviourPunCallbacks
{
    public Button sendMessageButton;
    public InputField inputText;
    public GameObject messageTextPrefab;
    public GameObject messagesList;
    public GameObject toHide;
    public GameObject toShow;
    public int roomSize;
    public GameObject scrollView;
    private PhotonView myPhotonView;
    private ScrollRect myScrollRect;
    private bool shouldWeSend = false;


    //Called when Input changes

    //Called when Input is submitted

    void SendAMessage()
    {
        if (inputText.text == String.Empty)
        {
            inputText.ActivateInputField(); //Re-focus on the input field
            inputText.Select(); //Re-focus on the input field

            return;
        }

        var text = PhotonNetwork.NickName + ": " + inputText.text;
        Debug.Log("Sending a message with " + text);
        inputText.text = String.Empty;
        inputText.ActivateInputField(); //Re-focus on the input field
        inputText.Select(); //Re-focus on the input field
        GameObject textMessage = Instantiate(messageTextPrefab, messagesList.transform, false) as GameObject;
        var objectText = textMessage.transform.GetChild(0).GetComponent<Text>();
        objectText.text = text;
        ScrollToBottom();
        myPhotonView.RPC("MessageReceived", RpcTarget.Others, text);
    }

    void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();

        myScrollRect.verticalNormalizedPosition = 0f;

        Canvas.ForceUpdateCanvases();
    }

    [PunRPC]
    private void MessageReceived(string text)
    {
        GameObject textMessage = Instantiate(messageTextPrefab, messagesList.transform, false) as GameObject;
        var objectText = textMessage.transform.GetChild(0).GetComponent<Text>();
        objectText.text = text;
        ScrollToBottom();
    }

    // Start is called before the first frame update
    void Start()
    {
        toHide.SetActive(true);
        toShow.SetActive(false);
        inputText.onEndEdit.AddListener(delegate { SendAMessage(); });
        myPhotonView = GetComponent<PhotonView>();
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LoadLevel("MenuScene");
        }
        else
        {
            ShowChat();
        }

        myScrollRect = scrollView.GetComponent<ScrollRect>();
        ScrollToBottom();
        sendMessageButton.onClick.AddListener(SendAMessage);
    }

    private void ShowChat()
    {
        toHide.SetActive(false);
        toShow.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We have successfully created or joined a room");
    }


    // Update is called once per frame
    void Update()
    {
    }
}