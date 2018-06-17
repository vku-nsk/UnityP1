using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrashZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  , IPointerClickHandler
{
  Vector3 normalScale;
  float EnlageCoeff = 1.8f;

  // Use this for initialization
  void Start () {
    normalScale = transform.localScale;
  }

  // Update is called once per frame
  void Update () {
		
	}

  public void OnPointerEnter(PointerEventData eventData)
  {
    var img = GetComponent<Image>();
    if (GameControl.Instance.HasActiveRoom)
    {
      img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);
      transform.localScale = new Vector3(normalScale.x * EnlageCoeff, normalScale.y * EnlageCoeff, normalScale.z);
    }
    else if (img.color.a != 1)
      img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    var img = GetComponent<Image>();
    img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    transform.localScale = normalScale;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    var img = GetComponent<Image>();
    if (GameControl.Instance.ActiveRoom != null)
    {
      Destroy(GameControl.Instance.ActiveRoom);
    }
    img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    transform.localScale = normalScale;
  }
}
