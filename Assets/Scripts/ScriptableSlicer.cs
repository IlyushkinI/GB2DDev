using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameProperties/SliceParametres", fileName ="New SliceParametres" )]
public class ScriptableSlicer : ScriptableObject
{
    [SerializeField] private AnimationCurve _curve = new AnimationCurve();
    [SerializeField] private float _time;


    public AnimationCurve animationCurve { get => _curve; }
    public float time { get => _time;}
}
