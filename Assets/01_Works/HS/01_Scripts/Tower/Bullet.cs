using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     private Rigidbody2D _rigidCompo;

     private void Awake()
     {
          _rigidCompo = GetComponent<Rigidbody2D>();
     }

     private void Update()
     {
          _rigidCompo.velocity = Vector2.right;
     }
}
