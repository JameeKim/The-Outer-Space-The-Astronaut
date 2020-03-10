using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    public float maxSpeed = 10.0f;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector2 directionRaw = new Vector2(horiz, vert);
        Vector2 velocity = directionRaw.magnitude * maxSpeed * directionRaw.normalized;

        rigidBody.velocity = velocity;
    }
}
