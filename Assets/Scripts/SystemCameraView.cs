using System.Collections;
using UnityEngine;

public class SystemCameraView : MonoBehaviour
{
  [Range(1, 10)]
  public float ScrollRate = 1;

  [Range(0.1f, 100)]
  public float PlanetScale = 5f;

  public float transitionDuration = 5f;

  public new Camera camera { get; private set; }

  public int CameraSteps = 20;
  public int MinCameraSize = 5;
  public int MaxCameraSize;
  private Vector3 DefaultPosition;
  private float DefaultSize;
  private int[] Steps;
  public int stepIndex;
  private Transform followTarget;

  public float CamScale {
    get {
      if (stepIndex + 1 == Steps.Length) return 0;
      else if (stepIndex > 0)
        return 1 - ((stepIndex + 1f) / Steps.Length);
      else return 1;
    }
  }


  private void Awake()
  {
    Steps = new int[CameraSteps];
    for (int i = 0; i < CameraSteps; i++)
    {
      Steps[i] = (int)(Mathf.Pow(2f, i) * MinCameraSize);
      MaxCameraSize = Steps[i];
    }

    camera = Camera.main;
    DefaultPosition = camera.transform.localPosition;
    camera.orthographicSize = DefaultSize = MaxCameraSize;
    stepIndex = CameraSteps - 1;
  }

  void Update()
  {

    if (Input.GetAxis("Mouse ScrollWheel") > 0 && stepIndex > 0) // zoom in
    {
      camera.orthographicSize = Steps[--stepIndex];
      followTarget = null;
    }
    if (Input.GetAxis("Mouse ScrollWheel") < 0 && stepIndex < Steps.Length - 1) // zoom out
    {
      camera.orthographicSize = Steps[++stepIndex];
      followTarget = null;
    }
    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
    {
      transform.Translate(new Vector3(camera.orthographicSize * Time.deltaTime * ScrollRate, 0, 0));
      followTarget = null;
    }
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    {
      transform.Translate(new Vector3(-camera.orthographicSize * Time.deltaTime * ScrollRate, 0, 0));
      followTarget = null;
    }
    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    {
      transform.Translate(new Vector3(0, -camera.orthographicSize * Time.deltaTime * ScrollRate, 0));
      followTarget = null;
    }
    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    {
      transform.Translate(new Vector3(0, camera.orthographicSize * Time.deltaTime * ScrollRate, 0));
      followTarget = null;
    }
    if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse4))
    {
      StartCoroutine(Transition());
      followTarget = null;
    }
    if(followTarget != null)
    {
      var followPoint = followTarget.position;
      followPoint.z = transform.position.z;
      this.transform.position = Vector3.Lerp(transform.position, followPoint, Time.deltaTime * 2);
    }

    if (Input.GetMouseButtonDown(0))
    {
      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out var hit) && hit.collider != null && hit.collider.gameObject.name == "Body")
      {
        followTarget = hit.collider.gameObject.transform;
      }
    }

  }

  IEnumerator Transition()
  {
    float t = 0.0f;
    Vector3 startingPos = transform.position;
    while (t < 1.0f)
    {
      t += Time.deltaTime * (Time.timeScale / transitionDuration);
      transform.position = Vector3.Lerp(startingPos, DefaultPosition, t);
      camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, DefaultSize, t * 0.005f);
      yield return 0;
    }
    transform.position = DefaultPosition;
    camera.orthographicSize = DefaultSize;
  }
}
