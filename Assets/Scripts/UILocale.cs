using UnityEngine;
using UnityEngine.UI;

public class UILocale : MonoBehaviour 
{
	public string locale;
	public string valueA;
	public string valueB;

	Text text;

	void OnEnable ()
	{
		if (text == null)
		{
			text  = GetComponent<Text>();
			if (text == null)
				text  = GetComponentInChildren<Text>();
		}

		if (text != null)
			text.text = Language.GetValue(locale, valueA, valueB);
	}

	public string a
	{
		set
		{
			valueA = value;
			OnEnable ();
		}
		get
		{
			return valueA;
		}
	}
}
