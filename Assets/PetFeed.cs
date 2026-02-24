using UnityEngine;

public class PetFeed : MonoBehaviour
{

    public ParticleSystem heartFX;
    ParticleSystem.EmissionModule em;
    public int touchTime = 0;
    Animator anim;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        if (heartFX == null)
        {
            heartFX = GetComponentInChildren<ParticleSystem>();
        }
        em = heartFX.emission;
        em.rateOverTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.TryGetComponent<CatStick>(out var catStick))
        {
            touchTime = 0;
            em.rateOverTime = 0f;
            anim.SetBool("isFeed", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<CatStick>(out var catStick))
        {
            touchTime++;
            if (touchTime == 30)
            {
                em.rateOverTime = 5f;
            }
            catStick.Damage(0.1f);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CatStick>(out var catStick))
        {
            touchTime = 0;
            em.rateOverTime = 0f;
            anim.SetBool("isFeed", false);

        }
    }
}
