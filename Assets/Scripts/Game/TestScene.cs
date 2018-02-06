using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;

public class TestScene : MonoBehaviour 
{
	public Image _image;
	public Text _text;

	public void Start()
	{
		TableManager.Instance.SetDialogList ();

		if(TableManager.Instance.DialogList[1].NextNumber == 1)
		{
			_image.color = Color.red;
		}
	}
}
