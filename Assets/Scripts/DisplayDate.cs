using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDate : MonoBehaviour
{
  public GalaxyController GalaxyController;
  private DateTime GalacticDate => GalaxyController.Galaxy.Date;
  private Text Label;

  // Start is called before the first frame update
  void Start()
  {
    Label = this.GetComponent<Text>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    Label.text = GalacticDate.ToString("MM/dd/yyyy HH:mm.ss");
  }
}
