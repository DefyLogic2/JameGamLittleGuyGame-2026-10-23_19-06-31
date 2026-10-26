using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer spriteRenderer;
    public float Scale;
    public float Angle;
    public float wobbleTimer;
    public float wobbleGoal;
    public float AngleRange = 15f;
    public float randRangeMin = 0.1f;
    public float randRangeMax = 0.3f;
    public Animator animator;
    public float rotateAmount = 1080f;
    public bool dedRotate = false;
    public float dedRotateTimer = 3f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, Angle);
        wobbleTimer += Time.deltaTime;
        if(dedRotate)
        {
            dedRotateTimer -= Time.deltaTime;
            Angle += rotateAmount * Time.deltaTime;
            if(dedRotateTimer <= 0)
            {
                dedRotateTimer = 3f;
                dedRotate = false;
                Angle = 0;
            }
        }

    }
    public void Wobble()
    {
       if (wobbleTimer >= wobbleGoal)
        {
            wobbleTimer = 0;
            wobbleGoal = Random.Range(randRangeMin, randRangeMax);
            Angle = Random.Range(-AngleRange, AngleRange);
        }
    }
    public void DieAnim()
    {
     
        dedRotate = true;
    }
    public void Stabilize()
    {
        if (!dedRotate) { Angle = 0; }
       
    }
    public void IdleAnim()
    {
        animator.SetTrigger("Idle");
        animator.ResetTrigger("SpinHat");
        animator.ResetTrigger("DontSpinHat");
    }
    public void DoubleJumpAnim()
    {
        animator.SetTrigger("SpinHat");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("DontSpinHat");
    }
    public void EndDoubleJumpAnim()
    {
        animator.SetTrigger("DontSpinHat");
        animator.ResetTrigger("Idle");
        
        animator.ResetTrigger("SpinHat");
    }
}
