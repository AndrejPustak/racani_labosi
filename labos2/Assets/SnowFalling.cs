using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFalling : MonoBehaviour
{
    Texture2D otexture;
    Texture2D texture;
    List<GameObject> particles = new List<GameObject>();
    GameObject particle;
    Camera mainCamera;
    int counter = 0;
    int freq = 15;
    int maxSize = 75;
    float speed = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        otexture = Resources.Load("snow") as Texture2D;

        mainCamera = Camera.main;

    }

    private void randomizeCOlor()
    {

        texture = new Texture2D(otexture.width, otexture.height);

        Color32[] pixels = otexture.GetPixels32();
        Color32[] npixels = new Color32[pixels.Length];
        float r = Random.Range(0f, 3f);
        for (int i = 0; i < pixels.Length; i++)
        {

            npixels[i] = new Color32(pixels[i].r, pixels[i].g, pixels[i].b, pixels[i].a);
            if(r < 1)
            {
                npixels[i].g = 0;
                npixels[i].b = 0;
                npixels[i].a = 255;
            } else if (r >= 1 && r < 2)
            {
                npixels[i].r = 0;
                npixels[i].b = 0;
                npixels[i].a = 255;
            } else
            {
                npixels[i].r = 0;
                npixels[i].g = 0;
                npixels[i].a = 255;
            }


            
        }

        texture.SetPixels32(npixels);
        texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particle = particles[i];
            if (particle != null)
            {
                particle.GetComponent<Renderer>().material.mainTexture = texture;
                particle.transform.Translate(0.0f, 0.0f, speed, Space.Self);
                //particle.transform.forward = mainCamera.transform.forward;
                //particle.transform.LookAt(mainCamera.transform)
                //particle.transform.Rotate(90, 0, 0, Space.Self);
                float scale = 0.0001f;
                particle.transform.localScale += new Vector3(-scale, -scale, -scale);
            }
        }

        counter++;
        if (counter == freq)
        {
            particles.Add(newParticle());
            if (particles.Count == 50)
            {
                GameObject p = particles[0];
                particles.RemoveAt(0);
                Destroy(p);
            }

            counter = 0;
        }
    }

    private GameObject newParticle()
    {
        particle = GameObject.CreatePrimitive(PrimitiveType.Plane);
        particle.name = "Particle";
        particle.transform.parent = this.gameObject.transform;

        float x = Random.Range(-5, 5);
        float z = Random.Range(-5, 5);

        particle.transform.Translate(x, 0.0f, z, Space.Self);

        //particle.transform.forward = mainCamera.transform.forward;
        //particle.transform.Rotate(90, 0, 0, Space.Self);

        
        //particle.GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Particles/Additive");
        particle.GetComponent<Renderer>().material = new Material(Shader.Find("Mobile/Particles/Additive"));
        particle.GetComponent<Renderer>().material = Instantiate<Material>(particle.GetComponent<Renderer>().material);
        randomizeCOlor();
        particle.GetComponent<Renderer>().material.mainTexture = texture;
        //float scale = Random.Range(0f, 0.2f);
        float scale = 0.07f;
        particle.transform.localScale += new Vector3(-1f + scale, -1f + scale, -1f + scale);

        return particle;
    }

}
