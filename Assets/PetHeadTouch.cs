using UnityEngine;

public class PetHeadTouch : MonoBehaviour
{
    public int touchTime = 0;
    public ParticleSystem heartFX;
    ParticleSystem.EmissionModule em;
    private void Start()
    {
        if(heartFX == null)
        {
            heartFX = GetComponentInChildren<ParticleSystem>();
        }
        em = heartFX.emission;
        em.rateOverTime = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hand"))
        {
            touchTime = 0;
            em.rateOverTime = 0;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            touchTime = 0;
            em.rateOverTime = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            touchTime++;
            if(touchTime == 100)
            {
                em.rateOverTime = 5f;
            }

        }
    }
}
