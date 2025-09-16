using UnityEngine;

public class Loopinterval : MonoBehaviour
{
    [SerializeField]
    public GameObject coinPrefab; // コインのプレハブ

    [SerializeField]
    public Vector3[] spawnPositions = new Vector3[10]; // 座標を10個保持
    [SerializeField]
    private float spawnInterval = 5f; // コインを生成する間隔（秒）
    [SerializeField]
    private int Number_of_coins = 3;

    void Start()
    {

        StartCoroutine(SpawnCoinLoop());
    }

    System.Collections.IEnumerator SpawnCoinLoop()
    {
        while (true)
        {
            SpawnRandomCoins(Number_of_coins); // 3個同時に降らせる
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomCoins(int count)
    {
        if (spawnPositions.Length == 0) return;

        // 重複しないインデックスをランダムで選ぶ
        System.Collections.Generic.List<int> indices = new System.Collections.Generic.List<int>();
        for (int i = 0; i < spawnPositions.Length; i++)
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

        // 指定数だけ生成
        for (int i = 0; i < count && i < indices.Count; i++)
        {
            Vector3 pos = spawnPositions[indices[i]];
            Instantiate(coinPrefab, pos, Quaternion.identity);
        }
    }

}


