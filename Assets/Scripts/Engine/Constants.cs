namespace Sailfin
{
  public static class Gravity
  {
    public const float Constant = 6.67e-11f;
  }

  public enum OrbitalType
  {
    Star      = 4,
    Planet    = 3,
    Moon      = 2,
    Asteroid  = 1,
    Comet     = 0
  }
}