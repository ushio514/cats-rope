using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonFunc : MonoBehaviour
{
    private BackgroundEffect backgroundEffect = null;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        backgroundEffect = GameObject.Find("MenuCanvas").GetComponent<BackgroundEffect>();
    }

    public void OnClick()
    {
        backgroundEffect.DoFade(OnSwitchLevel);
    }

    private void OnSwitchLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Level1");
    }
}
