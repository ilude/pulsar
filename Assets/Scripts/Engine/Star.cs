﻿using System;
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
      Mass = 1.99e30f,
      Type = OrbitalType.Star,
      Distance = 0,
      InitialAngle = 0
    };

    public Star() : this(DefaultConfig) {}
    public Star(OrbitalConfig config) : base(config) {}

    // stars don't move
    protected override void UpdatePosition(ulong galacticTime) {}
  }
}
