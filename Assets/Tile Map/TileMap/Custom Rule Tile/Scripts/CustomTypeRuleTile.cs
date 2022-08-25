
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class CustomTypeRuleTile : RuleTile<CustomTypeRuleTile.Neighbor> 
{
    public class Neighbor : RuleTile.TilingRule.Neighbor 
    {
        public const int SameType = 3;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) 
    {
        switch (neighbor) {
            case 2: return tile == null || !(tile is CustomTypeRuleTile); 
            case Neighbor.SameType: return tile is CustomTypeRuleTile;
        }
        return base.RuleMatch(neighbor, tile);
    }

    //public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    //{
    //    if (IsRunTime)
    //    {
    //        tileData.sprite = _TileData.sprite;
    //        tileData.transform = _TileData.transform;
    //    }
    //    else
    //    {
    //        base.GetTileData(position, tilemap, ref tileData);
    //        _TileData = tileData;
    //    }
    //}
    //[HideInInspector]
    //public TileData _TileData;

    //[HideInInspector]
    //public Sprite sprite;

    //[HideInInspector]
    //public Matrix4x4 transform;


    //public bool IsRunTime = false;

}
