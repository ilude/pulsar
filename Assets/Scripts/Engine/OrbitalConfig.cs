using UnityEngine;

namespace Sailfin
{
  public struct OrbitalConfig
  {
    public string Name;
    public float Mass;
    public ulong OrbitalDistance;
    public OrbitalType Type;
    public float InitialAngle;

    public static float RandomAngle => Random.Range(0f, 359f);
  }
}
