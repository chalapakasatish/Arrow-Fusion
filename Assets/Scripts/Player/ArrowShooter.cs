using System;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public ObjectPoolManager objectPoolManager;
    public float movingSpeed;
    public float timeGapForShoot;
    public bool isShooting;
    public Transform firstArrowPosition, secondArrowPosition;
    
    public enum PowerUpsEnum
    {
        SINGLEARROW,
        DOUBLEARROW,
        FIREARROW
    }
    public PowerUpsEnum powerUpsEnum;
    public event Action<PowerUpsEnum> powerUpAction;
    private void Awake()
    {
        powerUpsEnum = PowerUpsEnum.SINGLEARROW;//declaration enum
        powerUpAction += SwitchPowerups;// adding method to action
        powerUpAction?.Invoke(powerUpsEnum);//calling using action parameters
    }

    public void SwitchPowerups(PowerUpsEnum powerUpsEnum)
    {
        switch (powerUpsEnum)
        {
            case PowerUpsEnum.SINGLEARROW:
                ShootArrow(1);
                break;
            case PowerUpsEnum.DOUBLEARROW:
                ShootArrow(2);
                break;
            case PowerUpsEnum.FIREARROW:
                FireArrow();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PowerupTrigger powerupTrigger))
        {
            powerUpsEnum = (PowerUpsEnum)UnityEngine.Random.Range(0, 3);
            powerUpAction?.Invoke(powerUpsEnum);
        }
    }

    public void FireArrow()
    {
        // Get an arrow from the object pool
        GameObject arrow = objectPoolManager.GetArrowFromPoolFireArrow(firstArrowPosition.transform.position, Quaternion.identity);
        // Apply additional arrow behavior or force if needed
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        arrowRb.AddForce(transform.forward * movingSpeed, ForceMode.Impulse);
    }
    private void OnDestroy()
    {
        powerUpAction -= SwitchPowerups;
    }
    void Update()
    {
        #region Delete
        //var dx = Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x;
        //var dy = Input.mousePosition.y - Camera.main.WorldToScreenPoint(transform.position).y;
        //var strawRadians = Mathf.Atan2(dy, dx);
        //var strawDigrees = 360.0f * strawRadians / (2.0f * Mathf.PI);
        //transform.localRotation = Quaternion.Euler(transform.localRotation.x, -strawDigrees + 90,
        //    transform.rotation.z);
        #endregion

        if (timeGapForShoot <= 0 && isShooting)
        {
            powerUpAction?.Invoke(powerUpsEnum);
            timeGapForShoot = 1;
        }
        else
        {
            timeGapForShoot -= Time.deltaTime;
        }
    }

    void ShootArrow(int num)
    {
        switch (num)
        {
            case 1:
                // Get an arrow from the object pool
                GameObject arrow = objectPoolManager.GetArrowFromPool(firstArrowPosition.transform.position, Quaternion.identity);
                // Apply additional arrow behavior or force if needed
                Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
                arrowRb.AddForce(transform.forward * movingSpeed, ForceMode.Impulse);
                break;
            case 2:
                GameObject arrow1 = objectPoolManager.GetArrowFromPool(firstArrowPosition.transform.position, Quaternion.identity);
                // Apply additional arrow behavior or force if needed
                Rigidbody arrowRb1 = arrow1.GetComponent<Rigidbody>();
                arrowRb1.AddForce(transform.forward * movingSpeed, ForceMode.Impulse);
                GameObject arrow2 = objectPoolManager.GetArrowFromPool(secondArrowPosition.transform.position, Quaternion.identity);
                Rigidbody arrowRb2 = arrow2.GetComponent<Rigidbody>();
                arrowRb2.AddForce(transform.forward * movingSpeed, ForceMode.Impulse);
                break;
        }
    }
    
}
