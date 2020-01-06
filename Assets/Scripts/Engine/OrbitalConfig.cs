using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace Sailfin
{
  public class OrbitalConfig
  {
    public OrbitalConfig()
    {

    }

    public OrbitalConfig(string name)
    {
      Name = name;
    }

    public string Parent = null;
    public string Name = null;
    public float Mass = 0f;
    public ulong Distance = 0;
    public OrbitalAngle Angle = OrbitalAngle.Zero;
    public OrbitalType Type = OrbitalType.Planet;
  }

  public class OrbitalAngle
  {
    public static readonly float Zero = new OrbitalAngle(0f);
    public static readonly float Random = new OrbitalAngle(-1f);

    private float _angle;

    private OrbitalAngle(float angle)
    {
      _angle = angle;
    }

    public override string ToString()
    {
      return _angle.ToString();
    }

    public static OrbitalAngle Set(float angle)
    {
      return new OrbitalAngle(angle);
    }

    public static implicit operator float(OrbitalAngle oangle)
    {
      return oangle._angle;
    }

    public static implicit operator OrbitalAngle(float angle)
    {
      return new OrbitalAngle(angle);
    }
  }
  
  public class OrbitalConfigMap : ClassMap<OrbitalConfig>
  {
    public OrbitalConfigMap()
    {
      Map(m => m.Parent);
      Map(m => m.Name);
      Map(m => m.Mass);
      Map(m => m.Distance);
      Map(m => m.Type);
      Map(m => m.Angle).TypeConverter<OrbitalAngleConverter<OrbitalAngle>>();
    }
  }

  public class OrbitalAngleConverter<T> : DefaultTypeConverter
  {
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
      return OrbitalAngle.Set(Convert.ToSingle(text));
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
      return value.ToString();
    }
  }

}
