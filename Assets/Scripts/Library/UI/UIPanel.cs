using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour 
{
	public enum POPUP_TYPE
	{
		NONE = -1,
		FULLSCREEN,
		POPUP,
		SYSTEM,
	}

	public POPUP_TYPE _curPopupType = POPUP_TYPE.POPUP;
	public JTween.EaseType _curPopupStyle = JTween.EaseType.easeInBack;
	public float _popupTransitionTime = 0.5f;

	[Header("[Additional Panel Options]")]
	public UITabButton[] _tabButtons;

    [HideInInspector] public System.Action<UIPanel> OnCompleteAction;
   
    public void StartOpenPopup()
	{
        this.transform.localScale = Vector3.zero;

        JTween.ScaleTo(this.gameObject, new Vector3(1, 1, 1), _popupTransitionTime, 0, _curPopupStyle);
    }

    public void StartClosePopup()
    {
        this.transform.localScale = this.transform.localScale;

        JTween.ScaleTo(this.gameObject, JTween.Hash("scale", Vector3.zero, "time", _popupTransitionTime, "easetype", _curPopupStyle, "oncomplete", "ClosePopup"));
    }

    public void ClosePopup()
    {
        if (OnCompleteAction != null)
        {
            OnCompleteAction(this);
        }

        this.gameObject.SetActive(false);
    }

	public void SetActiveTabButton(int idx)
	{
		if(idx >= _tabButtons.Length)
		{
			JDebugger.Log ("tab button idx is out of range");
			return;
		}
		
		for(int i = 0; i < _tabButtons.Length; i++)
		{
			if(i == idx)
			{
				_tabButtons[i].Activate (true);
			}
			else
			{
				_tabButtons [i].Activate (false);
			}
		}
	}
}
