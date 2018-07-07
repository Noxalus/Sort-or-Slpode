using System.Collections;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject BlackBombPrefab;
    public GameObject RedBombPrefab;
    public GameObject BombsHolder;
    public float MinDirectionAngle;
    public float MaxDirectionAngle;

    void Start()
    {
        StartCoroutine(SpawnBombs());
	}

	void Update()
    {
	}

    IEnumerator SpawnBombs()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 0.5f));

        var numberToSpawn = Random.Range(1, 3);

        for (int i = 0; i < numberToSpawn; i++)
            SpawnBomb();

        StartCoroutine(SpawnBombs());
    }

    private void SpawnBomb()
    {
        var bombToInstantiate = Random.value > 0.5 ? BlackBombPrefab : RedBombPrefab;
        var newBomb = Instantiate(bombToInstantiate, BombsHolder.transform);
        newBomb.transform.localPosition = Vector3.zero;

        Vector3 v = Quaternion.AngleAxis(Random.Range(MinDirectionAngle, MaxDirectionAngle), Vector3.forward) * Vector3.up;
        var rigidbody = newBomb.GetComponent<Rigidbody2D>();
        rigidbody.velocity = v * Random.Range(1.5f, 3f);

        var bombComponent = newBomb.GetComponent<Bomb>();
    }
}
