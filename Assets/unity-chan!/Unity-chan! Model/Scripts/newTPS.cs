using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class newTPS : MonoBehaviour
{
    public float distance = 5.0f;
    public float mouseSens = 2.0f;
    public float pitchMin = -30f;
    public float pitchMax = 60f;

    private Transform target;
    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
    

    // Update is called once per frame
    void LateUpdate()
        {
            if (target == null)
            {
                Debug.LogWarning("target が設定されていません！");
                return;
            }

            Debug.Log("target の位置: " + target.name);
            float mouseX = Input.GetAxis("Mouse X") * mouseSens;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens;
            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.rotation = rotation;
        transform.position = target.position + offset;

    }
 }

