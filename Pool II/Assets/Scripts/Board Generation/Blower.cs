using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    bool blowing;
    [HideInInspector]
    public List<ParticleSystem> particles = new List<ParticleSystem>();
    Dictionary<ParticleSystem, ParticleSystem.MinMaxCurve> originalSpeedX =
    new Dictionary<ParticleSystem, ParticleSystem.MinMaxCurve>();
    Dictionary<ParticleSystem, ParticleSystem.MinMaxCurve> originalSpeedY =
new Dictionary<ParticleSystem, ParticleSystem.MinMaxCurve>();

    // Start is called before the first frame update
    void Awake()
    {
        particles = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
        foreach (var particle in particles)
        {
            ParticleSystem.VelocityOverLifetimeModule module = particle.velocityOverLifetime;
            originalSpeedX[particle] = module.x;
            originalSpeedY[particle] = module.y;
        }
    }

    public void Blow(int particleCount, float duration, ParticleSystem.MinMaxCurve blowSpeedX, ParticleSystem.MinMaxCurve blowSpeedY)
    {
        if (blowing) return;
        StartCoroutine(BlowCo(particleCount, duration, blowSpeedX, blowSpeedY));
    }

    IEnumerator BlowCo(int particleCount, float duration, ParticleSystem.MinMaxCurve blowSpeedX, ParticleSystem.MinMaxCurve blowSpeedY)
    {
        blowing = true;
        foreach (var particle in particles)
        {
            ParticleSystem.VelocityOverLifetimeModule module = particle.velocityOverLifetime;
            module.x = blowSpeedX;
            //module.orbitalZ = blowSpeedY;
            particle.Emit(particleCount);
        }
        yield return new WaitForSeconds(duration);
        foreach (var particle in originalSpeedX.Keys)
        {
            ParticleSystem.VelocityOverLifetimeModule module = particle.velocityOverLifetime;
            module.x = originalSpeedX[particle];
            //module.orbitalZ = originalSpeedY[particle];
        }
        blowing = false;
    }
}
