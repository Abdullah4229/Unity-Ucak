using System;
using UnityEngine;
using UnityEngine.UI;
//using System.IO.Ports;


/* Arduino bağlantsının çalışması için---->
Project Settings > Player > Other Settings > Api Compatibility Level > .NET 4.x seç*/

public class Joystick : MonoBehaviour
{
    RaycastHit nesne;
    RaycastHit mesafe_ray;
    public GameObject test_data;
    public Rigidbody rb;
    private Vector3 harek;
    int vuruldu;
    public float hareket_ks;
    public float donme_ks;
    public bool Vurus;
    public string js_deger;

    public Text Gaz;
    public Text Hata;
    public Text Mesafe_txt;

    //SerialPort data_stream;
    public string[] datas;
    public string COM;
    public int Band;
    public float sagY;
    public float sagX;
    public float solY;
    public float solX;
    public float mavi;
    public float ates;
    
    void Start()
    {/*
        if(Band == 0)
        {
            Hata.text = "COM ve Band Girilmedi";
        }
        else{
            Hata.text = "Uçuş serbest";
        }
        data_stream = new SerialPort(COM, Band); // (bağlantıyı arduinodan al gir, arduino da belirlediğin bandı seç)

        data_stream.Open();*/
    }

    void Update()
    {/*
        //--------Arduinodan verilerin alınıp okunması
        js_deger = data_stream.ReadLine();

        string[] datas = js_deger.Split(',');
        sagY = float.Parse(datas[0]);
        sagX = float.Parse(datas[1]);
        solY = float.Parse(datas[2]);
        solX = float.Parse(datas[3]);
        mavi = float.Parse(datas[4]);
        ates = float.Parse(datas[5]);

        //--------Uçak hareket ettirme
        harek = new Vector3(0f, 0f, solY + 2) * Time.deltaTime * hareket_ks;
        Gaz.text = "% " + solY * 0.25f;
        rb.MovePosition(transform.position + transform.TransformDirection(harek));

        transform.Rotate(sagY * Time.deltaTime * -donme_ks, sagX * Time.deltaTime * -donme_ks, sagX * Time.deltaTime * donme_ks);
        transform.Rotate(0f, 0f, solX * Time.deltaTime * donme_ks);
        
        if(Vurus)
        {
            if(ates == 1)
            {
                if(Physics.Raycast(transform.position,transform.forward,out nesne,1000.0f))
                {
                    if(nesne.collider.gameObject.tag=="Enemy")
                    {
                        //ateşSes.Play();
                        vuruldu+=1;
                        Debug.Log(vuruldu);
                        
                        if(vuruldu >= 5)
                        {
                            Destroy(nesne.collider.gameObject);
                        }
                    }else
                    {
                        Debug.LogWarning("Yetişmedi veya çarpmadı");
                        mesafe();
                    }
                }else
                {
                    Debug.LogWarning("Ateş etmedi !!!");
                }
            }
        }
/*
        float eksenZ = transform.rotation.z;
        if(eksenZ <= -0.05 && sagX == 0 && solX == 0)
        {
            transform.Rotate(0f, 0f, solY * Time.deltaTime * donme_ks);
            Debug.Log(eksenZ);
        }
        else if(eksenZ >= 0.05 && sagX == 0 && solX == 0)
        {
            transform.Rotate(0f, 0f, solY * Time.deltaTime * -donme_ks);
            Debug.Log(eksenZ);
        }
        else
        {
            Debug.Log(eksenZ);
        }*/

        //Debug.Log("Sag Y : " + datas[0] + " Sag X : " + datas[1] + " Sol Y : " + datas[2] + " Sol X : " + datas[3] + "  : " + datas[4] + " Bt 5 : " + datas[5]);
    
        WASD_Hareket();
    
    }
    
    public void mesafe()
    {
        if(Physics.Raycast(transform.position,transform.forward,out mesafe_ray,1000.0f))
        {
            Vector3 fark = mesafe_ray.collider.gameObject.transform.position - transform.position;
            Mesafe_txt.text = "Mesaf : " + fark;
        }
    }

    // Yeni eklenen klavye fonksiyonu
    public void WASD_Hareket()
    {
        // W ve S tuşları (Arduino'daki Y eksenleri gibi düşünüldü)
        float klavye_WS = Input.GetAxis("Vertical"); 
        
        // A ve D tuşları (Arduino'daki X eksenleri gibi düşünüldü)
        float klavye_AD = Input.GetAxis("Horizontal"); 

        // Boşluk tuşu (Arduino'daki ates == 1 durumu)
        bool klavye_Ates = Input.GetKeyDown(KeyCode.Space);

        //--------Klavye ile Uçak hareket ettirme
        Vector3 klavye_harek = new Vector3(0f, 0f, klavye_WS + 2) * Time.deltaTime * hareket_ks;
        Gaz.text = "% " + klavye_WS * 0.25f;
        rb.MovePosition(transform.position + transform.TransformDirection(klavye_harek));

        // Dönme İşlemi (Orijinal koddaki dönüş mantığı WASD'ye uyarlandı)
        transform.Rotate(klavye_WS * Time.deltaTime * -donme_ks, klavye_AD * Time.deltaTime * -donme_ks, klavye_AD * Time.deltaTime * donme_ks);
        
        // Ateş Etme İşlemi
        if(Vurus)
        {
            if(klavye_Ates)
            {
                if(Physics.Raycast(transform.position,transform.forward,out nesne,1000.0f))
                {
                    if(nesne.collider.gameObject.tag=="Enemy")
                    {
                        vuruldu+=1;
                        Debug.Log("Klavyeden Vuruldu: " + vuruldu);
                        
                        if(vuruldu >= 5)
                        {
                            Destroy(nesne.collider.gameObject);
                        }
                    }else
                    {
                        Debug.LogWarning("Yetişmedi veya çarpmadı");
                        mesafe();
                    }
                }else
                {
                    Debug.LogWarning("Ateş etmedi !!!");
                }
            }
        }
    }
}