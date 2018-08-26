using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldManager : ManagerBase<FieldManager>
{
    [SerializeField] float groundSize = 50f;
    [SerializeField] GameObject groundPrefab;
    [SerializeField] GameObject groundRoot;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject[] startGrounds;
    int ranRange;
    int spawnIndex;
    int deleteCount;
    [SerializeField] List<GameObject> grounds;
    [SerializeField] float blockSpawnDistance;
    void Awake()
    {
        Set(this);
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        ranRange = startGrounds.Length;
        spawnIndex = ranRange - 1;
        deleteCount = ranRange + spawnIndex;
        blockSpawnDistance = groundSize;
    }
    void Update()
    {
        if (playerController != null && playerController.transform.position.z > blockSpawnDistance)
        {
            CreateNewBlock();
        }
    }
    public void CreateNewBlock()
    {
        var newCubePos = new Vector3(0, 0, ranRange * groundSize);
        blockSpawnDistance = (ranRange - spawnIndex) * groundSize;
        var go = Instantiate(groundPrefab, newCubePos, Quaternion.Euler(0, 0, 0));
        go.transform.parent = groundRoot.transform;
        go.transform.rotation = groundRoot.transform.localRotation;
        grounds.Add(go);

        if (grounds.Count > deleteCount)
        {
            var del = grounds.First();
            grounds.Remove(del);
            Destroy(del);
        }

        ranRange++;
    }
}
