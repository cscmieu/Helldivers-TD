using UnityEngine;

namespace Building_Placement
{
    public abstract class GridItem : MonoBehaviour
    {
        [SerializeField] protected int xPosition;
        [SerializeField] protected int zPosition;

        public int GetPositionX()
        {
            return xPosition;
        }

        public int GetPositionZ()
        {
            return zPosition;
        }

        public void SetPositionX(int xCoordinate)
        {
            xPosition = xCoordinate;
        }
        
        public void SetPositionZ(int zCoordinate)
        {
            zPosition = zCoordinate;
        }
    }
}
