using Sailfin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrbitalView : MonoBehaviour
{
  public Orbital Orbital;
  public GalaxyController GalaxyController;
  public float Scale = 0;

  private Transform _body;
  private Transform Body => _body ?? (_body = this.transform.Find("Body"));
  private float MinDistance => GalaxyController.MinOrbitalDistance;
  private float GalacticScale => GalaxyController.GalacticScale;
  private Vector3 Center => this.transform.parent.position;
  private Vector3 Normalized = new Vector3(1, 1, 1);

  private TMP_Text label;
  public float labelStartOffset = 0;
  public Vector3 labelStartScale;
  private float CamMin => GalaxyController.Camera.MinCameraDistance;
  private float CamMax => GalaxyController.Camera.MaxCameraDistance;
  private float CamCur => GalaxyController.Camera.CurCameraDistance;
  private float rate => GalaxyController.LabelRate;
  //private Vector3 LabelScale => new Vector3(GalaxyController.LabelScale, GalaxyController.LabelScale, GalaxyController.LabelScale);

  private Vector3 OrbitalPosition;
  private Vector3 LabelPosition;
  private Vector3 LabelScale;

  // Start is called before the first frame update
  void Start()
  {
    this.gameObject.SetActive(false);
    this.name = Orbital.Name;

    Body.localScale = GalaxyController.GetBodySize(Orbital.Type);

    labelStartScale = this.transform.localScale;
    labelStartOffset = (Body.localScale.z * 0.5f) + 5;
    var canvas = this.GetComponentInChildren<Canvas>();
    canvas.name = string.Format("{0} Label", this.name);

    label = canvas.GetComponentInChildren<TMP_Text>();
    label.text = this.name;

    var parentPosition = (this.transform.parent != null) ? this.transform.parent.position : Vector3.zero;
    this.transform.position = Clamp(Orbital.Position / GalacticScale, Center, MinDistance) + parentPosition;
    this.gameObject.SetActive(true);
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    this.transform.position = OrbitalPosition;
    label.transform.position = LabelPosition;
    label.transform.localScale = LabelScale;
  }

  private void Update()
  {
    var parentPosition = (this.transform.parent != null) ? this.transform.parent.position : Vector3.zero;
    OrbitalPosition = Clamp(Orbital.Position / GalacticScale, Center, MinDistance) + parentPosition;

    if (CamCur == Mathf.Infinity || CamCur <= 0) return;

    var scale = (1 - (CamCur / (CamMax - CamMin)));
    var offset = labelStartOffset * scale;
    LabelPosition = (Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, offset, 0)));
    LabelScale = labelStartScale * scale;
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
