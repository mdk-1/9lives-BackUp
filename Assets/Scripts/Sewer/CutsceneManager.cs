using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public string levelToLoad;

    public void ChangeScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
