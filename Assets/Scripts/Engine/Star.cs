using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sailfin
{
  public class Star : Orbital
  {
    public Star(OrbitalConfig config) : base(config) {}

    // stars don't move
    protected override void UpdatePosition(ulong galacticTime) {}
  }
}
