using UnityEngine;

public class OpenTrigger : MonoBehaviour
{
    public Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(anim != null && other.CompareTag("Player"))
        {
            anim.SetBool("isOpen", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (anim != null && other.CompareTag("Player"))
        {
            anim.SetBool("isOpen", false);

        }
    }
}
