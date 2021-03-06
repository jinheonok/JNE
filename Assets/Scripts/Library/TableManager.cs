﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInfo
{
	public int Number { get; set;}
	public int NextNumber{ get; set;}
	public string Actor{ get; set;}
	public string Text{ get; set;}

	public DialogInfo()
	: this(0, 0, "", "")
	{
	}

	public DialogInfo(int number, int nextNumber, string actor, string text)
	{
		Number = number;
		NextNumber = nextNumber;
		Text = text;
	}
}

public class TableManager : SingletonBase<TableManager> 
{
	[HideInInspector] public Dictionary<int, DialogInfo> DialogList { private set; get;}

	public void Awake()
	{
		DialogList = new Dictionary<int, DialogInfo> ();
	}

	public void SetDialogList ()
	{
		List<string[]> dialogList = TextParser.LoadFile (ResourcesPath.CSV_FolderFath + ResourcesPath.CSV_Dialog);

		if(dialogList == null)
		{
			return;
		}

		for(int i = 0; i < dialogList.Count; i++)
		{
			string[] line = dialogList [i];
			if(line == null)
			{
				JDebugger.Log ("SetDialogList () - string is null");
				return;
			}

			DialogList.Add (int.Parse (line [0]), new DialogInfo(int.Parse(line[0]), int.Parse(line[1]), line[2], line[3]));
		}
	}
}
