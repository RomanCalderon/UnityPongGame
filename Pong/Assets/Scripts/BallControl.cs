using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BallControl : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField]
    float maxVelocity = 18;
    [SerializeField]
    float startForce = 80;

    [SerializeField]
    float randPitch;

    [SerializeField]
    AudioClip impactSound;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Invoke("LaunchBall", 1);
	}

    void LaunchBall()
    {
        float randomNumber = Random.Range(0.0f, 1.0f);
        float pitch = Random.Range(-randPitch, randPitch);
        if (randomNumber <= 0.5)
        {
            rb.AddForce(new Vector2(startForce, pitch));
        }
        else
        {
            rb.AddForce(new Vector2(-startForce, pitch));
        }
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        Invoke("LaunchBall", 1);
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

        if ((rb.velocity.y / rb.velocity.x) > 10)
            rb.AddForce(new Vector2((Random.Range(-10, 10)), 0));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider)
        {
            audioSource.PlayOneShot(impactSound);
        }

        if(col.collider.tag == "Player")
        {
            float maxDir = 50.0f;
            float velY = rb.velocity.y / 2 + Random.Range(-maxDir, maxDir);

            rb.AddForce(new Vector2(0, velY));

            if (rb.velocity.magnitude < maxVelocity)
                rb.AddForce(new Vector2((Mathf.Sign(rb.velocity.x) > 0)?startForce:-startForce, 0));
        }
    }
}
