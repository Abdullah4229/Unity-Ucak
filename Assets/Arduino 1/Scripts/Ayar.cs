using System.Net.Mime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Ayar : MonoBehaviour
{
    public GameObject ayar;
    public Joystick js;
    public Text Com;
    public Slider ayar_hiz;
    public Slider ayar_don;

    void Start()
    {
        ayar.SetActive(false);
    }

    public void ayarlar()
    {
        ayar.SetActive(true);
    }
    public void tamam()
    {
        js.COM = Com.text;
        js.hareket_ks = ayar_hiz.value * 0.0001f;
        js.donme_ks = ayar_don.value * 0.0001f;
        ayar.SetActive(false);
    }
}