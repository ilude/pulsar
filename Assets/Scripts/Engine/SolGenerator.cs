using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using UnityEngine;

namespace Sailfin
{
  public class SolGenerator : ISystemGenerator
  {
    public void CreateSystemsIn(Galaxy galaxy)
    {
      //var sol = new Star(Star.DefaultConfig);
      //galaxy.Systems.Add(sol);

      Star star = null;
      Dictionary<string, Orbital> systemMap = new Dictionary<string, Orbital>();
      using (var reader = new StreamReader("Assets\\Data\\sol.csv"))
      using (var csv = new CsvReader(reader))
      {
        csv.Configuration.RegisterClassMap<OrbitalConfigMap>();
        csv.Read();
        csv.ReadHeader();
        while (csv.Read())
        {
          var config = csv.GetRecord<OrbitalConfig>();
          if (star == null)
          {
            star = new Star(config);
            systemMap[star.Name] = star;
          }
          else
          {
            systemMap[config.Name] = new Orbital(config);
          }
        }
      }

      int count = 0;

      using (var reader = new StreamReader("Assets\\Data\\Asteroids.csv"))
      using (var csv = new CsvReader(reader))
      {
        csv.Configuration.RegisterClassMap<OrbitalConfigMap>();
        csv.Read();
        csv.ReadHeader();
        while (csv.Read() && count < 1000)
        {
          try
          {
            count++;
            var config = csv.GetRecord<OrbitalConfig>();
            if(!string.IsNullOrEmpty(config.Name) || config.Distance > 0)
              systemMap[config.Parent].AddChild(new Orbital(config));


          }
          catch(Exception ex)
          {
            Debug.Log("Error Parsing CSV record!\n"+ ex.StackTrace);
          }
          
        }
      }


      foreach (var body in systemMap.Values)
      {
        try { 
          if (body.Equals(star)) continue;
          systemMap[body.Config.Parent].AddChild(body);
        }
         catch (Exception ex)
        {
          Debug.Log(ex.Data.Values);
          //ex.Data.Values has more info...
        }
    }
      galaxy.Systems.Add(star);

      //sol.AddChild(new Orbital(new OrbitalConfig("Mercury") { Mass = 3.302e23f, Distance = 57909175000 }));
      //sol.AddChild(new Orbital(new OrbitalConfig("Venus") { Mass = 4.8690e24f, Distance = 108208930000 }));
      //sol.AddChild(new Orbital(new OrbitalConfig("Earth") { Mass = 5.972e24f, Distance = 149597890000 }))
      //  .AddChild(new Orbital(new OrbitalConfig("Luna") { Mass = 7.3476e22f, Distance = 384400000, Type = OrbitalType.Moon }));
      //sol.AddChild(new Orbital(new OrbitalConfig("Mars") { Mass = 6.4191e23f, Distance = 227936640000 }));
      //sol.AddChild(new Orbital(new OrbitalConfig("Jupiter") { Mass = 1.8987e27f, Distance = 778412010000 }));
      //sol.AddChild(new Orbital(new OrbitalConfig("Saturn") { Mass = 5.6851e26f, Distance = 1426725400000 }));
      //sol.AddChild(new Orbital(new OrbitalConfig("Uranus") { Mass = 8.6849e25f, Distance = 2870972200000 }));
      //sol.AddChild(new Orbital(new OrbitalConfig("Neptune") { Mass = 1.0244e26f, Distance = 4498252900000 }));

      //DumpStarToCsv(sol, "Assets\\Data\\sol.csv");
    }

    private static void DumpStarToCsv(Star sol, string path)
    {
      using (var writer = new StreamWriter(path))
      using (var csv = new CsvWriter(writer))
      {
        csv.Configuration.RegisterClassMap<OrbitalConfigMap>();
        csv.WriteHeader<OrbitalConfig>();
        csv.NextRecord();
        WriteOrbital(csv, sol);
      }
    }

    private static void WriteOrbital(CsvWriter csv, Orbital body)
    {
      csv.WriteRecord(body.Config);
      csv.NextRecord();
      foreach (Orbital child in body.Children)
      {
        if(string.IsNullOrEmpty(child.Config.Parent))
        {
          child.Config.Parent = body.Name;
        }
        WriteOrbital(csv, child);
      }
    }
  }
}