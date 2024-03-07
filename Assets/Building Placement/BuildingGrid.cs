using UnityEngine;

namespace Building_Placement
{
    public class BuildingGrid<T>
    {
        private int  _xSize;
        private int  _zSize;
        private int  _cellSize;
        private T[] _grid;
        
        public BuildingGrid(int x, int z, int cellSize)
        {
            _xSize    = x;
            _zSize    = z;
            _cellSize = cellSize;
            _grid     = new T[_xSize*_zSize];
        }
        
        
        
        public void SetGridValue(int xCoordinate, int zCoordinate, T value, out int ok)
        {
            if (xCoordinate >= _xSize || zCoordinate >= _zSize || xCoordinate < 0 || zCoordinate < 0)
            {
                ok = -1;
                return;
            }
            if (_grid[GetRelativeCoordinates(xCoordinate, zCoordinate)] is not null)
            {
                ok = 1;
                return;
            }
            _grid[GetRelativeCoordinates(xCoordinate, zCoordinate)] = value;
            ok                                                      = 0;
        }

        public T GetItem(int xCoordinate, int zCoordinate)
        {
            return _grid[GetRelativeCoordinates(xCoordinate, zCoordinate)];
        }

        public int SizeX()
        {
            return _xSize;
        }
        
        public int SizeZ()
        {
            return _zSize;
        }

        public int CellSize()
        {
            return _cellSize;
        }
        
        public void SetSizeX(int value)
        { 
            _xSize = value;
        }
        
        public void SetSizeZ(int value)
        {
            _zSize = value;
        }

        public void SetCellSize(int value)
        {
            _cellSize = value;
        }

        private int GetRelativeCoordinates(int xCoordinate, int zCoordinate)
        {
            return xCoordinate + zCoordinate * _xSize;
        }
    }
}
