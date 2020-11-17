using System.Collections.Generic;
using UnityEngine;

public class SpawnPopups : MonoBehaviour
{
    [SerializeField] private List<Popups> objectPoolsToSpawnFrom = null;

    [SerializeField] private Transform topLeftSpawn = null;
    [SerializeField] private Transform bottomRightSpawn = null;
    [SerializeField] private ObjectPooler objectPooler = null;

    public void Spawn()
    {
        int iconIndex = Random.Range(0, objectPoolsToSpawnFrom.Count);
        float x = Random.Range(topLeftSpawn.position.x, bottomRightSpawn.position.x);
        float y = Random.Range(bottomRightSpawn.position.y, topLeftSpawn.position.y);

        Popups icon = objectPoolsToSpawnFrom[iconIndex];
        Vector2 posToSpawn = new Vector2(x, y);

        float scale = Random.Range(icon.minScaleSize, icon.maxScaleSize);

        GameObject gameObject = objectPooler.SpawnGameObject(posToSpawn, Quaternion.identity, icon.poolId);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }
}
