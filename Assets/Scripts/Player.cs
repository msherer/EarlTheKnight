using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = true;

    public float speed = 1f;
    public float maxSpeed = 5f;
    public float moveForce = 365f;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

	private void Update()
	{
        //
	}

	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, rb2d.velocity.y);

        if (moveHorizontal * rb2d.velocity.x < maxSpeed) {
            rb2d.AddForce(Vector2.right * moveHorizontal * moveForce);
        }

        if (moveHorizontal * rb2d.velocity.x < maxSpeed) {
            rb2d.AddForce(Vector2.right * moveHorizontal * moveForce);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed) {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }

        if (Input.GetKeyUp("right")) {
            rb2d.velocity = new Vector2(0, 0);
        }

        if (Input.GetKeyUp("left"))
        {
            rb2d.velocity = new Vector2(0, 0);
        }

        if (moveHorizontal > 0 && !facingRight) {
            Flip();
        } else if (moveHorizontal < 0 && facingRight) {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
