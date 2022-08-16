using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class OwnedSkinView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lbl_skinName;
    [SerializeField]
    private Image img_skin;
    [SerializeField]
    private GameObject img_unavailableSkin;

    public IntEvent SelectSkinEvent { get; set; }

    public int SkinIndex { get; set; }
    public SkinType SkinType { get; set; }
    public string SkinName { get; set; }

    #region Data set
    public void SetIndexAndType(int _skinIndex, SkinType _skinType)
    {
        SkinIndex = _skinIndex;
        SkinType = _skinType;
    }

    public void SetName(string _skinName)
    {
        SkinName = _skinName;

        lbl_skinName.text = _skinName;
    }

    public void SetSkinSprite(Sprite _skinSprite)
    {
        img_skin.sprite = _skinSprite;
    }
    #endregion

    public void SelectSkin()
    {
        SelectSkinEvent.Raise(SkinIndex);
    }

    public void SetAsAvailable()
    {
        img_unavailableSkin.SetActive(false);
    }

    public void SetAsUnavailable()
    {
        img_unavailableSkin.SetActive(true);
    }
}
