using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEditor;
using UnityEngine;

public class EndMenu : GlobalEventListener
{
    public void MainMenu()
    {
        BoltLauncher.Shutdown();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void QUIT()
    {
        Application.Quit();
    }
}
