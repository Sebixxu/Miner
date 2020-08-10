using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Tilemap _tilemap;
    private List<GridData> _gridCache;

    [field: SerializeField] public float MiningRange { get; private set; } = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        _gridCache = new List<GridData>();

        _tilemap = gameObject.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {


    }
}