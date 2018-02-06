using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIButton : Selectable, IPointerClickHandler
{
    public UnityEvent _onButtonClick;

    [Header("[Custom Button Settings]")]
    public JTween.EaseType _buttonStyle = iTween.EaseType.easeInOutBack;
    public float _pressedScale = 0.8f;
    
    protected Image _image;
    protected Vector3 _localScale;
	protected RectTransform _rectTransform;
	protected System.Action<GameObject> _onButtonClickAction;

    protected override void Awake()
    {
        base.Awake();

        _image = this.GetComponent<Image>();
		_rectTransform = this.GetComponent<RectTransform> ();
    }

    protected override void Start()
    {
        base.Start();

        _localScale = this.transform.localScale;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        JTween.ScaleTo(this.gameObject, _localScale * _pressedScale, GetTransitionTime(), 0, _buttonStyle);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        JTween.ScaleTo(this.gameObject, _localScale, GetTransitionTime(), 0, _buttonStyle);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if(_onButtonClick != null)
        {
            _onButtonClick.Invoke();
        }

		if(_onButtonClickAction != null)
		{
			_onButtonClickAction (this.gameObject);
		}
    }

    public float GetTransitionTime()
    {
        return this.colors.fadeDuration;
    }

	public void SubscribeButton(System.Action<GameObject> action)
	{
		if(action == null)
		{
			return;
		}

		_onButtonClickAction += action;
	}

	public void UnsubscribeButton(System.Action<GameObject> action)
	{
		if(action == null)
		{
			return;
		}

		_onButtonClickAction -= action;
	}
}
