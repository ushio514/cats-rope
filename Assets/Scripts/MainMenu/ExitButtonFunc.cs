using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ExitButtonFunc : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Application.Quit();
       // UnityEditor.EditorApplication.isPlaying = false;
    }
}
