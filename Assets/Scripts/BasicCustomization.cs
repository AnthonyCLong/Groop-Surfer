using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class BasicCustomization : MonoBehaviour
{
    // struct coord
    // {
    //     public int x;
    //     public int y;
    //     public coord(int inx, int iny){x=inx; y=iny;}
    // }
    void Start()
    {
        
    }

    public void changeHair()
    {
        Color hairColor = transform.GetChild(0).gameObject.GetComponent<Image>().color;
    
        Texture2D texture = Resources.Load<Texture2D>("Player Components/Textures/Head"); 
        Color previous = texture.GetPixel(13,10);
       
        for(int x = 0; x< texture.width; x++)
        {
            for(int y = 0; y< texture.height; y++)
                {
                    if (texture.GetPixel(x,y) == previous)
                    {
                    //Debug.Log(x + " ,"+y);
                    texture.SetPixel(x, y, hairColor);
                    }
                }
        }
        texture.Apply();
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Head.png", bytes);

    }

    public void changeSkin()
    {
        Color skinColor = transform.GetChild(0).gameObject.GetComponent<Image>().color;
    
        Texture2D headTexture = Resources.Load<Texture2D>("Player Components/Textures/Head");
        Texture2D leftArmTexture = Resources.Load<Texture2D>("Player Components/Textures/Left Arm"); 
        Texture2D rightArmTexture = Resources.Load<Texture2D>("Player Components/Textures/Right Arm");
        Texture2D torsoTexture = Resources.Load<Texture2D>("Player Components/Textures/Torso");
        Color previous = headTexture.GetPixel(2,2);
       
        //setting head
        for(int x = 0; x< 117; x++)
        {
            for(int y = 0; y< headTexture.height; y++)
                {
                    if (headTexture.GetPixel(x,y) == previous)
                    {
                    //Debug.Log(x + " ,"+y);
                    headTexture.SetPixel(x, y, skinColor);
                    }
                }
        }
        headTexture.Apply();
        byte[] bytes = headTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Head.png", bytes);

        //setting left arm
        for(int x = 0; x< leftArmTexture.width; x++)
        {
            for(int y = 0; y< leftArmTexture.height; y++)
                {
                    if (leftArmTexture.GetPixel(x,y) == previous)
                    {
                    //Debug.Log(x + " ,"+y);
                    leftArmTexture.SetPixel(x, y, skinColor);
                    }
                }
        }
        leftArmTexture.Apply();
        bytes = leftArmTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Left Arm.png", bytes);

        //setting right arm
        for(int x = 0; x< rightArmTexture.width; x++)
        {
            for(int y = 0; y< rightArmTexture.height; y++)
                {
                    if (rightArmTexture.GetPixel(x,y) == previous)
                    {
                    //Debug.Log(x + " ,"+y);
                    rightArmTexture.SetPixel(x, y, skinColor);
                    }
                }
        }
        rightArmTexture.Apply();
        bytes = rightArmTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Right Arm.png", bytes);

        //setting torso
        for(int x = 113; x< 117; x++)
        {
            for(int y = 1; y< 6; y++)
                {
                    if (torsoTexture.GetPixel(x,y) == previous)
                    {
                    //Debug.Log(x + " ,"+y);
                    torsoTexture.SetPixel(x, y, skinColor);
                    }
                }
        }
        torsoTexture.Apply();
        bytes = torsoTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Torso.png", bytes);
    }

    public void changeEyes()
    {
        Color eyeColor = transform.GetChild(0).gameObject.GetComponent<Image>().color;

        Texture2D texture = Resources.Load<Texture2D>("Player Components/Textures/Head");
        texture.SetPixel(51, 0, eyeColor);
        texture.SetPixel(56, 0, eyeColor);
        
        texture.SetPixel(63, 0, eyeColor);
        texture.SetPixel(68, 0, eyeColor);

        texture.Apply();
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Head.png", bytes);
    }
    public void changeShirt()
    {
        Color skinColor = transform.GetChild(0).gameObject.GetComponent<Image>().color;
        
            Texture2D leftArmTexture = Resources.Load<Texture2D>("Player Components/Textures/Left Arm"); 
            Texture2D rightArmTexture = Resources.Load<Texture2D>("Player Components/Textures/Right Arm");
            Texture2D torsoTexture = Resources.Load<Texture2D>("Player Components/Textures/Torso");
            Color previous = torsoTexture.GetPixel(50,1);
        
            //setting torso
            for(int x = 50; x< torsoTexture.width; x++)
            {
                for(int y = 0; y< torsoTexture.height; y++)
                    {
                        if (torsoTexture.GetPixel(x,y) == previous)
                        {
                        //Debug.Log(x + " ,"+y);
                        torsoTexture.SetPixel(x, y, skinColor);
                        }
                    }
            }
            torsoTexture.Apply();
            byte[] bytes = torsoTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Torso.png", bytes);

            //setting left arm
            for(int x = 0; x< leftArmTexture.width; x++)
            {
                for(int y = 0; y< leftArmTexture.height; y++)
                    {
                        if (leftArmTexture.GetPixel(x,y) == previous)
                        {
                        //Debug.Log(x + " ,"+y);
                        leftArmTexture.SetPixel(x, y, skinColor);
                        }
                    }
            }
            leftArmTexture.Apply();
            bytes = leftArmTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Left Arm.png", bytes);
            
            //setting right arm
            for(int x = 0; x< rightArmTexture.width; x++)
            {
                for(int y = 0; y< rightArmTexture.height; y++)
                    {
                        if (rightArmTexture.GetPixel(x,y) == previous)
                        {
                        //Debug.Log(x + " ,"+y);
                        rightArmTexture.SetPixel(x, y, skinColor);
                        }
                    }
            }
            rightArmTexture.Apply();
            bytes = leftArmTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Right Arm.png", bytes);
    }  

    public void changePants()
    {
        Color pantsColor = transform.GetChild(0).gameObject.GetComponent<Image>().color;
        
            Texture2D torsoTexture = Resources.Load<Texture2D>("Player Components/Textures/Torso");
            Color previous = torsoTexture.GetPixel(1,2);
        
            //setting torso
            for(int x = 0; x< 40; x++)
            {
                for(int y = 0; y< torsoTexture.height; y++)
                    {
                        if (torsoTexture.GetPixel(x,y) == previous)
                        {
                        //Debug.Log(x + " ,"+y);
                        torsoTexture.SetPixel(x, y, pantsColor);
                        }
                    }
            }
            torsoTexture.Apply();
            byte[] bytes = torsoTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Torso.png", bytes);
            // var Info = new DirectoryInfo("Assets/Resources/Player Components/Textures/Torso");
            // Debug.Log(Info);

    }  

    public void changeShoes()
    {
        Color shoeColor = transform.GetChild(0).gameObject.GetComponent<Image>().color;
        
            Texture2D leftFootTexture = Resources.Load<Texture2D>("Player Components/Textures/Left Foot");
            Texture2D rightFootTexture = Resources.Load<Texture2D>("Player Components/Textures/Right Foot");
            Color previous = leftFootTexture.GetPixel(4,1);
        
            //setting left foot
            for(int x = 4; x< leftFootTexture.width; x++)
            {
                for(int y = 0; y< leftFootTexture.height; y++)
                    {
                        if (leftFootTexture.GetPixel(x,y) == previous)
                        {
                        //Debug.Log(x + " ,"+y);
                        leftFootTexture.SetPixel(x, y, shoeColor);
                        }
                    }
            }
            leftFootTexture.Apply();
            byte[] bytes = leftFootTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Left Foot.png", bytes);

            //setting right foot
            for(int x = 4; x< rightFootTexture.width; x++)
            {
                for(int y = 0; y< rightFootTexture.height; y++)
                    {
                        if (rightFootTexture.GetPixel(x,y) == previous)
                        {
                        //Debug.Log(x + " ,"+y);
                        rightFootTexture.SetPixel(x, y, shoeColor);
                        }
                    }
            }
            rightFootTexture.Apply();
            bytes = rightFootTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/Resources/Player Components/Textures/Right Foot.png", bytes);

    }                      
}
