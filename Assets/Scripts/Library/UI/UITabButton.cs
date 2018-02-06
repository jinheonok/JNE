using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITabButton : UIButton
{
    [Header("[Custom Tab Button Settings]")]
    public string _spriteOn = null;
    public string _spriteOff = null;
    public Vector2 _sizeOn = new Vector2(1, 1);
    public Vector2 _sizeOff = new Vector2(1, 1);

    protected override void Start()
    {
        base.Start();

        if (string.IsNullOrEmpty(_spriteOn))
        {
            _spriteOn = _image.sprite.name;
        }
        if (string.IsNullOrEmpty(_spriteOff))
        {
            _spriteOff = _image.sprite.name;
        }
			
        if (_sizeOn == new Vector2(0, 0))
        {
			_sizeOn = _rectTransform.sizeDelta;
        }

        if (_sizeOff == new Vector2(0, 0))
        {
			_sizeOff = _rectTransform.sizeDelta;
        }
    }

    public void Activate(bool flag)
    {
        interactable = flag;

		Vector2 sizeDelta = this.GetComponent<RectTransform> ().sizeDelta;

        if (flag)
        {
            _image.sprite = SpriteResources.Instance.GetSprite(_spriteOn);
			JTween.ValueTo(this.gameObject, JTween.Hash("from", sizeDelta.x, "to", _sizeOn.x, "time", GetTransitionTime(), "easeType", _buttonStyle, "onupdate", "UpdatedWidth"));
			JTween.ValueTo(this.gameObject, JTween.Hash("from", sizeDelta.y, "to", _sizeOn.y, "time", GetTransitionTime(), "easeType", _buttonStyle, "onupdate", "UpdatedHeight"));
        }
        else
        {
            _image.sprite = SpriteResources.Instance.GetSprite(_spriteOff);
			JTween.ValueTo (this.gameObject, JTween.Hash ("from", sizeDelta.x, "to", _sizeOff.x, "time", GetTransitionTime (), "easeType", _buttonStyle, "onupdate", "UpdatedWidth"));
			JTween.ValueTo (this.gameObject, JTween.Hash ("from", sizeDelta.y, "to", _sizeOff.y, "time", GetTransitionTime (), "easeType", _buttonStyle, "onupdate", "UpdatedHeight"));
        }
    }

	public void UpdatedWidth(float width)
	{
		Vector2 sizeDelta = _rectTransform.sizeDelta;
		sizeDelta.x = width;
		_rectTransform.sizeDelta = sizeDelta;
	}

	public void UpdatedHeight(float height)
	{
		Vector2 sizeDelta = _rectTransform.sizeDelta;
		sizeDelta.y = height;
		_rectTransform.sizeDelta = sizeDelta;
	}

//    public override void OnPointerClick(PointerEventData eventData)
//    {
//        base.OnPointerClick(eventData);
//
//		Activate(!interactable);
//    }
}
