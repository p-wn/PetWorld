using Unity.VisualScripting;
using UnityEngine;

public class CatStick : MonoBehaviour
{

    public float hp = 100;
    public void Damage(float _dmg)
    {
        hp -= _dmg;
        if(hp < 0)
        {
            Destroy(gameObject);
        }
    }
}
