using UnityEngine;

public enum BombType
{
    Black,
    Red
}

public class Bomb : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public ParticleSystem ParticleEffect;
    public BombType BombType;

    private float _speed;
    private bool _dragging;
    private Vector3 _offset;
    private bool _sorted;
    private bool _inside;

    void Start ()
    {
        _speed = 1;
        _sorted = false;
        _inside = false;
    }

	void Update ()
    {
	}

    private void OnMouseDown()
    {
        if (_sorted)
            return;

        _dragging = true;
        _offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    private void OnMouseUp()
    {
        _dragging = false;
        _sorted = _inside;

        if (_sorted)
        {
            OnSorted();
        }
    }

    private void OnSorted()
    {
        ParticleEffect.Stop();
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        RigidBody.MovePosition(Camera.main.ScreenToWorldPoint(newPosition) + _offset);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        _inside = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        _inside = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // When the bomb hits a wall
        if ((!_dragging && collision.gameObject.tag == "OutsideCollider") ||
            (_sorted && collision.gameObject.tag == "InsideCollider"))
        {
            var normal = collision.contacts[0].normal;
            RigidBody.velocity = Vector3.Reflect(RigidBody.velocity, normal);
        }
    }
}
