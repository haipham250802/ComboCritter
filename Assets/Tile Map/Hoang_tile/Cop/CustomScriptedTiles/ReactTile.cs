using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "VinTools/Custom Tiles/React Tile")]
public class ReactTile : Tile
{
    //[Button("Refresh Tile Count")]
    //public void Refresh()
    //{
    //    connectZone = null;
    //}
    //public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    //{
    //    base.RefreshTile(position, tilemap);
    //}

    //public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    //{

    //    //get these values from the base class
    //    base.GetTileData(position, tilemap, ref tileData);

    //    tileData.sprite = GetSprite(position);
    //}


    //[Header("Tile block")]
    //public Vector2Int m_size = Vector2Int.one;
    //public Vector2Int m_TopRightRect = Vector2Int.one;
    //public Vector2Int m_BottomLeftRect = Vector2Int.one;
    //public Sprite[] m_Sprites;
    //List<Vector2Int[]> connectZone;
    //public Sprite GetSprite(Vector3Int pos)
    //{
    //    //check if array lenght matches the dimensions
    //    if (m_Sprites.Length != m_size.x * m_size.y) return sprite;
    //    //prevents the values to be negative
    //    while (pos.x < m_size.x) { pos.x += m_size.x; }
    //    while (pos.y < m_size.y) { pos.y += m_size.y; }

    //    //get the index on each axis
    //    int x = pos.x % m_size.x;
    //    int y = pos.y % m_size.y;

    //    //get the index in the array
    //    int index = x + (((m_size.y - 1) * m_size.x) - y * m_size.x);

    //    //returns the correct sprite
    //    return m_Sprites[index];
    //}
    //public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    //{
    //    DebugCustom.LogColor("StartUp");
    //    Vector2Int _pos = new Vector2Int(position.x, position.y);
    //    int id0 = 0;
    //    int id1 = 0;
    //    Vector2Int[] newArray = new Vector2Int[] { _pos };
    //    //check if array lenght matches the dimensions
    //    if (m_Sprites.Length != m_size.x * m_size.y) return sprite;
    //    if (connectZone == null)
    //    {
    //        connectZone = new List<Vector2Int[]>();
    //        connectZone.Add(newArray);
    //    }
    //    else
    //    {
    //        if (IsConnect(_pos, ref id0))
    //        {
    //            AddConnectTile(_pos, id0);
    //            SortArray(id0);
    //            while (IsStillConnect(_pos, id0, ref id1))
    //            {
    //                AddConnectZone(id0, id1);
    //                SortArray(id0);
    //            }
    //        }
    //        else
    //        {
    //            connectZone.Add(newArray);
    //        }
    //    }
    //    for (int i = 0; i < connectZone.Count; i++)
    //    {
    //        DebugCustom.LogColor("Zone : " + i + " : " + connectZone[i].Length);
    //    }
    //    return base.StartUp(position, tilemap, go);
    //}
    //public virtual bool IsConnect(Vector2Int _pos, ref int id0)
    //{
    //    bool conected = false;
    //    for (int i = 0; i < connectZone.Count; i++)
    //    {
    //        for (int j = 0; j < connectZone[i].Length; j++)
    //        {
    //            if (Vector2Int.Distance(_pos, connectZone[i][j]) < 1.1f)
    //            {
    //                id0 = i;
    //                conected = true;
    //                break;
    //            }
    //        }
    //    }
    //    return conected;
    //}
    //public virtual void AddConnectTile(Vector2Int _pos, int id0)
    //{
    //    Vector2Int[] newArray = new Vector2Int[connectZone[id0].Length + 1];
    //    for (int i = 0; i < connectZone[id0].Length; i++)
    //    {
    //        newArray[i] = connectZone[id0][i];
    //    }
    //    newArray[connectZone[id0].Length - 1] = _pos;
    //    connectZone[id0] = newArray;
    //}
    //public virtual bool IsStillConnect(Vector2Int _pos, int id0, ref int id1)
    //{
    //    bool connected = false;
    //    if (connectZone.Count == 1)
    //    {
    //        return false;
    //    }
    //    for (int i = 0; i < connectZone.Count; i++)
    //    {
    //        if(i != id0)
    //        {
    //            for (int j = 0; j < connectZone[i].Length; j++)
    //            {
    //                if (Vector2Int.Distance(_pos, connectZone[i][j]) < 1.1f)
    //                {
    //                    id1 = i;
    //                    connected = true;
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    return connected;
    //}
    //public virtual void AddConnectZone(int id0, int id1)
    //{
    //    Vector2Int[] oldArray = connectZone[id1];
    //    Vector2Int[] newArray = new Vector2Int[connectZone[id0].Length + oldArray.Length] ;
    //    for (int i = 0; i < connectZone[id0].Length; i++)
    //    {
    //        newArray[i] = connectZone[id0][i];
    //    }
    //    for (int i = connectZone[id0].Length - 1; i < newArray.Length - 1; i++)
    //    {
    //        newArray[i] = connectZone[id1][i - connectZone[id0].Length + 1];
    //    }
    //    connectZone[id0] = newArray;
    //    connectZone.RemoveAt(id1);
    //}
    //public void SortArray(int id0)
    //{
    //    Vector2Int[] newArray = connectZone[id0];
    //    bool sortDone = true;
    //    while (!sortDone)
    //    {
    //        for (int i = 1; i < newArray.Length; i++)
    //        {
    //            Vector2Int lastPos = newArray[i - 1];
    //            Vector2Int pos = newArray[i];
    //            if (pos.y > lastPos.y)
    //            {
    //                sortDone = false;
    //                newArray[i - 1] = pos;
    //                newArray[i] = lastPos;
    //            }
    //            else if (pos.y == lastPos.y && pos.x < lastPos.x)
    //            {
    //                sortDone = false;
    //                newArray[i - 1] = pos;
    //                newArray[i] = lastPos;
    //            }
    //        }
    //    }
    //    connectZone[id0] = newArray;
    //}
}
