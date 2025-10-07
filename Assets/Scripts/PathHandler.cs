using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    public static PathHandler Instance;

    public int pathLength;
    public List<Transform> pathTransformList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        pathLength = pathTransformList.Count;
    }

    public Transform GetPath(int index)
    {
        return pathTransformList[index];
    }
}