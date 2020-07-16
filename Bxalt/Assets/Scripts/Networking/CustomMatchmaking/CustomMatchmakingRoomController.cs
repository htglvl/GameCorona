using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomMatchmakingRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiPlayerSceneIndex;
    [SerializeField]
    private GameObject LobbyPanel;
    [SerializeField]
    private GameObject RoomPanel;
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private Transform PlayerContainer;
    [SerializeField]
    private GameObject PlayerListingPrefab;
    [SerializeField]
    private TMP_Text RoomNameDisplay;
    void ClearplayerListing()
    {
        for (int i = PlayerContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(PlayerContainer.GetChild(i).gameObject);
        }
    }
    void ListPlayer()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject tempListing = Instantiate(PlayerListingPrefab, PlayerContainer);
            TMP_Text tempText = tempListing.transform.GetChild(0).GetComponent<TMP_Text>();
            tempText.text = player.NickName;
        }
    }
    public override void OnJoinedRoom()
    {
        RoomPanel.SetActive(true);
        LobbyPanel.SetActive(false);
        RoomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
        ClearplayerListing();
        ListPlayer();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ClearplayerListing();
        ListPlayer();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearplayerListing();
        ListPlayer();
        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(multiPlayerSceneIndex);
        }
    }
    IEnumerator reJoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }
    public void BackOnClick()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(reJoinLobby());
    }
}
