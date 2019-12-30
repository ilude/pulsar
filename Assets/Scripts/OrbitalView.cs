using Sailfin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

  private TMP_Text label;
  private float labelStartOffset = 0;
  private Vector3 labelStartScale;
  private float CamMin => GalaxyController.Camera.MinCameraDistance;
  private float CamMax => GalaxyController.Camera.MaxCameraDistance;
  private float CamCur => GalaxyController.Camera.CurCameraDistance;
  private float rate => GalaxyController.LabelRate;
  private Vector3 LabelScale => new Vector3(GalaxyController.LabelScale, GalaxyController.LabelScale, GalaxyController.LabelScale);


  // Start is called before the first frame update
  void Start()
  {
    
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (Orbital == null) return;
    this.name = Orbital.Name;

    this.transform.position = Clamp(Orbital.Position / GalacticScale, Center, MinDistance);
    if (this.transform.parent != null) this.transform.position += this.transform.parent.position;
  }

  private void Update()
  {
    if (labelStartOffset == 0)
    {
      Body.localScale = GalaxyController.GetBodySize(Orbital.Type);

      labelStartScale = Body.localScale;
      labelStartOffset = (Body.localScale.z * 0.5f) + 5;
      var canvas = this.GetComponentInChildren<Canvas>();
      canvas.name = string.Format("{0} Label", this.name);

      label = canvas.GetComponentInChildren<TMP_Text>();
      label.text = this.name;
    }

    
    if (CamCur == Mathf.Infinity || CamCur <= 0) return;
    var offset = labelStartOffset * (1 - (CamCur / (CamMax - CamMin) * rate));
    label.transform.position = (Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, offset, 0)));
    label.transform.localScale = labelStartScale * ((1f - (CamCur / (CamMax - CamMin)))/100);
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
