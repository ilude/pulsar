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
  private float currBodyScale;

  private float CamScale => GalaxyController.Camera.CamScale;

  // Start is called before the first frame update
  void Start()
  {
    name = Orbital.Name;
    transform.position = Orbital.Position;

    Body.localScale = GalaxyController.GetBodySize(Orbital.Type);

    labelStartOffset = (Body.localScale.z * 0.4f);
    var canvas = this.GetComponentInChildren<Canvas>();
    canvas.name = string.Format("{0} Label", name);

    label = canvas.GetComponentInChildren<TMP_Text>();
    label.text = name;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    transform.position = Orbital.Position;
  }

  private void LateUpdate()
  {
    Camera.main.ResetWorldToCameraMatrix(); // Force camera matrix to be updated
    var offset = (CamScale == 0) ? labelStartOffset : labelStartOffset / CamScale;
    label.transform.position = (GalaxyController.Camera.camera.WorldToScreenPoint(transform.position + new Vector3(0, (float)offset, 0)));
    label.transform.localScale = Vector3.one * CamScale;

    if (CamScale <= 0.55f && Orbital.Type == OrbitalType.Planet && Orbital.OrbitalDistance < 1000 && label.enabled)
    {
      // hide label
      label.CrossFadeAlpha(0.0f, 1f, false);
      label.enabled = false;
    }
    else if (CamScale > 0.55f && Orbital.Type == OrbitalType.Planet && Orbital.OrbitalDistance < 1000 && label.enabled == false)
    {
      label.CrossFadeAlpha(1.0f, 1f, false);
      label.enabled = true;
    }
    
    if(CamScale < 0.75f && Orbital.Type < OrbitalType.Planet && label.enabled)
    {
      // hide label
      label.CrossFadeAlpha(0.0f, 1f, false);
      label.enabled = false;
    }
    else if(CamScale >= 0.75f && Orbital.Type < OrbitalType.Planet && label.enabled == false)
    {

      label.CrossFadeAlpha(1.0f, 1f, false);
      label.enabled = true;
    }

    Debug.LogFormat("CamScale: {0}", CamScale);
  }

  public IEnumerator FadeTextToFullAlpha(float t, TMP_Text label)
  {
    label.enabled = true;
    label.color = new Color(label.color.r, label.color.g, label.color.b, 0);
    while (label.color.a < 1.0f)
    {
      label.color = new Color(label.color.r, label.color.g, label.color.b, label.color.a + (Time.deltaTime / t));
      yield return null;
    }
  }

  public IEnumerator FadeTextToZeroAlpha(float t, TMP_Text label)
  {
    label.color = new Color(label.color.r, label.color.g, label.color.b, 1);
    while (label.color.a > 0.0f)
    {
      label.color = new Color(label.color.r, label.color.g, label.color.b, label.color.a - (Time.deltaTime / t));
      yield return null;
    }
    label.enabled = false;
  }
}
