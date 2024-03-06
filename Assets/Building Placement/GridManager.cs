using Singletons;
using Turrets.Scripts;
using UnityEngine;

namespace Building_Placement
{
    public class GridManager : SimpleSingleton<GridManager>
    {
	    private BuildingGrid<Turret> _grid;
	    private int                  _cellSize;

	    private void Awake()
	    {
		    _grid     = GetComponent<BuildingGrid<Turret>>();
		    _cellSize = _grid.CellSize();
	    }
	    
	    public void PlaceTurret(Vector3 worldCoordinates, Turret turretToPlace)
	    {
		    GetGridRelativePosition(worldCoordinates, out var xCoordinate, out var zCoordinate);
		    _grid.SetGridValue(xCoordinate, zCoordinate, turretToPlace);
	    }
	    
	    private Vector3 GetWorldPosition(int xCoordinate, int zCoordinate)
	    {
		    return new Vector3(xCoordinate, 0f, zCoordinate) * _cellSize;
	    }

	    private void GetGridRelativePosition(Vector3 worldPosition, out int xCoordinate, out int zCoordinate)
	    {
		    xCoordinate = Mathf.FloorToInt(worldPosition.x / _cellSize);
		    zCoordinate = Mathf.FloorToInt(worldPosition.z / _cellSize);
	    }
    }
}
