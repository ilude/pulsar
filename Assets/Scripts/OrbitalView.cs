using Sailfin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalView : MonoBehaviour
{
  public Orbital Orbital;
  public GalaxyController GalaxyController;

  private Transform _body;
  private Transform Body => _body ?? (_body = this.transform.Find("Body"));
  private float MinDistance => GalaxyController.MinOrbitalDistance;
  private float GalacticScale => GalaxyController.GalacticScale;
  private Vector3 Center => this.transform.parent.position;
  private Vector3 Normalized = new Vector3(1, 1, 1);

  // Start is called before the first frame update
  void Start()
  {
    this.name = Orbital.Name;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (Orbital == null) return;
    this.transform.position = Clamp(Orbital.Position / GalacticScale, Center, MinDistance);
    if (this.transform.parent != null) this.transform.position += this.transform.parent.position;
    Body.localScale = GalaxyController.GetBodySize(Orbital.Type);
  }

  private Vector3 Clamp(Vector3 position, Vector3 center, float min)
  {
    var offset = position - center;
    var distance = offset.sqrMagnitude;
    if (distance < MinDistance * MinDistance)
      position = offset.normalized * min;

    return position;
  }
}
