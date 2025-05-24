using System.Collections;
using System.Collections.Generic;
using JoostenProductions;
using UnityEngine;

public class CameraSpring : OverridableMonoBehaviour
{
    [Min(0.01f)]
    [SerializeField] private float _halfLife = 0.075f;
    [Space]
    [SerializeField] private float _frequency = 18f;
    [Space]
    [SerializeField] private float _angularDisplacement = 2f;
    [SerializeField] private float _linearDisplacement = 0.05f;

    private Vector3 _springPosition;
    private Vector3 _springVelocity;
    void Start()
    {
        _springPosition = transform.position;
        _springVelocity = Vector3.zero;
    }

    public override void UpdateMe()
    {
        transform.localPosition = Vector3.zero;

        Spring(ref _springPosition, ref _springVelocity, transform.position, _halfLife, _frequency, Time.deltaTime);
        
        var localSpringPosition = _springPosition - transform.position;
        var springHeight = Vector3.Dot(localSpringPosition, transform.up);

        transform.localEulerAngles = new Vector3(-springHeight * _angularDisplacement, 0f, 0f);
        transform.localPosition = localSpringPosition * _linearDisplacement;
    }

    private static void Spring(ref Vector3 current, ref Vector3 velocity, Vector3 target, float halfLife, float frequency, float timeStep)
    {
        var dampingRatio = -Mathf.Log(0.5f) / (frequency * halfLife);
        var f = 1f + 2f * timeStep * dampingRatio * frequency;
        var oo = frequency * frequency;
        var hoo = timeStep * oo;
        var hhoo = timeStep * hoo;
        var detInv = 1f / (f + hhoo);
        var detX = f * current + timeStep * velocity + hhoo * target;
        var detV = velocity + hoo * (target - current);
        current = detX * detInv;
        velocity = detV * detInv;
    }
}
