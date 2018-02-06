using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class TextParser
{
	//ResourcesPath.CSVDialog
	public static List<string[]> LoadFile(string filePath)
	{		
		TextAsset fileFullPath = Resources.Load<TextAsset> (filePath);

		Debug.Log (filePath);
		string csvTextData = fileFullPath.text;

		string[] rows = csvTextData.Split('\n');

		List<string[]> stringList = new List<string[]> ();

		for(int i = 0; i < rows.Length; i++)
		{
			string[] element = rows [i].Split (',');
			stringList.Add (element);
		}

		return stringList;
	}
}