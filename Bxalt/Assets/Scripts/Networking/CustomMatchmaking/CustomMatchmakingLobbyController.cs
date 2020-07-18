using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomMatchmakingLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject LobbyConnectButton;
    [SerializeField]
    private GameObject LobbyPanel;
    [SerializeField]
    private GameObject MainPanel;
    [SerializeField]
    private TMP_InputField PlayerNameInput;
    private string RoomName;
    private int RoomSize;
    private List<RoomInfo> RoomListing;
    [SerializeField]
    private Transform RoomContainer;
    [SerializeField]
    private GameObject RoomListingPrefab;
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        LobbyConnectButton.SetActive(true);
        RoomListing = new List<RoomInfo>();
        if (PlayerPrefs.HasKey("NickName"))
        {
            if (PlayerPrefs.GetString("NickName") == "")
            {
                PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("NickName");
            }
        }
        else
        {
            PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
        }
        PlayerNameInput.text = PhotonNetwork.NickName;
    }
    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("NickName", nameInput);
    }
    public void JoinLobbyOnClick()
    {
        MainPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if (RoomListing != null)
            {
                tempIndex = RoomListing.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if (tempIndex != -1)
            {
                RoomListing.RemoveAt(tempIndex);
                Destroy(RoomContainer.GetChild(tempIndex).gameObject);
            }
            if (room.PlayerCount > 0)
            {
                RoomListing.Add(room);
                ListRoom(room);
            }
        }
    }
    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }
    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(RoomListingPrefab, RoomContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }

    public void OnRoomNameChanged(string nameIn)
    {
        RoomName = nameIn;
    }
    public void OnRoomSizeChanged(string sizeIn)
    {
        RoomSize = int.Parse(sizeIn);
    }
    public void CreatRoom()
    {
        Debug.Log("Created Room");
        RoomOptions RoomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom(RoomName, RoomOps);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to creat room failed");
    }
    public void MatchMackingCancel()
    {
        MainPanel.SetActive(true);
        LobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
}
