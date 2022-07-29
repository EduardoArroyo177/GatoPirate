using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleIndicatorView : MonoBehaviour
{
    [SerializeField]
    private Color activeColor;
    [SerializeField]
    private Color inactiveColor;

    private Image image;

    public void Initialize()
    {
        image = GetComponent<Image>();
    }

    public void SetAsActive()
    {
        image.color = activeColor;
    }

    public void SetAsInactive()
    {
        image.color = inactiveColor;
    }
}
