using System;
using UnityEngine;

namespace Building_Placement
{
    public class BuildingGrid<T> : MonoBehaviour
    {
        private int  _xSize;
        private int  _zSize;
        private int  _cellSize;
        private T[,] _grid;
        
        public void SetGridValue(int xCoordinate, int zCoordinate, T value)
        {
            if (xCoordinate < _xSize && zCoordinate < _zSize && xCoordinate >= 0 && zCoordinate >= 0)
                _grid[xCoordinate, zCoordinate] = value;
        }

        public T GetItem(int xCoordinate, int zCoordinate)
        {
            return _grid[xCoordinate, zCoordinate];
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
    }
}
