using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public ObjectPoolManager objectPoolManager;
    public float movingSpeed;

    void Update()
    {
        var dx = Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x;
        var dy = Input.mousePosition.y - Camera.main.WorldToScreenPoint(transform.position).y;
        var strawRadians = Mathf.Atan2(dy, dx);
        var strawDigrees = 360.0f * strawRadians / (2.0f * Mathf.PI);
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, -strawDigrees + 90,
            transform.rotation.z);

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.localPosition = Vector3.right * movingSpeed * Time.deltaTime;
        //    //transform.Translate(Vector3.right * movingSpeed * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.localPosition = Vector3.left * movingSpeed * Time.deltaTime;
        //    //transform.Translate(Vector3.left * movingSpeed * Time.deltaTime);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            ShootArrow();
        }
    }

    void ShootArrow()
    {
        // Get an arrow from the object pool
        GameObject arrow = objectPoolManager.GetArrowFromPool(transform.position, transform.rotation);

        // Apply additional arrow behavior or force if needed
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        arrowRb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }
}
