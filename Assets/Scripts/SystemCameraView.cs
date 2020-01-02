using System.Collections;
using UnityEngine;

public class SystemCameraView : MonoBehaviour
{
  [Range(1, 10)]
  public float ScrollRate = 1;

  public new Camera camera { get; private set; }

  public float MinCameraSize = 5f;
  public int CameraSteps = 20;
  //public float CamScale => (1 - (camera.orthographicSize / (MaxCameraDistance - MinCameraDistance)));
  public float transitionDuration = 5f;
  

  private Vector3 DefaultPosition;
  private float DefaultSize;
  private int[] Steps;
  [Range(0.1f, 100)]
  public float PlanetScale = 5f;

  private void Awake()
  {
    camera = Camera.main;
    DefaultPosition = camera.transform.localPosition;
    DefaultSize = camera.orthographicSize;

    Steps = new int[CameraSteps];
    for (int i = 0; i < CameraSteps; i++)
    {
      Steps[i] = (int)(Mathf.Pow(2f, i) * MinCameraSize);
    }
  }

  void Update()
  {

    if (Input.GetAxis("Mouse ScrollWheel") > 0) // zoom in
    {
      camera.orthographicSize = Mathf.Clamp(camera.orthographicSize / 2, MinCameraDistance, MaxCameraDistance);
    }
    if (Input.GetAxis("Mouse ScrollWheel") < 0) // zoom out
    {
      camera.orthographicSize = Mathf.Clamp(camera.orthographicSize * 2, MinCameraDistance, MaxCameraDistance);
    }
    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
    {
      transform.Translate(new Vector3(camera.orthographicSize * Time.deltaTime * ScrollRate, 0, 0));
    }
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    {
      transform.Translate(new Vector3(-camera.orthographicSize * Time.deltaTime * ScrollRate, 0, 0));
    }
    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    {
      transform.Translate(new Vector3(0, -camera.orthographicSize * Time.deltaTime * ScrollRate, 0));
    }
    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
    {
      transform.Translate(new Vector3(0, camera.orthographicSize * Time.deltaTime * ScrollRate, 0));
    }
    if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse4))
    {
      StartCoroutine(Transition());
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
