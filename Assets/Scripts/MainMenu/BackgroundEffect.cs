using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundEffect : MonoBehaviour
{
    private GameObject canvasObject = null;
    private CanvasGroup canvasGroup = null;

    public delegate void FinishCallback();

    private const float fadeRate = 0.05f;
    private bool isFading = false;
    private FinishCallback callbackFunc = null;

    public void Start()
    {
        canvasObject = GameObject.Find("MenuCanvas");
        canvasGroup = canvasObject.GetComponent<CanvasGroup>();
    }

    public void Update()
    {
        if (isFading)
        {
            canvasGroup.alpha -= fadeRate;
            if (canvasGroup.alpha <= 0.0f)
            {
                // Faded, call callback
                isFading = false;
                if (callbackFunc != null)
                {
                    callbackFunc();
                    callbackFunc = null;
                }
            }
        }
    }

    public void DoFade(FinishCallback cb)
    {
        isFading = true;
        callbackFunc = cb;
        canvasGroup.blocksRaycasts = false;
    }

    public void ResetState()
    {
        isFading = false;
        callbackFunc = null;
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
