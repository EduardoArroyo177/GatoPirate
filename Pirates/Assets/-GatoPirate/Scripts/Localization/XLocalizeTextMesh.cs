using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class XLocalizeTextMesh : MonoBehaviour {

	public string Key;
	[HideInInspector]
	public string filePath;

	/// <summary>
	/// Calls the LoadKey on Awake
	/// </summary>
	void Awake()
	{
		LoadKey();
	}

	/// <summary>
	/// Enable or disable the component in here.
	/// </summary>
	void Start() { }

	/// <summary>
	/// Loads the text from the localization file and images from resouces folder
	/// </summary>
	public void LoadKey()
	{
		if (this.GetComponent<TextMeshProUGUI>() != null && !Key.Equals(""))
		{
			this.GetComponent<TextMeshProUGUI>().text = XLocalization.Get(this.gameObject, Key).Replace("\"", "");
		}
		else if (this.GetComponent<TextMeshPro>() != null && !Key.Equals(""))
		{
			this.GetComponent<TextMeshPro>().text = XLocalization.Get(this.gameObject, Key).Replace("\"", "");
		}
		else if (this.GetComponent<Image>() != null)
		{
			filePath = XLocalization.Get(this.gameObject, "FilePath");
			Sprite tempSprite = Resources.Load<Sprite>(filePath + XLocalization.Get(this.gameObject, Key));
			this.GetComponent<Image>().sprite = tempSprite;
		}
		else if (this.GetComponent<RawImage>() != null)
		{
			filePath = XLocalization.Get(this.gameObject, "FilePath");
			Texture tempTexture = Resources.Load<Texture>(filePath + XLocalization.Get(this.gameObject, Key));
			this.GetComponent<RawImage>().texture = tempTexture;
		} 
		else if (this.GetComponent<SpriteRenderer>() != null) 
		{
			filePath = XLocalization.Get(this.gameObject, "FilePath");
			Sprite tempSprite = Resources.Load<Sprite>(filePath + XLocalization.Get(this.gameObject, Key));
			this.GetComponent<SpriteRenderer>().sprite = tempSprite;
		}
	}

	private void SetTextMeshText()
	{
		this.GetComponent<TextMeshProUGUI>().text = XLocalization.Get(this.gameObject, Key).Replace("\"", "");
	}

	private void SetTextMesh(string characterToReplace, string stringToReplace)
	{
		this.GetComponent<TextMeshProUGUI>().text = XLocalization.Get(this.gameObject, Key).Replace(characterToReplace, stringToReplace);
	}

	public void LoadDynamicKey(string dynamicKey)
	{
		Key = dynamicKey;
		LoadKey();
	}

	public void LoadAndAppendDynamicKey(string dynamicKey, string stringToAppend)
	{
		if (this.GetComponent<TextMeshProUGUI>() != null)
		{
			this.GetComponent<TextMeshProUGUI>().text = 
				XLocalization.Get(this.gameObject, dynamicKey).Replace("\"", "") + stringToAppend;
		}
		else if (this.GetComponent<TextMeshPro>() != null)
		{
			this.GetComponent<TextMeshPro>().text = 
				XLocalization.Get(this.gameObject, dynamicKey).Replace("\"", "") + stringToAppend;
		}
	}

	public void LoadAndReplaceDynamicKey(string dynamicKey, string stringToReplace)
	{
		if (this.GetComponent<TextMeshProUGUI>() != null)
		{
			this.GetComponent<TextMeshProUGUI>().text =
				XLocalization.Get(this.gameObject, dynamicKey).Replace("*", stringToReplace);
		}
		else if (this.GetComponent<TextMeshPro>() != null)
		{
			this.GetComponent<TextMeshPro>().text =
				XLocalization.Get(this.gameObject, dynamicKey).Replace("*", stringToReplace);
		}
	}

}
