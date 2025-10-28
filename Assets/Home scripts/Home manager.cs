using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Homemanager : MonoBehaviour
{
    public void OnstartButton()
    {
        SceneManager.LoadScene("Demos");
    }
}
