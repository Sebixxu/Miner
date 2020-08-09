using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private Tilemap _tilemap;
    private List<GridData> _gridCache;

    [SerializeField] private int durability = 1;
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

    void OnMouseDown()
    {
        ProcessMineClick();
    }

    private void ProcessMineClick()
    {
        var clickedCellPosition = GetClickedCellPosition();

        var gridData = _gridCache.FirstOrDefault(x => x.Position == clickedCellPosition);
        if (gridData == null)
        {
            _gridCache.Add(new GridData(clickedCellPosition, 1));
        }
        else
        {
            gridData.Durability--;
        }

        if (gridData?.Durability <= 0)
            _tilemap.SetTile(clickedCellPosition, null);
    }

    private Vector3Int GetClickedCellPosition()
    {
        return _tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
