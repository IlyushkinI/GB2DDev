using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
public class MoveBox : MonoBehaviour
{

    [SerializeField] private float duration = 4f;
    [SerializeField] private Transform endPositionTransform;
    [SerializeField] private List<Vector3> path;
    [SerializeField] private AnimationCurve curve;
    private Vector3 endPosition;

    private Material material;

    void Start()
    {


        transform.DOLocalRotate(new Vector3(1, 1, 0), 5f);
        //GameObject spher = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //spher.transform.position = transform.position;
        //Material materialSphere = spher.GetComponent<Renderer>().material;

        //var sequence =  DOTween.Sequence();
        

        //foreach (var item in path)
        //{
        //    var tween = spher.transform.DOMove(item, duration);
        //    sequence.Append(tween);
        //    var colorTween = materialSphere.DOColor(Color.red, 2f).SetEase(curve);
        //    sequence.Join(colorTween);
        //    colorTween = materialSphere.DOColor(Color.white, 2f);
        //    sequence.Append(colorTween);
        //    sequence.AppendCallback(() => Debug.Log($"Step {item.ToString()} end."));


        //    //var tween = transform.DOMove(item, duration);
        //    //sequence.Append(tween);
        //}

        //sequence.Play();


    }
}
