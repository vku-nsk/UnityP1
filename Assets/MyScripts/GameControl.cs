using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

  public static GameControl Instance { get; private set; }


  private GameObject _activeRoom;
  public GameObject ActiveRoom
  {
    get
    {
      return _activeRoom;
    }
    set
    {
      if (_activeRoom != value)
      {
        if (_activeRoom != null)
        {
          var rc = _activeRoom.GetComponent<CollisionLimiter>();
          rc.StopMoving();
        }
        _activeRoom = value;
      }
    }
  }

  public bool HasActiveRoom
  {
    get { return _activeRoom != null; }
  }


  void Awake()
  {
    //If we don't currently have a game control...
    if (Instance == null)
      //...set this one to be it...
      Instance = this;
    //...otherwise...
    else if (Instance != this)
      //...destroy this one because it is a duplicate.
      Destroy(gameObject);
    DontDestroyOnLoad(gameObject);
  }

  // Update is called once per frame
  void Update () {
		
	}
}
