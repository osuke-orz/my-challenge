using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class HPbar : MonoBehaviour
{

    private Transform target; // キャラクターのTransform
    public Vector3 offset = new Vector3(0, 2.0f, 0); // 頭上に表示

    void Start()
    {
        StartCoroutine(WaitAndSetTarget());
    }

    IEnumerator WaitAndSetTarget()
    {
        while (target == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                PhotonView pv = player.GetComponent<PhotonView>();
                if (pv != null && pv.IsMine)
                {
                    target = player.transform;
                    Debug.Log("target を設定: " + target.name);
                    yield break;
                }
            }
            yield return null; // 次のフレームまで待つ
        }
    }
    void LateUpdate()
    {
        {
            if (target == null)
            {
                Debug.LogWarning("target が設定されていません！");
                return;
            }

            Debug.Log(transform.name+"の位置: " + transform.position);

            transform.position = target.position + offset;
        }
    }
}
