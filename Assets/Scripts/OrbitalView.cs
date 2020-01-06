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

    if (CamScale <= 0.55f && Orbital.Type == OrbitalType.Planet && Orbital.OrbitalDistance < 1000 && label.alpha == 1)
    {
      // hide label
      FadeOut(label, 0.5f);
    }
    else if (CamScale > 0.55f && Orbital.Type == OrbitalType.Planet && Orbital.OrbitalDistance < 1000 && label.alpha == 0)
    {
      FadeIn(label, 0.5f);
    }
    
    if(CamScale < 0.75f && Orbital.Type == OrbitalType.Moon && label.alpha == 1)
    {
      // hide label
      FadeOut(label, 0.5f);
    }
    else if(CamScale >= 0.75f && Orbital.Type == OrbitalType.Moon && label.alpha == 0)
    {
      FadeIn(label, 0.5f);
    }

    //Debug.LogFormat("CamScale: {0}", CamScale);
  }

  private void FadeOut(TMP_Text label, float duration = 0.5f)
  {
    StartCoroutine(Fade(label, FadeDirection.Out, duration));
  }
  private void FadeIn(TMP_Text label, float duration = 0.5f)
  {
    StartCoroutine(Fade(label, FadeDirection.In, duration));
  }

  private IEnumerator Fade(TMP_Text label, FadeDirection direction, float duration = 0.5f)
  {
    float start = (direction == FadeDirection.In) ? 0f : 1f;
    float end = (direction == FadeDirection.Out) ? 0f : 1f;
    float currentTime = 0f;
    while (currentTime < duration)
    {
      float alpha = Mathf.Lerp(start, end, currentTime / duration);
      label.color = new Color(label.color.r, label.color.g, label.color.b, alpha);
      currentTime += Time.deltaTime;
      yield return null;
    }
    label.alpha = (direction == FadeDirection.In) ? 1f : 0f;
    yield break;
  }

  private enum FadeDirection
  {
    In,
    Out
  }
}
