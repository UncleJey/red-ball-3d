using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ButtonClick : MonoBehaviour 
{
    public delegate void ClickMethod(Action method=null);
    public ClickMethod clickMethod;
    public Action clickAction;
    public Text label;
	void OnClick()
	{
        if (clickMethod != null)
        {
            clickMethod(clickAction);
        }
		else if(clickAction!=null)
		{
			clickAction();
		}
	}

	
}
