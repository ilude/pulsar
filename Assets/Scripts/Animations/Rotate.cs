using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rotate : MonoBehaviour
{
  [Range(0,45)]
  public float RotationSpeed = 5;

  // Update is called once per frame
  void FixedUpdate()
  {
    this.transform.Rotate(0, RotationSpeed, 0);
  }
}
