using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sailfin
{
  public class SolGenerator : ISystemGenerator
  {
    public void CreateSystemsIn(Galaxy galaxy)
    {
      var sol = new Star(Star.DefaultConfig);
      galaxy.Systems.Add(sol);

      sol.AddChild(new Orbital(new OrbitalConfig
        {
          Name = "Mercury",
          Mass = 5.972e24f,
          OrbitalDistance = 149597890000,
          Type = OrbitalType.Planet,
          InitialAngle = OrbitalConfig.RandomAngle
        }));
    }
  }
}