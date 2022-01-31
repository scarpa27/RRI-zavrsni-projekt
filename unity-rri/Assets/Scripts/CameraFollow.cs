using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float camMoveSpeed = 20.0f;
    public float rotationSpeed = 30.0f;
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string rotLeft = "q";
    public string rotRight = "e";
    public bool panScrolling;
    public float maxCamH = 10.0f;
    private Vector3 _centar;
    private float _edgeScrollSpeed;
    private float _facing;
    private bool _follow = true;
    private float _mouseEnd;
    private float _mouseStart;

    private Vector3 _offset;


    private void Start()
    {
        _offset = new Vector3(0, 15, -5);
        _centar = player.transform.position;

        transform.position = _centar + _offset;
        transform.LookAt(_centar);

        _facing = Mathf.Deg2Rad * transform.eulerAngles.y;
        _edgeScrollSpeed = camMoveSpeed;
    }

    private void LateUpdate()
    {
        _centar = _follow ? player.transform.position : _centar;
        _facing = Mathf.Deg2Rad * transform.eulerAngles.y;


        if (Input.GetKey(KeyCode.LeftShift)) _edgeScrollSpeed = 2.5f * camMoveSpeed;
        else _edgeScrollSpeed = camMoveSpeed;


        _offset += ScrollZoom();
        _centar += CameraMove();


        var h = Mathf.Clamp(_offset.y / 3, 2, maxCamH);
        _offset = new Vector3(-h * Mathf.Sin(_facing), h * 3, -h * Mathf.Cos(_facing));


        MouseRotate();

        if (Input.GetKey(rotLeft)) RotirajKameruOkoCentra(-rotationSpeed * Time.deltaTime);

        if (Input.GetKey(rotRight)) RotirajKameruOkoCentra(rotationSpeed * Time.deltaTime);


        if (Input.GetKey("space")) _follow = true;


        transform.position = _centar + _offset;
    }


    private Vector3 ScrollZoom()
    {
        return new Vector3(
            0,
            -0.6f * Input.mouseScrollDelta.y,
            0);
    }

    private Vector3 CameraMove()
    {
        var ret = new Vector3(0, 0, 0);

        if (Input.GetKey(keyUp) || panScrolling && Input.mousePosition.y >= 0.96 * Screen.height)
        {
            _follow = false;
            ret.z += _edgeScrollSpeed * Time.deltaTime * Mathf.Cos(_facing);
            ret.x += _edgeScrollSpeed * Time.deltaTime * Mathf.Sin(_facing);
        }

        if (Input.GetKey(keyDown) || panScrolling && Input.mousePosition.y <= 0.04 * Screen.height)
        {
            _follow = false;
            ret.z -= _edgeScrollSpeed * Time.deltaTime * Mathf.Cos(_facing);
            ret.x -= _edgeScrollSpeed * Time.deltaTime * Mathf.Sin(_facing);
        }

        if (Input.GetKey(keyLeft) || panScrolling && Input.mousePosition.x <= 0.02 * Screen.width)
        {
            _follow = false;
            ret.x -= _edgeScrollSpeed * Time.deltaTime * Mathf.Cos(_facing);
            ret.z += _edgeScrollSpeed * Time.deltaTime * Mathf.Sin(_facing);
        }

        if (Input.GetKey(keyRight) || panScrolling && Input.mousePosition.x >= 0.98 * Screen.width)
        {
            _follow = false;
            ret.x += _edgeScrollSpeed * Time.deltaTime * Mathf.Cos(_facing);
            ret.z -= _edgeScrollSpeed * Time.deltaTime * Mathf.Sin(_facing);
        }

        return ret;
    }

    private void MouseRotate()
    {
        if (Input.GetMouseButtonDown(2)) _mouseStart = Input.mousePosition.x;

        if (Input.GetMouseButton(2))
        {
            _mouseEnd = Input.mousePosition.x;
            RotirajKameruOkoCentra((_mouseStart - _mouseEnd) * Time.deltaTime * rotationSpeed / 2);
            _mouseStart = _mouseEnd;
        }
    }

    private void RotirajKameruOkoCentra(float kut)
    {
        transform.RotateAround(_centar, Vector3.up, kut);
        // transform.LookAt(_centar);
    }
}