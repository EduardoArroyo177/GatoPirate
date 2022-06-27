using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy;

    private void OnEnable()
    {
        Invoke(nameof(DestroyParticle), timeToDestroy);
    }

    private void DestroyParticle()
    {
        gameObject.SetActive(false);
    }
}
