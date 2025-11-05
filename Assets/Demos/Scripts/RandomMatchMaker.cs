using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.TextCore.Text;

public class RandomMatchMaker : MonoBehaviourPunCallbacks
{
    //インスペクターから設定
    public GameObject PhotonObject;
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        RoomOptions roomOption = new RoomOptions();
        //最大８人
        roomOption.MaxPlayers = 8;
        PhotonNetwork.CreateRoom(null, roomOption);

    }

    public override void OnJoinedRoom()
    {
        GameObject Photonplayer= PhotonNetwork.Instantiate(
            PhotonObject.name,
            new Vector3(0f, 1f, 0f),
            Quaternion.identity,
            0
            );

        GameObject weaponInstance = PhotonNetwork.Instantiate(
            weapon.name,
            new Vector3(0f, 1f, 0f),
            Quaternion.identity,
            0
        );
        PhotonView playerview = Photonplayer.GetComponent<PhotonView>();
        PhotonView weaponview = weaponInstance.GetComponent<PhotonView>();
        weaponview.TransferOwnership(PhotonNetwork.LocalPlayer);
        Debug.Log( weaponview );
        playerview.RPC("AttachWeapon", RpcTarget.AllBuffered, weaponview.ViewID);

        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<newTPS>().enabled = true;
        GameObject hpbar = GameObject.FindWithTag("UI");
        hpbar.GetComponent<HPbar>().enabled = true;

    }



}
