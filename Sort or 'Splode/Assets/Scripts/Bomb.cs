using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Rigidbody2D RigidBody;

    private float _speed;
    private bool _dragging;
    private Vector3 _offset;

    void Start ()
    {
        _speed = 1;
        RigidBody.velocity = new Vector2(_speed, _speed);
    }

	void Update ()
    {
	}

    private void OnMouseDown()
    {
        _dragging = true;
        _offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    private void OnMouseUp()
    {
        _dragging = false;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        RigidBody.MovePosition(Camera.main.ScreenToWorldPoint(newPosition) + _offset);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        // When the bomb hits a wall
        if (!_dragging)
        {
            var normal = collision.contacts[0].normal;
            RigidBody.velocity = Vector3.Reflect(RigidBody.velocity, normal);
        }
    }
}
