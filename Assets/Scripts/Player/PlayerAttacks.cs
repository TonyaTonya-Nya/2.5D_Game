using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject fireBall;
    public Transform fireBallPoint;
    public float fireBallSpeed = 600;
    public Transform model;

    public void FireBallAttack()
    {

        GameObject ball = Instantiate(fireBall, fireBallPoint.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(-fireBallPoint.right * fireBallSpeed);
    }

    public void BossAttack()
    {
        float x = Random.Range(-100, 100) / 100f;
        float z = Random.Range(-100, 100) / 100f;
        Vector3 v = new Vector3(x, 0, z);
        GameObject ball = Instantiate(fireBall, fireBallPoint.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(-v * fireBallSpeed);
    }

}
