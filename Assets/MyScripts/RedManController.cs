using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedManController : MonoBehaviour {

	// Use this for initialization
	void Start () {
    var rb2d = GetComponent<Rigidbody2D>();
    rb2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
  }

  // вызывается перед  Start ()
  void OnEnable()
  {
  }

  // Update is called once per frame
  void Update () {
		
	}
}
