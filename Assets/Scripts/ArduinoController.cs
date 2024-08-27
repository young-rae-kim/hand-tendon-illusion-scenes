using UnityEngine;
using System.IO.Ports;
using System.Collections;
using System;
using TMPro;

public class ArduinoController : MonoBehaviour
{   
    [SerializeField]
    private string portName = "COM3";

    [SerializeField]
    private int baudrate = 115200;

    public TextMeshProUGUI vibrationText;
    public bool Vibration
    {
        get { return vibration; }
        set
        {
            vibration = value;
            vibrationText.text = "Vibration: " + vibration;
        }
    }

    public bool usingArduino = false;
    private bool vibration = false;
    private SerialPort port;
    private readonly int bufferBytes = 3;
    private byte[] buffer;


    // Start is called before the first frame update
    void Start()
    {
        if (usingArduino)
        {
            port = new SerialPort(portName, baudrate);
            // StartCoroutine(AsyncVibrateForSeconds(false, 5.0f));
        }

        vibrationText.text = "Vibration: " + vibration;
    }

    public void Vibrate(bool extension = true) 
    {
        if (!Vibration)
        {
            Vibration = true;
            Debug.Log("Vibration playing");
            buffer = new byte[3] { (byte) (extension ? 1 : 2), 1, 0 };
            SendData();
        }
    }

    public void Stop(bool extension = true)
    {
        if (Vibration) 
        {
            buffer = new byte[3] { (byte) (extension ? 1 : 2), 0, 0 };
            SendData();
            Debug.Log("Vibration stopped");
            Vibration = false;
        }
    }

    private void SendData()
    {
        if (usingArduino)
        {
            if (!port.IsOpen) { port.Open(); }

            port.Write(buffer, 0, bufferBytes);
            port.Close();
        }
    }
    public IEnumerator VibrateForSeconds(bool extension = true, float seconds = 2.0f)
    {
        Vibrate(extension);
        yield return new WaitForSeconds(seconds);
        Stop(extension);
    }

    public IEnumerator AsyncVibrateForSeconds(bool extension = true, float seconds = 2.0f)
    {
        if (!Vibration) 
        {
            Vibration = true;
            Debug.Log("Vibration playing");

            int quarterSeconds = ((int) Math.Floor(seconds * 1000)) / 250;
            buffer = new byte[3] { (byte) (extension ? 1 : 2), 2, (byte) quarterSeconds };
            SendData();

            yield return new WaitForSeconds(seconds);
            Debug.Log("Vibration stopped");
            Vibration = false;
        }
    }
}
