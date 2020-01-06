using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class Label : MonoBehaviour
{
  public int offset = 5;

  private TMP_Text label;
  // Start is called before the first frame update
  private void Start()
  {
    Init();
  }

  private void Init()
  {
    if ((label != null && label.text == this.name)) return;
    var canvas = this.GetComponentInChildren<Canvas>();
    canvas.name = string.Format("{0} Label", this.name);

    label = canvas.GetComponentInChildren<TMP_Text>();
    label.text = this.name;

    var offset = this.transform.localScale.z;
  }

  private void LateUpdate()
  {
    Init();

    label.transform.position = (Camera.main.WorldToScreenPoint(this.transform.position) + new Vector3(0,offset,0));
  }

}
