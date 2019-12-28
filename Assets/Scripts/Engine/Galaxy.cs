using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sailfin
{ 
  public class Galaxy
  {
    public ISystemGenerator Generator { get; private set; }
    public bool IsGenerated { get; private set; }
    public List<Star> Systems { get; }
    public ulong GalacticTime { get; private set; }
    public DateTime Date { get; internal set; }

    public Galaxy()
    {
      GalacticTime = 0;
      Date = new DateTime(2025, 1, 1);
      Systems = new List<Star>();
    }

    #region Generation

    public void Generate(ISystemGenerator generator)
    {
      Generator = generator;
      Generator.CreateSystemsIn(this);
      Update();
      IsGenerated = true;
    }

    #endregion

    #region Update

    public void Update()
    {
      if (IsGenerated == false) return;

      foreach (var star in Systems) star.Update(GalacticTime);
    }

    #endregion

  }
}
