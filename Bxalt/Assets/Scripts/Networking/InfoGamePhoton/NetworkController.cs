using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    //Documentation:https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
    //Scripting API: https://doc-api.photonengine.com/en/pun/v2/index.html
    // Start is called before the first frame update
    // if unity editor is connect to a difference region of a standalone build then 
    //set the fixed region in PhotonServerSetting during development process
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server");
    }
}
