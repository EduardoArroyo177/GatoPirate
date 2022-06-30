using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestRestartTween : MonoBehaviour
{
    public GameObject boxTween;

    DOTweenPath path;

    private void Awake()
    {
        path = boxTween.GetComponent<DOTweenPath>();
    }

    public void ShowBox()
    {
        path.DOPlay();
        boxTween.SetActive(true);
    }

    public void HideBox()
    {
        path.DORestart();
        boxTween.SetActive(false);
    }
}
