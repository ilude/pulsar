using UnityEngine;

namespace Sailfin
{
  public struct OrbitalConfig
  {
    public string Name;
    public float Mass;
    public ulong Distance;
    public OrbitalType Type;
    public float? InitialAngle;

    public OrbitalConfig(string name)
    {
      Name = name;
      Mass = 0;
      Distance = 0;
      Type = OrbitalType.Planet;
      InitialAngle = null;
    }

    public static float RandomAngle => Random.Range(0f, 359f);
  }
}
