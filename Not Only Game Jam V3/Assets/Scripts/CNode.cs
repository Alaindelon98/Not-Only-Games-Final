using UnityEngine;


public class CNode
{
    public int gridPositionX;
    public int gridPositionY;
    public int nodeSize;
    public Vector3 position;
    public bool isTransitable = true;

    public CNode fatherNode;
    public int distanceF, distanceG, distanceH;
    public bool isOpen;
    public bool isPortal;


    public CNode() { }

    public CNode(int _gridPositionX, int _gridPositionY, int _nodeSize, Vector3 _position, bool _isTransitable)
    {
        gridPositionX = _gridPositionX;
        gridPositionY = _gridPositionY;
        nodeSize = _nodeSize;
        position = _position;
        isTransitable = _isTransitable;
    }

    public void Grita()

    {
        Debug.Log(isTransitable);

    }


}