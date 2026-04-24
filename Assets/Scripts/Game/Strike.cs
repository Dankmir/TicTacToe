using UnityEngine;

public class Strike : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] Animator animator;

    private readonly int triggerHash = Animator.StringToHash("Strike");

    private void OnEnable()
    {
        if (animator)
            animator.SetTrigger(triggerHash);
    }

    // Attached to an animation event
    public void SpawnParticles()
    {
        if (particles)
            particles.Play();
    }
}
