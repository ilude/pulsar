using Sailfin;
using Sailfin.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyController : MonoBehaviour
{
  [EnumNamedArray(typeof(OrbitalType))]
  public GameObject[] OrbitalPreFabs = new GameObject[Enum.GetValues(typeof(OrbitalType)).Length];

  [Range(1, 100)]
  public int Scaler = 50;

  [Range(1, 10)]
  public int ScalerPower = 7;

  public ISystemGenerator SystemGenerator;

  public float GalacticScale => Mathf.Pow(Scaler, ScalerPower);

  

  public Galaxy Galaxy;

  // Start is called before the first frame update
  void Start()
  {
    Galaxy = new Galaxy();
    Galaxy.Generate(SystemGenerator);
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (Galaxy == null || !Galaxy.IsGenerated) return;

    Galaxy.Update();
  }

 private GameObject GetPlanetPrefab(OrbitalType type) => OrbitalPreFabs[(int)type] ?? OrbitalPreFabs[(int)OrbitalType.Planet];
  
}
