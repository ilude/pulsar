using Sailfin;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrbitalView : MonoBehaviour
{
  public Orbital Orbital;
  public Orbital Parent;
  public GalaxyController GalaxyController;

  private Transform _body;
  private Transform Body => _body ?? (_body = this.transform.Find("Body"));

  private TMP_Text label;
  private float labelStartOffset = 0;
  private float CamScale => GalaxyController.Camera.CamScale;

  

  // Start is called before the first frame update
  void Start()
  {
    name = Orbital.Name;
    transform.position = Orbital.Position;

    Body.localScale = GalaxyController.GetBodySize(Orbital.Type);

    labelStartOffset = (Body.localScale.z * 0.5f);
    var canvas = this.GetComponentInChildren<Canvas>();
    canvas.name = string.Format("{0} Label", name);

    label = canvas.GetComponentInChildren<TMP_Text>();
    label.text = name;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    transform.position = Orbital.Position;

    var offset = labelStartOffset / CamScale;
    label.transform.position = (Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, offset, 0))); ;
    label.transform.localScale = Vector3.one * CamScale;
    Debug.LogFormat("CamScale: {0}", CamScale);
  }
}
