using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomControl : CollisionLimiter
{

	// Use this for initialization
	protected override void Start () {
    base.Start();
  }

  // Update is called once per frame
  protected override void Update () {
    // base.Update();
    if (IsMoving)
    {
      float distToScreen = Camera.main.WorldToScreenPoint(transform.position).z;
      Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distToScreen);
      Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
      MoveConstrained(newPos);
    }
  }

  public bool IsAlignedTo(RoomControl siblingRoom)
  {
    bool aligned = false;
    var thiBc = GetComponent<BoxCollider2D>();
    float thisExtX = thiBc.bounds.extents.x;
    float thisExtY = thiBc.bounds.extents.y;
    var siblingBc= siblingRoom.GetComponent<BoxCollider2D>();
    float siblingExtX = siblingBc.bounds.extents.x;
    float siblingExtY = siblingBc.bounds.extents.y;

    aligned = (Mathf.Approximately(transform.position.x, siblingRoom.transform.position.x)
     && Mathf.Approximately(Mathf.Abs(transform.position.y - siblingRoom.transform.position.y),
     thisExtY + siblingExtY))
     || (Mathf.Approximately(transform.position.y, siblingRoom.transform.position.y)
     && Mathf.Approximately(Mathf.Abs(transform.position.x - siblingRoom.transform.position.x),
     thisExtX + siblingExtX));
    return aligned;
  }

  public bool IsDocked()
  {
    var doorEdges=GetComponentsInChildren<EdgeCollider2D>();
    return doorEdges.Any(d => d.enabled == false);
  }


  float Hit2DDistance(RaycastHit2D hit2D)
  {
    float d = Mathf.Infinity;

    if (hit2D.collider != null)
      d =((Vector2)hit2D.transform.position - (Vector2)this.transform.position).SqrMagnitude();
    return d;
  }

  protected override void MoveConstrained(Vector3 newPos)
  {
    Vector2 moveVec = new Vector2(newPos.x - transform.position.x, newPos.y - transform.position.y);
    float distance = moveVec.magnitude;
    var thiBc = GetComponent<BoxCollider2D>();
    float thisExtX = thiBc.bounds.extents.x;
    float thisExtY = thiBc.bounds.extents.y;
    bool roomDocked = IsDocked();

    int count = bc2d.Cast((Vector2)newPos, contactFilter, hitBuffer, distance, true);
    if (count > 0)
    {
      if (roomDocked)
      {
         newPos = transform.position;
      }
      else
      {
        var nearestHit = hitBuffer.OrderBy(hd => Hit2DDistance(hd)).First();
        Vector2 hitNormal = nearestHit.normal;
        float hitExtX = thisExtX;
        float hitExtY = thisExtY;
        var hitBc = nearestHit.collider as BoxCollider2D;
        if (hitBc != null)
        {
          hitExtX = hitBc.bounds.extents.x;
          hitExtY = hitBc.bounds.extents.y;
        }

        float defContactOffset = Physics2D.defaultContactOffset; // Vector2.kEpsilon;
        if (Mathf.Abs(hitNormal.x) < defContactOffset)
          hitNormal.x = 0;
        if (Mathf.Abs(hitNormal.y) < defContactOffset)
          hitNormal.y = 0;
        float signHitX = Mathf.Sign(hitNormal.x);
        float signHitY = Mathf.Sign(hitNormal.y);
        if (Mathf.Abs(hitNormal.x) > Mathf.Abs(hitNormal.y) && signHitX == -Mathf.Sign(moveVec.x))
        {
          newPos.x = nearestHit.transform.position.x + signHitX * (thisExtX + hitExtX);
          newPos.y = nearestHit.transform.position.y; // выравнивание центров по Y
        }
        else if (Mathf.Abs(hitNormal.x) < Mathf.Abs(hitNormal.y) && signHitY == -Mathf.Sign(moveVec.y))
        {
          newPos.y = nearestHit.transform.position.y + signHitY * (thisExtY + hitExtY);
          newPos.x = nearestHit.transform.position.x; // выравнивание центров по X
        }
      }
    }
    transform.position = newPos;
  }

}
