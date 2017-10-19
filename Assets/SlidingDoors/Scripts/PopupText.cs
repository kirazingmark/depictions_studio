using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
	public static Text textScreen;

	void Start ()
	{
		// cache a reference to the Text component that is in the same GameObject to which an instance of this script has been addeedd
		textScreen = GetComponent<Text>();
	}

	public static void DisplayText (string ptext)
	{
		textScreen.text = ptext;
	}
}