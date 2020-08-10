using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

public class MineableTilemap : MonoBehaviour
{
    private Tilemap _tilemap;
    private GameObject _playerGameObject;
    private Player _playerComponent;
    private Renderer _playerRenderer;

    private List<GridData> _gridData;

    [SerializeField] private bool isCollectable = false;
    [SerializeField] private int durability = 1;
    [SerializeField] private Sprite[] damagedSprites;

    // Start is called before the first frame update
    void Start()
    {
        _gridData = new List<GridData>();

        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _playerComponent = _playerGameObject.GetComponent<Player>();

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
        
        if (!CheckIsInRange())
            return;

        var a = _tilemap.GetTile(clickedCellPosition);
        var b = gameObject.tag;

        if (gameObject.CompareTag("Ore") && isCollectable) // Nie wiem czy jest sens sprawdzać oba
        {
            //play sound?

            //get points

            Debug.Log("Złotko stonks!");
        }


        if (durability > 1)
        {
            int currentDamageLevel;
            var currentCellData = _gridData.FirstOrDefault(x => x.Position == clickedCellPosition);

            if (currentCellData == null)
            {
                var currentGridData = new GridData(clickedCellPosition, durability - 1);
                _gridData.Add(currentGridData);
                currentDamageLevel = durability - currentGridData.Durability;
            }
            else
            {
                currentCellData.Durability--;

                if (currentCellData.Durability <= 0)
                {
                    RemoveTilemap(clickedCellPosition, currentCellData);
                    return;
                }

                currentDamageLevel = durability - currentCellData.Durability;
            }

            ChangeTileSprite(currentDamageLevel, clickedCellPosition);
        }
        else
        {
            _tilemap.SetTile(clickedCellPosition, null);
        }
    }

    private void RemoveTilemap(Vector3Int clickedCellPosition, GridData currentCellData)
    {
        _tilemap.SetTile(clickedCellPosition, null);
        _gridData.Remove(currentCellData);
    }

    private void ChangeTileSprite(int currentDamageLevel, Vector3Int clickedCellPosition)
    {
        Tile newTile = ScriptableObject.CreateInstance<Tile>();
        newTile.sprite = damagedSprites[damagedSprites.Length - currentDamageLevel];
        _tilemap.SetTile(clickedCellPosition, newTile);
    }

    private bool CheckIsInRange()
    {
        var worldClickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var distance = Vector2.Distance(_playerGameObject.transform.position, worldClickPoint);
        var isInRange = distance <= _playerComponent.MiningRange;
        return isInRange;
    }

    private Vector3Int GetClickedCellPosition()
    {
        return _tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
