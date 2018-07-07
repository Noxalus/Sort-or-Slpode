using System.Collections;
using UnityEngine;

public enum BombType
{
    Black,
    Red
}

public class Bomb : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public ParticleSystem WickParticleEffect;
    public ParticleSystem ExplosionParticleEffect;
    public BombType BombType;
    public float SecondsToExplode;

    private bool _dragging;
    private Vector3 _offset;
    private bool _sorted;
    private bool _inside;
    private bool _wrongSort;
    private float _startTimer;

    void Start ()
    {
        _sorted = false;
        _inside = false;
        _wrongSort = false;

        ExplosionParticleEffect.gameObject.SetActive(false);
        _startTimer = Time.time;
    }

	void Update ()
    {
        if (!_sorted && Time.time > _startTimer + SecondsToExplode)
        {
            ExplosionParticleEffect.gameObject.SetActive(true);
            LaunchExplosionAnimation();
        }
	}

    private void LaunchExplosionAnimation()
    {
        StartCoroutine(DestroyBomb());
    }

    private IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
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
        ExplosionParticleEffect.Stop();
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
