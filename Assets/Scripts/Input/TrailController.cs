using JoostenProductions;
using UnityEngine;


public sealed class TrailController : BaseController
{

    #region Fields

    private readonly int _framesForStartTrail = 2;
    private int _currentFrame;

    private readonly ResourcePath _trailView = new ResourcePath { PathResource = "Prefabs/Trail" };
    private readonly TrailRenderer _trail;

    private DelegateForUpdate _delegateUpdate;

    #endregion


    #region CodeLifeCycles

    public TrailController(Transform transformUI)
    {
        _trail = GameObject.Instantiate(ResourceLoader.LoadPrefab(_trailView), transformUI, false).GetComponent<TrailRenderer>();
        AddGameObjects(_trail.gameObject);
        _trail.emitting = false;

        if (Input.mousePresent)
        {
            _delegateUpdate = MouseTouch;
        }
        else
        {
            _delegateUpdate = FingerTouch;
        }

        UpdateManager.SubscribeToUpdate(DoItInUpdate);
    }

    #endregion


    #region Methods

    delegate void DelegateForUpdate();

    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(DoItInUpdate);
    }

    private void FingerTouch()
    {
        DrawTrail(Input.touchCount > 0, _trail, Input.GetTouch(0).position, ref _currentFrame, _framesForStartTrail);
    }

    private void MouseTouch()
    {
        DrawTrail(Input.GetMouseButton(0), _trail, CorrectPosition(Input.mousePosition), ref _currentFrame, _framesForStartTrail);
    }

    private void DrawTrail(bool needDraw, TrailRenderer trail, Vector3 drawPosition, ref int currentFrame, int framesForStartTrail)
    {
        if (needDraw)
        {
            currentFrame++;
            trail.transform.position = drawPosition;
            if (!trail.emitting && currentFrame == framesForStartTrail)
            {
                trail.emitting = true;
            }
        }
        else
        {
            if (trail.emitting)
            {
                trail.emitting = false;
                currentFrame = 0;
            }
        }
    }

    private Vector3 CorrectPosition(Vector3 position)
    {
        return new Vector3(position.x - 552.5f, position.y - 294.5f, 0.0f) / 50.0f;
    }

    private void DoItInUpdate()
    {
        _delegateUpdate();
    }

    #endregion

}
