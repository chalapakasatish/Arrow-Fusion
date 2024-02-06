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
        DOUBLEARROW
    }
    public PowerUpsEnum powerUpsEnum;
    public event Action powerUpAction;
    private void Start()
    {
        powerUpsEnum = PowerUpsEnum.SINGLEARROW;
        powerUpAction?.Invoke();
        switch (powerUpsEnum) 
        { 
            case PowerUpsEnum.SINGLEARROW:
                powerUpAction = SingleArrow;
                break; 

            case PowerUpsEnum.DOUBLEARROW:
                powerUpAction = DoubleArrow;
                break;
        }
    }

    public void SingleArrow()
    {
        ShootArrow(1);
    }
    public void DoubleArrow()
    {
        ShootArrow(2);
    }
    private void OnDestroy()
    {
        powerUpAction -= SingleArrow;
        powerUpAction -= DoubleArrow;
    }
    void Update()
    {
        //var dx = Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x;
        //var dy = Input.mousePosition.y - Camera.main.WorldToScreenPoint(transform.position).y;
        //var strawRadians = Mathf.Atan2(dy, dx);
        //var strawDigrees = 360.0f * strawRadians / (2.0f * Mathf.PI);
        //transform.localRotation = Quaternion.Euler(transform.localRotation.x, -strawDigrees + 90,
        //    transform.rotation.z);

        if (timeGapForShoot <= 0 && isShooting)
        {
            powerUpAction?.Invoke();
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
