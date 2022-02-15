using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveFoliage : MonoBehaviour
{
    float _enterOffset;
    bool _isBending;
    bool _isRebounding;
    float _colliderHalfWidth;
    float _exitOffset;
    MeshFilter _meshFilter = new MeshFilter();
    float BEND_FACTOR;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.layer == k.Layers.PLAYER)
    //     {
    //         _enterOffset = col.transform.position.x - transform.position.x;
    //     }
    // }
    // void OnTriggerStay2D(Collider2D col)
    // {
    //     if (col.gameObject.layer == k.Layers.PLAYER)
    //     {
    //         var offset = col.transform.position.x - transform.position.x;

    //         if (_isBending || Mathf.Sign(_enterOffset) != Mathf.Sign(offset))
    //         {
    //             _isRebounding = false;
    //             _isBending = true;

    //             // figure out how far we have moved into the trigger and then map the offset to -1 to 1.
    //             // 0 would be neutral, -1 to the left and +1 to the right.
    //             var radius = _colliderHalfWidth + col.bounds.size.x * 0.5f;
    //             _exitOffset = map(offset, -radius, radius, -1f, 1f);
    //             setVertHorizontalOffset(_exitOffset);
    //         }
    //     }
    // }


    // // simple method to offset the top 2 verts of a quad based on the offset and BEND_FACTOR constant
    // void setVertHorizontalOffset(float offset)
    // {
    //     var verts = _meshFilter.mesh.vertices;

    //     verts[1].x = 0.5f + offset * BEND_FACTOR / transform.localScale.x;
    //     verts[3].x = -0.5f + offset * BEND_FACTOR / transform.localScale.x;

    //     _meshFilter.mesh.vertices = verts;
        
    // }
}
