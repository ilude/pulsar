using System;
using System.Collections.Generic;

namespace Sailfin
{
  public class Galaxy
  {
    public ISystemGenerator Generator { get; private set; }
    public bool IsGenerated { get; private set; }
    public List<Star> Systems { get; }
    public ulong GalacticTime { get; private set; }
    public DateTime Date { get; internal set; }

    Dictionary<TimeBy, Func<ulong>> InvokePulse = new Dictionary<TimeBy, Func<ulong>>(Enum.GetValues(typeof(TimeBy)).Length);


    public Galaxy()
    {
      GalacticTime = 0;
      Date = new DateTime(2025, 1, 1);
      Systems = new List<Star>();
      
    
      InvokePulse[TimeBy.Hour]  = () => Pulse(dateTime => dateTime.AddHours(1));
      InvokePulse[TimeBy.Day]   = () => Pulse(dateTime => dateTime.AddDays(1));
      InvokePulse[TimeBy.Week]  = () => Pulse(dateTime => dateTime.AddDays(5));
      InvokePulse[TimeBy.Month] = () => Pulse(dateTime => dateTime.AddMonths(1));
      InvokePulse[TimeBy.Year]  = () => Pulse(dateTime => dateTime.AddYears(1));
    }

    #region Generation

    public void Generate(ISystemGenerator generator)
    {
      Generator = generator;
      Generator.CreateSystemsIn(this);
      Update(0);
      IsGenerated = true;
      
    }

    #endregion

    #region Update

    public void Update(ulong seconds)
    {
      GalacticTime += seconds;
      Date.AddSeconds(seconds);

      foreach (var star in Systems) star.Update(GalacticTime);
    }
    
    #endregion

    internal ulong Advance(TimeBy amount)
    {
      if (IsGenerated == false) return 0; 

      return InvokePulse[amount]();
    }

    private ulong Pulse(Func<DateTime, DateTime> pulse)
    {
      var startDate = Date;
      var pulseDate = pulse(Date);

      while (pulseDate.Day > 29)
        pulseDate = pulseDate.AddDays(1);

      return Convert.ToUInt64((pulseDate - startDate).TotalSeconds);
    }
  }
}
