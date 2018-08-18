using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldManager : ManagerBase<FieldManager>
{
    [SerializeField] float groundSize = 50f;
    [SerializeField] GameObject endArea;
    [SerializeField] GameObject groundPrefab;
    [SerializeField] GameObject groundRoot;
    int ranRange = 3;
    [SerializeField] List<GameObject> grounds;
    void Awake()
    {
        Set(this);
        Application.targetFrameRate = 60;
    }
    void Update()
    {
    }
    public void CreateNewBlock()
    {
        var newCubePos = new Vector3(0, 0, ranRange * groundSize);
        var nextEndAreaPos = new Vector3(0, 0, (ranRange - 2) * groundSize);
        var go = Instantiate(groundPrefab, newCubePos, Quaternion.Euler(0, 0, 0));
        go.transform.parent = groundRoot.transform;
        grounds.Add(go);
        endArea.transform.position = new Vector3(0, 0, nextEndAreaPos.z);

        if (grounds.Count > 5)
        {
            var del = grounds.First();
            grounds.Remove(del);
            Destroy(del);
        }

        ranRange++;
    }
}
