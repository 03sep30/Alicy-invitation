using UnityEngine;

public class ParticelAutoDestroyer : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if ( particle.isPlaying == false )
        {
            Destroy(gameObject);
        }
    }
}
