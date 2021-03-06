﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T>: MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;
	private static object _lock = new object();

	public static T Instance
	{
		get
		{
			lock(_lock)
			{
				if(_instance == null)
				{
					_instance = (T) FindObjectOfType (typeof(T));

					if(_instance != null && FindObjectsOfType(typeof(T)).Length > 1)
					{
						JDebugger.Log ("Multiple Singleton");
					}

					if(_instance == null)
					{
						GameObject singleton = new GameObject ();
						_instance = singleton.AddComponent<T> ();
						singleton.name = "(singleton)" + typeof(T).ToString ();
					}
				}

				return _instance;
			}
		}
	}

	public virtual void OnDestroy()
	{
		_instance = null;
	}
}
