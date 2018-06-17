using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
    // doorClosedEdge используется для взаимодействия с объектом, пытающимся "пройти в дверь"
    // для взаимодействия с другими дверями при стыковке используется триггерный BoxCollider2D
    var doorClosedEdge = GetComponent<EdgeCollider2D>();
    doorClosedEdge.enabled = true;
    var rndr = GetComponent<SpriteRenderer>();
    rndr.color = Color.black;
  }

  // Update is called once per frame
  void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "DOOR")
    {
      bool roomsAligned = GetComponentInParent<RoomControl>().IsAlignedTo(other.gameObject.GetComponentInParent<RoomControl>());
      if (roomsAligned)
      {
        var doorClosedEdge = GetComponent<EdgeCollider2D>();
        doorClosedEdge.enabled = false;
        var rndr = GetComponent<SpriteRenderer>();
        rndr.color = Color.white;
      }
    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    if (other.gameObject.tag == "DOOR")
    {
      bool roomsAligned = GetComponentInParent<RoomControl>().IsAlignedTo(other.gameObject.GetComponentInParent<RoomControl>());
      var doorClosedEdge = GetComponent<EdgeCollider2D>();
      var rndr = GetComponent<SpriteRenderer>();
      if (roomsAligned)
      {
        doorClosedEdge.enabled = false;
        rndr.color = Color.white;
      }
      else
      {
        doorClosedEdge.enabled = true;
        rndr.color = Color.black;
      }
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag == "DOOR")
    {
      var doorClosedEdge = GetComponent<EdgeCollider2D>();
      doorClosedEdge.enabled = true;
      var rndr = GetComponent<SpriteRenderer>();
      rndr.color = Color.black;
    }
  }

}

