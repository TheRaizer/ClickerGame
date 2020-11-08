using System.Collections.Generic;
using UnityEngine;

public class FallingIconsManager : MonoBehaviour
{
    [SerializeField] private Transform leftSpawn = null;
    [SerializeField] private Transform rightSpawn = null;
    private ObjectPooler objectPooler;

    private void Awake()
    {
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    public void SpawnFallingIcon()
    {
        float y = leftSpawn.position.y;
        float x = Random.Range(leftSpawn.position.x, rightSpawn.position.x);
        objectPooler.SpawnGameObject(new Vector2(x, y), Quaternion.identity, "FallingIcons");
    }
}
