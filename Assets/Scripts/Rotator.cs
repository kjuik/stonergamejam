using UnityEngine;
using Unity.Entities;

public class Rotator : MonoBehaviour
{
    public float Speed;
}

public class RotatorSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;

        Entities.ForEach((Rotator r, Transform t) =>
            t.Rotate(0f, r.Speed * deltaTime, 0f));
    }
}