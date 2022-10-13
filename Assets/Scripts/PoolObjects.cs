using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects
{
    private readonly List<GameObject> _poolObjects;
    private readonly GameObject _cube;
    private readonly int _startCountPool;

    public PoolObjects(GameObject cube, int startCountPool)
    {
        _cube = cube;
        _startCountPool = startCountPool;
        _poolObjects = new List<GameObject>(_startCountPool);
    }

    public IEnumerator CoroutineInit(Transform parent)
    {
        for (var i = 0; i < _startCountPool; i++)
        {
            var cube = Object.Instantiate(_cube, Vector3.zero, Quaternion.identity, parent);
            cube.SetActive(false);
            _poolObjects.Add(cube);
            yield return null;
        }
    }

    public GameObject GetObject(Transform parent)
    {
        var count = _poolObjects.Count;
        if (count != 0)
        {
            var cube = _poolObjects[count - 1];
            cube.SetActive(true);
            _poolObjects.Remove(cube);
            return cube;
        }
        else
        {
            var cube = Object.Instantiate(_cube, Vector3.zero, Quaternion.identity, parent);
            cube.SetActive(true);
            return cube;
        }
    }

    public void ReturnObject(GameObject poolObject)
    {
        poolObject.SetActive(false);
        poolObject.transform.localPosition = Vector3.zero;
        _poolObjects.Add(poolObject);
    }
}