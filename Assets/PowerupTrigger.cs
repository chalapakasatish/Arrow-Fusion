using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ArrowShooter shooter))
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Invoke("DestroyPowerup", 3f);
    }
    public void DestroyPowerup()
    {
        Destroy(gameObject);
    }
}
