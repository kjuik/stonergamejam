using UnityEngine;
using Unity.Entities;

public class Bouncer : MonoBehaviour
{
    public float Strength;
    public Transform[] Anchors;
    public float[] initialDistances;

    public float Wear;
    public Vector3 Velocity { get; private set; }

    public void UpdateVelocity(float deltaTime)
    {
        Velocity *= (1 - Wear * deltaTime);

        for (var i=0; i<Anchors.Length; ++i)
        {
            var vecToAnchor = Anchors[i].position - transform.position;
            var imbalance = initialDistances[i] - Vector3.Magnitude(vecToAnchor);
            var force = Strength * imbalance;

            Velocity += force * deltaTime * vecToAnchor.normalized * -1f;
        }
    }
}

public class BouncerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;

        Entities.ForEach((Bouncer b, Transform t) =>
        {
            b.UpdateVelocity(deltaTime);
            t.position += b.Velocity * deltaTime;
        });
    }
}