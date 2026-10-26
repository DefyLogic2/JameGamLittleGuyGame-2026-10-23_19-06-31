using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isParryable = false;
    public bool MovingObject = false;
    public float direction = -1f;
    public float speed = 5f;
    public bool DieAfterTime = true;
    public float AliveTime = 5f;
    public float parryForceX = 10f;
    public float parryForceY = 5f;
    public bool DoDedSpin = false;
    public Rigidbody2D rb2D;
    void Start()
    {
        if(MovingObject)
        {
            Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DieAfterTime)
        {
            AliveTime -= Time.deltaTime;
            if (AliveTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Parry()
    {
        //rb2D.linearVelocity = Vector2.zero;
        rb2D.AddForce(Vector2.right * parryForceX + Vector2.up * parryForceY, ForceMode2D.Impulse);

    }
}
