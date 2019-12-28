using UnityEngine;

public class MouseHandler : MonoBehaviour
{
  void Update()
  {

    // Get mouse scrollwheel forwards || check if main camera is too small, prevents a couple errors
    if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 1)
    {
      // Scroll camera inwards
      Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize -= Camera.main.orthographicSize / 5, 20f, 100000f);
    }

    // Get mouse scrollwheel backwards || optional code
    if (Input.GetAxis("Mouse ScrollWheel") < 0 /*Optional |: && Camera.main.orthographicSize < maxScroll :| */)
    {
      // Scrolling Backwards
      Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize += Camera.main.orthographicSize / 5, 20f, 100000f);
    }

    var movement = new Vector3(0, 0, 0);
    var movementSpeed = Camera.main.orthographicSize / 10;

    //Move the GameObject
    if (Input.GetKey("a"))
    {
      movement.x -= movementSpeed;
    }
    if (Input.GetKey("s"))
    {
      movement.y -= movementSpeed;

    }
    if (Input.GetKey("d"))
    {
      movement.x += movementSpeed;
    }
    if (Input.GetKey("w"))
    {
      movement.y += movementSpeed;
    }

    //Debug.Log(movement);

    Camera.main.transform.position += movement;
  }
}
