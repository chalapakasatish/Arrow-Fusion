using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// Coords of next target;
    /// </summary>
    public Vector3 NextNode;

    /// <summary>
    /// ID of ball. -1 if bonus.
    /// </summary>
    public int BallId { get; private set; }

    /// <summary>
    /// Path controller.
    /// </summary>
    public BezierPathController PathController;

    public bool MustBeDestroyed = true;
    public int count = 1;

    public void SetBallId(int id)
    {
        BallId = id;
        Debug.Log(BallId);
        GetComponent<MeshRenderer>().material = (id == -1) ? GameObject.FindGameObjectWithTag("BallsFactory").GetComponent<BallsFactory>().BonusMaterial : GameObject.FindGameObjectWithTag("BallsFactory").GetComponent<BallsFactory>().AvailableMaterials[id];
    }

    private IEnumerator SelfDestroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (MustBeDestroyed) Destroy(gameObject);
    }

    /// <summary>
    /// Interaction between objects.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow")
        {
            ObjectPoolManager.Instance.ReturnArrowToPool(other.gameObject);
            other.transform.position = Vector3.zero;
            other.transform.rotation = Quaternion.identity;
            count--;
            if(count <= 0)
            {
                //PathController.Correction();
                //PathController.DestroyBall(this);
                if (PathController.BallSequence.Contains(this))
                {
                    //PathController.StopSequence();
                    PathController.StartCoroutine(PathController.DelayedCheckEqualBalls(this,0f));
                }
                if (count <= 0)
                {
                    PathController.DestroyBall(this);
                }
                    
            }
        }

        GetComponent<AnimationPlayer>().Play(AnimationThrowType.OnCollide);
        if (PathController.BallSequence.Contains(this)) return;
        if (other.gameObject.tag == "BallsTrap") return;
        
        StopAllCoroutines();
        //if (other.gameObject.tag == "Ball")
        //{
        //    PathController.InsertBallInSequence(this, other.GetComponent<Ball>());
        //    MustBeDestroyed = false;
        //}
    }

    private IEnumerator ShootCoroutine(Vector3 target, float time)
    {
        var passedTime = 0f;
        var startPosition = transform.position;
        while (passedTime < time)
        {
            passedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, target, passedTime / time);
            yield return new WaitForEndOfFrame();
        }
    }

    public void Shoot(Vector3 target, float time)
    {
        StartCoroutine(ShootCoroutine(target, time));
        StartCoroutine(SelfDestroyCoroutine(time));
    }

}
