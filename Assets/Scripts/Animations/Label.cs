using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class Label : MonoBehaviour
{
  public Vector3 Offset;

  private TMP_Text label;
  // Start is called before the first frame update
  private void Start()
  {
    var canvas = this.GetComponentInChildren<Canvas>();
    canvas.name = string.Format("{0} Label", this.name);

    label = canvas.GetComponentInChildren<TMP_Text>();
    label.text = this.name;
  }

  private void Update()
  {
    
    label.transform.position = Camera.main.WorldToScreenPoint(this.transform.position) + Offset;
  }

}
