using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLimiter : MonoBehaviour {

  float moveSpeed = 1.0f;

  protected Rigidbody2D rb2d=null;
  protected BoxCollider2D bc2d;
  protected ContactFilter2D contactFilter;
  protected RaycastHit2D[] hitBuffer = new RaycastHit2D[8];
  //  protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(8);
  protected bool _isMoving = false;
  Color activColor = Color.yellow;

  protected bool IsMoving
  {
    get { return _isMoving; }
    set
    {
      if (_isMoving != value)
      {
        _isMoving = value;
        var rndr = GetComponent<SpriteRenderer>();
        if (_isMoving)
        {
          GameControl.Instance.ActiveRoom = gameObject;
          rndr.color = activColor;
        }
        else
        {
          GameControl.Instance.ActiveRoom = null;
          rndr.color = Color.white;
        }
      }
    }
  }

  protected const float shellRadius = 0.01f;

  void OnEnable()
  {
    // rb2d = GetComponent<Rigidbody2D>();
    bc2d = GetComponent<BoxCollider2D>();
    activColor.a = 0.8f;
  }

  protected virtual void Start()
  {
    contactFilter.useTriggers = false;
    contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    contactFilter.useLayerMask = true;
  }

  private void OnDestroy()
  {
    IsMoving = false;
  }

  public void StartMoving()
  {
    IsMoving = true;
  }

  public void StopMoving()
  {
    IsMoving = false;
  }

  // Update is called once per frame
  protected virtual void Update () {
    // плавное перемещение стрелками
    if (IsMoving)
    {
      var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
      Vector3 newPos = transform.position + move * moveSpeed * Time.deltaTime;
      MoveConstrained(newPos);
    }
  }

  private void OnMouseDown()
  {
    if(transform.position.x > -8.3) // левее панели кнопок
      IsMoving = !IsMoving;
  }

  protected virtual void MoveConstrained(Vector3 newPos)
  {
    Vector2 moveVec = new Vector2(newPos.x - transform.position.x, newPos.y - transform.position.y);
    float distance = moveVec.magnitude;

    
    int count = rb2d.Cast(moveVec, contactFilter, hitBuffer, distance + shellRadius);
    if (count > 0)
    {
      //hitBufferList.Clear();
      //for (int i = 0; i < count; i++)
      //{
      //  hitBufferList.Add(hitBuffer[i]);
      //}
    }
    transform.position = newPos;
  }

}
