using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sailfin
{
  public class Orbital
  {
    public string Name { get; protected set; }
    public float Mass { get; protected set; }
    public float TimeToOrbit { get; protected set; }
    public float InitAngle { get; protected set; }
    public float OffsetAngle { get; protected set; }
    public ulong OrbitalDistance { get; protected set; }
    public OrbitalType Type { get; protected set; }
    public virtual Vector3 Position { get; protected set; }

    public Orbital Parent { get; protected set; }
    public List<Orbital> Children { get; protected set; }
    

    public Orbital(OrbitalConfig config)
    {
      Name = config.Name;
      Mass = config.Mass;
      InitAngle = 0;// (config.InitialAngle == null) ? Random.Range(0,359) : (float)config.InitialAngle;
      OrbitalDistance = config.Distance;
      Type = config.Type;

      OffsetAngle = 0;
      Position = new Vector3(0, 0, 0);
      Children = new List<Orbital>();
    }

    internal void Update(ulong galacticTime)
    {
      UpdatePosition(galacticTime);

      foreach (var child in Children) child.Update(galacticTime);
    }

    protected virtual void UpdatePosition(ulong galacticTime)
    {
      // Advance our angle by the correct amount of time.
      OffsetAngle = (galacticTime / TimeToOrbit) * 2f * Mathf.PI;
      var position = new Vector3(
         Mathf.Sin(InitAngle + OffsetAngle) * OrbitalDistance,
        -Mathf.Cos(InitAngle + OffsetAngle) * OrbitalDistance,
         0  
       );
      if (Parent != null)
        position += Parent.Position;

      this.Position = position;
    }

    public Orbital AddChild(Orbital c)
    {
      c.Parent = this;
      Children.Add(c);

      var s1 = (39.4784176f * Mathf.Pow(c.OrbitalDistance, 3f));
      var s2 = Gravity.Constant * (this.Mass + c.Mass);
      var s3 = s1 / s2;
      c.TimeToOrbit = Mathf.Sqrt(s3);

      if((c.OrbitalDistance /= 600000000) < 15)
      {
        var multiplier = Children.IndexOf(c) + 3f;
        var minDistance = (c.OrbitalDistance * 10f) + (5f * multiplier);
        c.OrbitalDistance = Convert.ToUInt64(Mathf.RoundToInt(minDistance));
      }

      return c;
    }

    public void RemoveChild(Orbital c)
    {
      c.Parent = null;
      Children.Remove(c);
    }
  }

}
