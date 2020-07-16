using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text SizeText;
    private string RoomName;
    private int RoomSize;
    private int PlayerCount;
    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(RoomName);
    }

    public void SetRoom(string NameInput, int SizeInput, int CountInput)
    {
        RoomName = NameInput;
        RoomSize = SizeInput;
        PlayerCount = CountInput;
        nameText.text = NameInput;
        SizeText.text = CountInput + "/" + SizeInput;
    }
}
