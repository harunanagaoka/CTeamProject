using UnityEngine;

public class Loopinterval : MonoBehaviour
{
    [Header("1点コイン設定")]
    [SerializeField] private GameObject coin1Prefab;
    [SerializeField] private float coin1Interval = 5f;
    [SerializeField] private int coin1Count = 3;

    [Header("3点コイン設定")]
    [SerializeField] private GameObject coin3Prefab;
    [SerializeField] private float coin3Interval = 8f;
    [SerializeField] private int coin3Count = 1;

    [SerializeField] public Vector3[] spawnPositions1 = new Vector3[10];
    [SerializeField] public Vector3[] spawnPositions3 = new Vector3[10];

    private bool hasStarted = false;

    void Update()
    {
        if (!hasStarted && Timer.IsGameStarted)
        {
            hasStarted = true;
            StartCoroutine(SpawnCoinLoop(coin1Prefab, coin1Interval, coin1Count));
            StartCoroutine(SpawnCoinLoop(coin3Prefab, coin3Interval, coin3Count));
        }
    }

    System.Collections.IEnumerator SpawnCoinLoop(GameObject prefab, float interval, int count)
    {
        while (true)
        {
            if (!remainingtime.IsTimeOver)
            {
                SpawnRandomCoins(count, prefab);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    void SpawnRandomCoins(int count, GameObject prefab)
    {
        if (spawnPositions1.Length == 0) return;

        var indices = new System.Collections.Generic.List<int>();
        for (int i = 0; i < spawnPositions1.Length; i++)
        {
            indices.Add(i);
        }
        // シャッフル
        for (int i = 0; i < indices.Count; i++)
        {
            int j = Random.Range(i, indices.Count);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }

        for (int i = 0; i < count && i < indices.Count; i++)
        {
            Vector3 pos = spawnPositions1[indices[i]];
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}


