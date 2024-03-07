using Singletons;
using Turrets.Scripts;
using UnityEngine;

namespace Building_Placement
{
    public class GridManager : SimpleSingleton<GridManager>
    {
	    [SerializeField] private int xSize;
	    [SerializeField] private int zSize;
	    [SerializeField] private int cellSize;
	    
	    private BuildingGrid<Turret> _grid;

	    private void Awake()
	    {
		    _grid = new BuildingGrid<Turret>(xSize, zSize, cellSize);
		    _grid.SetCellSize(cellSize);
		    _grid.SetSizeX(xSize);
		    _grid.SetSizeZ(zSize);
	    }
	    
	    public void PlaceTurret(Vector3 worldCoordinates, Turret turretToPlace, out Vector3 objectWorldCoordinates, out int ok)
	    {
		    GetGridRelativePosition(worldCoordinates, out var xCoordinate, out var zCoordinate);
		    _grid.SetGridValue(xCoordinate, zCoordinate, turretToPlace, out ok);
		    objectWorldCoordinates = GetWorldPosition(xCoordinate, zCoordinate);
	    }
	    
	    private Vector3 GetWorldPosition(int xCoordinate, int zCoordinate)
	    {
		    return new Vector3(xCoordinate, 0f, zCoordinate) * cellSize;
	    }

	    private void GetGridRelativePosition(Vector3 worldPosition, out int xCoordinate, out int zCoordinate)
	    {
		    xCoordinate = Mathf.FloorToInt(worldPosition.x / cellSize);
		    zCoordinate = Mathf.FloorToInt(worldPosition.z / cellSize);
	    }
    }
}
