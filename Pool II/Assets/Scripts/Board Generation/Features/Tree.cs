using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Feature
{
    [HideInInspector]
    public Shaker shaker;
    public List<ParticleSystem> leaves = new List<ParticleSystem>();
    int shakeLeafCount = 1;
    bool blowing;

    Dictionary<ParticleSystem, ParticleSystem.MinMaxCurve> originalSpeedX =
    new Dictionary<ParticleSystem, ParticleSystem.MinMaxCurve>();
    Dictionary<ParticleSystem, ParticleSystem.MinMaxCurve> originalSpeedY =
new Dictionary<ParticleSystem, ParticleSystem.MinMaxCurve>();

    public override void PostSetup()
    {
        base.PostSetup();
        shaker = (Shaker)gameObject.GetAffecter("MainAffecter");
        leaves = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
        GetComponent<TagHandler>().tags.Add(Tag.Greenery);
        foreach (var particle in leaves)
        {
            ParticleSystem.VelocityOverLifetimeModule module = particle.velocityOverLifetime;
            originalSpeedX[particle] = module.x;
            originalSpeedY[particle] = module.x;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BaseBall>())
        {
            if (!shaker || shaker.shaking) return;
            shaker.Activate(0.2f);
            EmitLeaves(shakeLeafCount);
        }
    }

    public void EmitLeaves(int leafCount)
    {
        foreach (var leaf in leaves)
        {
            leaf.Emit(leafCount);
        }
    }

    public void EmitLeaves(int leafCount, float duration, ParticleSystem.MinMaxCurve blowSpeedX, ParticleSystem.MinMaxCurve blowSpeedY)
    {
        if (blowing) return;
        StartCoroutine(BlowLeaves(leafCount, duration, blowSpeedX, blowSpeedY));
    }

    IEnumerator BlowLeaves(int leafCount, float duration, ParticleSystem.MinMaxCurve blowSpeedX, ParticleSystem.MinMaxCurve blowSpeedY)
    {
        blowing = true;
        foreach (var particle in leaves)
        {
            ParticleSystem.VelocityOverLifetimeModule module = particle.velocityOverLifetime;
            module.x = blowSpeedX;
            module.orbitalX = blowSpeedY;
            particle.Emit(leafCount);
        }
        yield return new WaitForSeconds(duration);
        foreach (var particle in originalSpeedX.Keys)
        {
            ParticleSystem.VelocityOverLifetimeModule module = particle.velocityOverLifetime;
            module.x = originalSpeedX[particle];
            module.orbitalY = originalSpeedY[particle];
        }
        blowing = false;
    }
}
