using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sailfin
{
  public class Star : Orbital
  {
    public static readonly OrbitalConfig DefaultConfig = new OrbitalConfig
    {
      Name = "Sol",
      Mass = Convert.ToUInt64(1.99e30),
      Type = OrbitalType.Star,
      OrbitalDistance = 0,
      InitialAngle = 0
    };

    public Star() : base(DefaultConfig)
    {

    }

    protected override void UpdatePosition(ulong galacticTime)
    {
      // stars don't move
    }
  }
}
