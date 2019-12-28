using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sailfin
{
  public struct OrbitalConfig
  {
    public string Name;
    public ulong Mass;
    public ulong OrbitalDistance;
    public OrbitalType Type;
    public float InitialAngle;
  }
}
