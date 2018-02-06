using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 이 클래스는 iTween 을 상속받은 클래스로, 편의성을 조금 더 개선하려는 목적으로 만든 클래스이다.
 * 
 */ 

public class JTween : iTween
{
    private JTween(Hashtable h)
        : base(h)
    {
    }

    public static void ScaleTo(GameObject target, Vector3 scale, float time, float delay = 0, EaseType easeType = EaseType.linear)
    {
        iTween.ScaleTo(target, iTween.Hash("scale", scale, "time", time, "delay", delay, "easetype", easeType));
    }
}
