using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceen : MonoBehaviour
{
    public void back()
    {
        SceneManager.LoadScene("StartScene");
    }
}
