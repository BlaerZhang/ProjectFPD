using System.Collections;
using System.Collections.Generic;
using Events;
using JoostenProductions;
using UnityEngine;
using DG.Tweening;

public class CameraZoom : OverridableMonoBehaviour
{
    [Range(1f, 1.5f)]
    [SerializeField] private float _FOVMultiplierOnFire = 1.05f;
    [SerializeField] private float _zoomDurationOnFire = 0.1f;
    [Space]
    [Range(1f, 1.5f)]
    [SerializeField] private float _FOVMultiplierOnRun = 1.1f;
    private Camera _camera;
    private float _originalFOV;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        WeaponEvents.OnFire += OnFire;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        WeaponEvents.OnFire -= OnFire;
    }

    void Start()
    {
        _camera = Camera.main;
        _originalFOV = _camera.fieldOfView;
    }

    public override void UpdateMe()
    {
        
    }

    private void OnFire()
    {
        // punch fov
        _camera.DOFieldOfView(_originalFOV * _FOVMultiplierOnFire, _zoomDurationOnFire).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            _camera.DOFieldOfView(_originalFOV, _zoomDurationOnFire);
        });
    }
}
