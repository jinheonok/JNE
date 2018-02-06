using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JDebugger : MonoBehaviour
{
	public static void Log(string log)
	{
		#if UNITY_EDITOR
		Debug.Log ("[JNE] :" + log);
		#endif
	}
}
