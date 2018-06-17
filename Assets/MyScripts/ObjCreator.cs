using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjCreator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

  public RoomControl PrefabEmptyRoom;
  public RoomControl PrefabPopulatedRoom;

  public float EnlageCoeff = 1.2f;
  private Vector3 nomalScale;
  private bool scaled = false;

  public void CreateEmptyRoom()
  {
    var newRoom=Instantiate(PrefabEmptyRoom, new Vector3(-11,8.6f), Quaternion.identity);
    newRoom.StartMoving();
  }

  public void CreatePopulatedRoom()
  {
    var newRoom = Instantiate(PrefabPopulatedRoom, new Vector3(-11, 5.35f), Quaternion.identity);
    newRoom.StartMoving();
  }

  // Use this for initialization
  void Start () {
    nomalScale = transform.localScale;
  }

  // Update is called once per frame
  void Update () {
    var selectable = gameObject.GetComponent<Selectable>();
    if (GameControl.Instance != null && GameControl.Instance.HasActiveRoom)
    {
      selectable.interactable = false;
      if (scaled)
      {
        scaled = false;
        transform.localScale = nomalScale;
      }
    }
    else
      selectable.interactable = true;
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    if (gameObject.GetComponent<Selectable>().interactable)
    {
      if (!scaled)
      {
        scaled = true;
        transform.localScale = new Vector3(nomalScale.x * EnlageCoeff, nomalScale.y * EnlageCoeff, nomalScale.z);
      }
    }
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    scaled = false;
    transform.localScale = nomalScale;
  }
}
