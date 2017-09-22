using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {
    public float fadeDuration = 1;
    public float timeToFade;
    public Texture destinationTexture;
    public Image ImageComponent;

    public enum changeState
    {
        ChangingToBlack,
        ChangingToImage,
        Done
    }

    public changeState ImageState;
    // Use this for initialization
    void Start()
    {
        ImageComponent = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {

        switch(ImageState)
        {
            case changeState.ChangingToBlack:
                ImageComponent.color = Color.Lerp(Color.white, Color.black, (fadeDuration - (Time.timeSinceLevelLoad - timeToFade)));
                if (timeToFade < Time.timeSinceLevelLoad)
                    StartImgChange(true);
                break;
            case changeState.ChangingToImage:
                ImageComponent.color = Color.Lerp(Color.white, Color.white, (fadeDuration - (Time.timeSinceLevelLoad - timeToFade)));
                if (timeToFade < Time.timeSinceLevelLoad)
                    ImageState = changeState.Done;
                break;
            case changeState.Done:
                // Do nothing
                break;

        }

    }

    public void ChangeImage()
    {
        ImageComponent.material.mainTexture = destinationTexture;
    }

    public void StartImgChange(bool readyForImage)
    {
        timeToFade = Time.timeSinceLevelLoad + fadeDuration;
        
        if(readyForImage)
        {
            ImageState = changeState.ChangingToImage;
        }
        else
        {
            ImageState = changeState.ChangingToBlack;
        }
        // 
    }
}
