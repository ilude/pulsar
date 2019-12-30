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

      sol.AddChild(new Orbital(new OrbitalConfig("Mercury") { Mass = 3.302e23f, Distance = 57909175000 }));
      sol.AddChild(new Orbital(new OrbitalConfig("Venus") { Mass = 4.8690e24f, Distance = 108208930000 }));
      sol.AddChild(new Orbital(new OrbitalConfig("Earth") { Mass = 5.972e24f, Distance = 149597890000 }))
        .AddChild(new Orbital(new OrbitalConfig("Luna") {Mass = 7.3476e22f, Distance = 384400000, Type = OrbitalType.Moon }));
      sol.AddChild(new Orbital(new OrbitalConfig("Mars") { Mass = 6.4191e23f, Distance = 227936640000 }));
      sol.AddChild(new Orbital(new OrbitalConfig("Jupiter") { Mass = 1.8987e27f, Distance = 778412010000 }));
      sol.AddChild(new Orbital(new OrbitalConfig("Saturn") { Mass = 5.6851e26f, Distance = 1426725400000 }));
      sol.AddChild(new Orbital(new OrbitalConfig("Uranus") { Mass = 8.6849e25f, Distance = 2870972200000 }));
      sol.AddChild(new Orbital(new OrbitalConfig("Neptune") { Mass = 1.0244e26f, Distance = 4498252900000 }));
      
    }
  }
}