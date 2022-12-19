using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public bool isLeft = false;
    public GameObject cannonBall;
    Transform shotPosition;
    public float shotDelay = 3.0f;
    public float destroyDelay = 5.0f;
    public float cannonBallSpeed = 5.0f;

    private void Start()
    {
        if (isLeft)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        shotPosition = gameObject.transform.GetChild(0).GetComponent<Transform>();
        StartCoroutine(ShotCannonBall());   
    }

    IEnumerator ShotCannonBall()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotDelay);
            GameObject cannonball = Instantiate(cannonBall, shotPosition.position, Quaternion.identity);
            cannonball.GetComponent<CannonBall>().isLeft = isLeft;
            cannonball.GetComponent<CannonBall>().cannonBallSpeed = cannonBallSpeed;

            if (isLeft)
                cannonball.GetComponent<SpriteRenderer>().flipX = true;
            else
                cannonball.GetComponent<SpriteRenderer>().flipX = false;


            cannonball.GetComponent<CannonBall>().DestoryDelay(destroyDelay);
        }
    }
}
