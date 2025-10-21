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




        Transform handTransform = Photonplayer.transform.Find("Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand"); // モデルに合わせてパスを調整
        weaponInstance.transform.SetParent(handTransform);
        weaponInstance.transform.localPosition = Vector3.zero; // 手の位置に合わせる
        weaponInstance.transform.localRotation = Quaternion.identity; // 回転も合わせる




        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<newTPS>().enabled = true;
        GameObject hpbar = GameObject.FindWithTag("UI");
        hpbar.GetComponent<HPbar>().enabled = true;

    }
}
