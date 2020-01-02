using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbView : MonoBehaviour
{
  public float Radius = 1;
  public float _distance;
  public float Distance => _distance / 1000 / 6000;
  public float Mass = 1;
  public float RelativeSize = 1;
  public float Orbit => transform.position.x;
  public Material LineMaterial;
  public float lineWidth = 5;

  private Vector3 scale = Vector3.one;
  private OrbView parent;
  private Camera cam;
  private SystemCameraView camView;

  private float BodySize => parent.BodySize; //cam.orthographicSize/ camView.PlanetScale;
  private Transform body;

  // Start is called before the first frame update
  void Start()
  {
    cam = Camera.main;
    camView = cam.GetComponent<SystemCameraView>();
    parent = transform.parent.GetComponentInParent<OrbView>();
    body = transform.GetChild(0);

    
  }


  public void DrawCircle(OrbView child)
  {
    var segments = 360 * 4;
    LineRenderer line;
    if(this.gameObject.TryGetComponent<LineRenderer>(out line) == false)
    {
      line = this.gameObject.AddComponent<LineRenderer>();
    }
    if (line == null) return;
    line.useWorldSpace = false;
    line.startWidth = lineWidth;
    line.endWidth = lineWidth;
    line.positionCount = segments + 1;
    line.material = LineMaterial;

    var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
    var points = new Vector3[pointCount];

    for (int i = 0; i < pointCount; i++)
    {
      var rad = Mathf.Deg2Rad * (i * 360f / segments);
      points[i] = new Vector3(Mathf.Sin(rad) * child.Distance, -Mathf.Cos(rad) * child.Distance, 0);
    }

    line.SetPositions(points);
  }

  // Update is called once per frame
  void Update()
  {
    var children = this.GetComponentsInChildren<OrbView>().Where(x => !x.Equals(this));
    foreach (var child in children)
    {
      DrawCircle(child);
    }

    body.localScale = Vector3.one * RelativeSize; /// BodySize;

    if (parent == null) return;

    //var s1 = (39.4784176f * Mathf.Pow(Distance, 3f));
    //var s2 = Sailfin.Gravity.Constant * (this.Mass + parent.Mass);
    //var s3 = s1 / s2;
    //var TimeToOrbit = Mathf.Sqrt(s3);

    //// Advance our angle by the correct amount of time.
    ////float offsetAngle = (Time.time / TimeToOrbit) * 2f * Mathf.PI;
    //float offset1 = (Time.time / TimeToOrbit) * 2f * Mathf.PI;
    //float offsetAngle = Time.time * (2f * Mathf.PI / TimeToOrbit * 86400f * 5);

    //float distance = parentRadius + Distance + (Size / 2f);
    //_angle += speed * Time.deltaTime;

    var InitAngle = 0f;
    var offsetAngle = 0f;
    //var distance = (parent.Radius + this.Radius + Distance) / 6000;
    

    this.transform.position = new Vector3(
      Distance * Mathf.Cos(InitAngle + offsetAngle),
      Distance * Mathf.Sin(InitAngle + offsetAngle), 0);

   // Debug.LogFormat("{0} => {1}", this.name, this.transform.position);
  }
}
