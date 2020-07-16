using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class QuickStarLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject QuickStartButton;
    [SerializeField]
    private GameObject QuickCancelButton;
    [SerializeField]
    private int RoomSize;
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        QuickStartButton.SetActive(true);
    }
    public void QuickStart()
    {
        QuickStartButton.SetActive(false);
        QuickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("quickstartcalled");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("failed To join random room");
        CreatRoom();
    }
    void CreatRoom()
    {
        Debug.Log("creating room");
        int RandomRoomNumber = Random.Range(0, 10000);
        RoomOptions RoomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + RandomRoomNumber, RoomOps);
        Debug.Log(RandomRoomNumber);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed To Creat Room");
        CreatRoom();
    }
    public void QuickCacel()
    {
        QuickCancelButton.SetActive(false);
        QuickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();

    }
}
