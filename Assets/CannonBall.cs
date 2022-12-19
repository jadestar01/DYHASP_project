using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isLeft = false;
    public float cannonBallSpeed = 5.0f;

    void Start()
    {
    }

    private void Update()
    {
        if (isLeft)
            transform.Translate(Vector2.left * Time.deltaTime * cannonBallSpeed);
        else
            transform.Translate(Vector2.right * Time.deltaTime * cannonBallSpeed);
    }

    public void DestoryDelay(float time)
    {
        StartCoroutine(KillSelf(time));
    }

    IEnumerator KillSelf(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
