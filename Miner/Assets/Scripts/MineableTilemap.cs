using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MineableTilemap : MonoBehaviour
{
    private Tilemap _tilemap;
    private List<GridData> _gridData;

    [SerializeField] private int durability = 1;
    // Start is called before the first frame update
    void Start()
    {
        _gridData = new List<GridData>();

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

        if (durability > 1)
        {
            var currentCellData = _gridData.FirstOrDefault(x => x.Position == clickedCellPosition);

            if (currentCellData == null)
            {
                _gridData.Add(new GridData(clickedCellPosition, durability - 1));
            }
            else
            {
                currentCellData.Durability--;

                if (currentCellData.Durability <= 0)
                {
                    _tilemap.SetTile(clickedCellPosition, null);
                    _gridData.Remove(currentCellData);
                }
            }
        }
        else
        {
            _tilemap.SetTile(clickedCellPosition, null);
        }
    }

    private Vector3Int GetClickedCellPosition()
    {
        return _tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
