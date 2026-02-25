using Unity.Burst.CompilerServices;
using UnityEngine;

public class Treasure : MonoBehaviour
{

    public ParticleSystem hintFx;
    ParticleSystem.EmissionModule em;
    public GameObject findFx;
    public GameObject treasure;


    private void Awake()
    {
        em = hintFx.emission;
        treasure.SetActive(false);
        findFx.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            em.rateOverTime = 0;
            treasure.SetActive(true);
            findFx.SetActive(true);
        }
    }
}
