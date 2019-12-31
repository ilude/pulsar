using Sailfin;
using Sailfin.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GalaxyController : MonoBehaviour
{
  [EnumNamedArray(typeof(OrbitalType))]
  public OrbitalView[] OrbitalPreFabs = new OrbitalView[Enum.GetValues(typeof(OrbitalType)).Length];
  private OrbitalView GetPlanetPrefab(OrbitalType type) => OrbitalPreFabs[(int)type] ?? OrbitalPreFabs[(int)OrbitalType.Planet];

  [EnumNamedArray(typeof(OrbitalType))]
  public int[] OrbitalViewBodySize = new int[Enum.GetValues(typeof(OrbitalType)).Length];
  public Vector3 GetBodySize(OrbitalType type)
  {
    var size = (OrbitalViewBodySize[(int)type] > 0) ? OrbitalViewBodySize[(int)type] : DefaultBodySize;
    return new Vector3(size, size, size);
  }

  public CameraView Camera;

  [Range(1, 100)]
  public int Scaler = 50;

  [Range(1, 10)]
  public int ScalerPower = 7;

  [Range(1,100)]
  public float MinOrbitalDistance = 35f;

  [Range(1, 100)]
  public int DefaultBodySize = 20;

  [Range(1, 100)]
  public float LabelRate = 5;

  [Range(0, 1)]
  public float LabelScale = 1;
  
  public float GalacticScale => Mathf.Pow(Scaler, ScalerPower);
  
  public Galaxy Galaxy;

  private TimeBy TimeStep { get; set; }

  private bool RealTime = false;

  // Start is called before the first frame update
  void Awake()
  {
    Galaxy = new Galaxy();
    Galaxy.Generate(new SolGenerator());

    CreateView(Galaxy.Systems.First(), this.transform);
    this.gameObject.SetActive(true);
    Debug.Log("Galaxy Created!");
  }

  private void CreateView(Orbital body, Transform transform)
  {
    var view = Instantiate<OrbitalView>(GetPlanetPrefab(body.Type), transform);
    view.Orbital = body;
    view.GalaxyController = this;

    foreach (var child in body.Children) CreateView(child, view.transform);
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (Galaxy == null || !Galaxy.IsGenerated) return;

    if (RealTime) Galaxy.Advance(TimeStep);

    Galaxy.Update();
  }

  public void Pause()
  {
    RealTime = false;
  }

  [EnumAction(typeof(TimeBy))]
  public void Advance(int timeby)
  {
    Galaxy.Advance((TimeBy)timeby);
  }

  [EnumAction(typeof(TimeBy))]
  public void RunAt(int timeby)
  {
    TimeStep = (TimeBy)timeby;
    RealTime = true;
  }

  

}
