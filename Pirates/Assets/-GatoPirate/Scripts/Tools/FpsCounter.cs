using UnityEngine;
using System.Collections;

public class FpsCounter : MonoBehaviour
{
	public UnityEngine.UI.Text fpsText;
	public bool limitFPS;
	string label = "";
	float count;
	private GUIStyle guiStyle = new GUIStyle(); //create a new variable

	void Awake()
    {
		if (limitFPS)
			Application.targetFrameRate = 60;
		guiStyle.fontSize = 5;
		
		DontDestroyOnLoad(gameObject);
    }

    IEnumerator Start()
	{
		GUI.depth = 2;
		while (true)
		{
			if (Time.timeScale == 1)
			{
				yield return new WaitForSeconds(0.1f);
				count = (1 / Time.deltaTime);
				label = "FPS :" + (Mathf.Round(count));
			}
			else
			{
				label = "Pause";
			}
			fpsText.text = label;
			yield return new WaitForSeconds(0.5f);
		}
	}

	//void OnGUI()
	//{
		
	//	//GUI.Label(new Rect(5, 40, 400, 100), label);
	//}
}