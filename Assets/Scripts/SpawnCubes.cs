using System.Collections;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField] private Transform _poolParent;
    [SerializeField] private GameObject _cube;
    [SerializeField, Range(0, 10)] private int _startCountPool;

    [Space, Header("Settings Cube")] 
    [SerializeField, Range(1, 50)] private float _distanceToMove;
    [SerializeField, Range(.1f, 25)] private float _speedCube;
    [SerializeField, Range(.1f, 10)] private float _spawnFrequency;

    private PoolObjects _pool;

    private void Awake()
    {
        _pool = new PoolObjects(_cube, _startCountPool);
        StartCoroutine(_pool.CoroutineInit(_poolParent));
    }

    private void Start()
    {
        StartCoroutine(CoroutineSpawnCubes());
    }

    private IEnumerator CoroutineSpawnCubes()
    {
        while (true)
        {
            var cube = _pool.GetObject(_poolParent);
            StartCoroutine(MoveObject(cube));
            yield return new WaitForSeconds(_spawnFrequency);
        }
    }

    private IEnumerator MoveObject(GameObject cube)
    {
        var cubeTransform = cube.transform;
        var target = cubeTransform.forward * _distanceToMove;
        while (Vector3.Distance(cubeTransform.localPosition, target) > 0.1f)
        {
            cubeTransform.Translate(cubeTransform.forward * _speedCube * Time.deltaTime);
            
            yield return null;
        }
        
        _pool.ReturnObject(cube);
    }
}
