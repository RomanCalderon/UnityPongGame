using UnityEngine;

public class PlayerControls : MonoBehaviour {
    
    
    public int playerID;
    public float aiDifficulty = 1;
    string controller;

    [SerializeField]
    float boundary = 5;
    
    float speed = 10;

    Rigidbody2D rb;
    Vector2 moveVector;
    Rigidbody2D ball;
    Vector2 ballVelocity;
    float yIntercept;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = playerID.ToString();
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        MoveManager();
    }

    void MoveManager()
    {
        if(playerID == 1 || playerID == 2)
        {
            // User Player
            moveVector += new Vector2(0, Input.GetAxis("Vertical" + controller) * Time.deltaTime * speed);
            moveVector = Vector2.ClampMagnitude(moveVector, boundary);
            rb.MovePosition(moveVector);
        }
        else
        {
            // AI Player
            // Move vertically in an attempt to hit the ball
            Vector2 interception = new Vector2(transform.position.x, ball.transform.position.y);

            //ballVelocity = ball.velocity;

            //if(ballVelocity.x != 0)
            //    yIntercept = (ball.position.y / (ballVelocity.y / ballVelocity.x) * ball.position.x);

            //Vector2 interception = new Vector2(transform.position.x, yIntercept);

            transform.position = Vector2.MoveTowards(transform.position, interception, Time.deltaTime * aiDifficulty);
            
        }
    }

    void OnDrawGizmo()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(transform.position.x, yIntercept), 1);
    }
}
