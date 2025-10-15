using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HPbar : MonoBehaviour
{

    private Transform target; // �L�����N�^�[��Transform
    public Vector3 offset = new Vector3(0, 2.0f, 0); // ����ɕ\��

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
                    Debug.Log("target ��ݒ�: " + target.name);
                    yield break;
                }
            }
            yield return null; // ���̃t���[���܂ő҂�
        }
    }
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.forward = Camera.main.transform.forward; // �J�����Ɍ�����
        }
        Debug.Log(transform.position);
    }

}
