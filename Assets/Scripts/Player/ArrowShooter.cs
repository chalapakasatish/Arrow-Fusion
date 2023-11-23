using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public ObjectPoolManager objectPoolManager;
    public float movingSpeed;
    public float timeGapForShoot;

    void Update()
    {
        //var dx = Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x;
        //var dy = Input.mousePosition.y - Camera.main.WorldToScreenPoint(transform.position).y;
        //var strawRadians = Mathf.Atan2(dy, dx);
        //var strawDigrees = 360.0f * strawRadians / (2.0f * Mathf.PI);
        //transform.localRotation = Quaternion.Euler(transform.localRotation.x, -strawDigrees + 90,
        //    transform.rotation.z);

        if (Input.GetMouseButtonDown(0) && timeGapForShoot <= 0)
        {
            ShootArrow();
            timeGapForShoot = 2;
        }
        else
        {
            timeGapForShoot -= Time.deltaTime;
        }
    }

    void ShootArrow()
    {
        // Get an arrow from the object pool
        GameObject arrow = objectPoolManager.GetArrowFromPool(transform.position, transform.rotation);

        // Apply additional arrow behavior or force if needed
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        arrowRb.AddForce(transform.forward * movingSpeed, ForceMode.Impulse);
    }
}
