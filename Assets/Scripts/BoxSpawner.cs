using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject BoxPrefab;
    public int MaxSpawnCount = 10;

    float _timer = 0f;
    int _currentCount = 0;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1f)
        {
            _timer = 0f;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        if (_currentCount >= MaxSpawnCount)
        {
            return;
        }

        Vector2 spawnPos = GetRandomPositionInCamera();
        Instantiate(BoxPrefab, spawnPos, Quaternion.identity);
        _currentCount++;
    }

    Vector2 GetRandomPositionInCamera()
    {
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Camera.main.aspect;

        float x = Random.Range(-width * 0.5f, width * 0.5f);
        float y = Random.Range(-height * 0.5f, height * 0.5f);

        return new Vector2(x, y);
    }
}