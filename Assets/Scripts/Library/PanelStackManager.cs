using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelStackManager : SingletonBase<PanelStackManager> 
{
	public enum PopupName
	{
		None = -1,

		Test,

		Max
	}
		
	public List<GameObject> pupupPrefabList;

	private static float distanceBetweenPopup = 50.0f;
	private List<GameObject> popupStack = new List<GameObject>(20);
	private Dictionary<PopupName, List<GameObject>> usedPopups = new Dictionary<PopupName, List<GameObject>>();

	private void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}

	/// <summary>
	/// Push the specified popupObj.
	/// 
	/// Set local z position and hierarchy
	/// </summary>
	/// <param name="popupObj">Popup object.</param>
	public void Push(GameObject popupObj)
	{
		UIPanel popup = popupObj.GetComponent<UIPanel> ();
        popup.OnCompleteAction = ActionAtPopPanel;
		Transform popupTrans = popup.transform;

		Vector3 position = popupTrans.localPosition;
		position.z = -Count () * distanceBetweenPopup;
	
		popupTrans.SetParent (GetActivatedGroupTrans());
		popupTrans.localPosition = position;

		popupObj.SetActive (true);
        popup.StartOpenPopup();
        popupStack.Add (popupObj);
    }

    public void Push(PopupName name)
	{
		//1) find popup in usedPopups -> Init -> Push
		//2) if didnt find popop in usedPopups? instantiate popup in popupList -> Init -> Add Used Popups -> Push
		List<GameObject> popupObjs = null;
		if(usedPopups.TryGetValue(name, out popupObjs))
		{
			Debug.Log ("popupObjs.Count : " + popupObjs.Count);

			for(int i = 0; i < popupObjs.Count; i++)
			{
				if(!popupObjs[i].activeSelf)
				{
					Push (popupObjs [i]);
					return;
				}
			}
		}
			
		GameObject popupObj = GameObject.Instantiate<GameObject> (pupupPrefabList [(int)name], GetActivatedGroupTrans());

		if(popupObjs == null)
		{
			popupObjs = new List<GameObject> ();
			popupObjs.Add (popupObj);
			usedPopups.Add (name, popupObjs);
		}
		else
		{
			popupObjs.Add (popupObj);
		}

		Push (popupObj);
	}

	public void Pop()
	{
		if(Empty())
		{
			JDebugger.Log ("PanelStackManager Is Empty");
			return;
		}

		int lastIdx = popupStack.Count - 1;
        
        UIPanel popup = popupStack[lastIdx].GetComponent<UIPanel>();
        popup.StartClosePopup();

        popupStack.RemoveAt(lastIdx);
    }

	public void PopAll()
	{
		for(int i = 0; i < popupStack.Count; i++)
		{
			Pop ();
		}
	}

	public int Count()
	{
		Debug.Log (popupStack.Count);
		return popupStack.Count;
	}

	public bool Empty()
	{
		return popupStack.Count == 0;
	}

	public Transform GetActivatedGroupTrans()
	{
		Camera[] cameras = Camera.allCameras;

		for(int i = 0; i < Camera.allCameras.Length; i++)
		{
			if(cameras[i].CompareTag("UICamera"))
			{
				return cameras [i].transform.Find ("UI_Canvas").Find ("Activated");
			}
		}

		JDebugger.Log ("Can't find ActivatedGroupTrans");
		return null;
	}

	public Transform GetDeactivatedGroupTrans()
	{
		Camera[] cameras = Camera.allCameras;

		for(int i = 0; i < Camera.allCameras.Length; i++)
		{
			if(cameras[i].CompareTag("UICamera"))
			{
				return cameras [i].transform.Find ("UI_Canvas").Find ("Deactivated");
			}
		}

		JDebugger.Log ("Can't find DeactivatedGroupTrans");
		return null;
	}

	public void ActionAtPopPanel(UIPanel popup)
    {
        Transform popupTrans = popup.transform;
        Vector3 position = popupTrans.localPosition;
        position.z = 0;

        popupTrans.SetParent(GetDeactivatedGroupTrans());
        popupTrans.localPosition = position;
    }
}
