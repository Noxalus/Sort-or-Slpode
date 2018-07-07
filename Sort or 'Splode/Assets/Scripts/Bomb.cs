using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody2D RigidBody;

    private float _speed;

	void Start ()
    {
        _speed = 5;
        RigidBody.velocity = new Vector2(_speed, _speed);
    }

	void Update ()
    {
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var normal = collision.contacts[0].normal;

        RigidBody.velocity = Vector3.Reflect(RigidBody.velocity, normal);
    }
}
