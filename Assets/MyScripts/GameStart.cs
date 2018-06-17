using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

  public GameControl PrefabGameControl;

  private void Awake()
  {
    if(GameControl.Instance == null)
      Instantiate(PrefabGameControl);
  }
	
}
