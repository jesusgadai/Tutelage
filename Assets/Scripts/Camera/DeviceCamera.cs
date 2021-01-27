using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

public class DeviceCamera : MonoBehaviour
{
    public Image preview;
    public Sprite cameraIcon;
    public Button retake;
    public Button next;
    public UnityEvent onImageCapture;
    public UnityEvent newNextAction;

    void Start()
    {
        next.onClick.AddListener(() => { TakePicture(); });
    }

    public void TakePicture()
    {
        if (NativeCamera.IsCameraBusy())
            return;

        ProcessImage(512);
    }

    void ProcessImage(int maxSize)
    {
        preview.sprite = cameraIcon;
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                CropImage(texture);
                // Destroy(texture);
            }
        }, maxSize);


        Debug.Log("Permission result: " + permission);
    }

    void CropImage(Texture2D texture)
    {
        // ImageCropper
        ImageCropper.Instance.Show(texture, (bool result, Texture originalImage, Texture2D croppedImage) =>
        {
            if (result)
            {
                // Assign cropped texture to the RawImage
                Material material = preview.material;
                preview.sprite = null;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");
                material.mainTexture = croppedImage;

                if (onImageCapture != null)
                    onImageCapture.Invoke();

                retake.onClick.AddListener(() =>
                {
                    Destroy(originalImage);
                    Destroy(croppedImage);
                });

                next.onClick.RemoveAllListeners();
                next.onClick.AddListener(() =>
                {
                    SaveImage(DuplicateTexture(croppedImage));
                    Destroy(originalImage);
                    Destroy(croppedImage);
                    newNextAction.Invoke();
                });
            }
        },
        settings: new ImageCropper.Settings()
        {
            ovalSelection = true,
            autoZoomEnabled = true,
            selectionMinAspectRatio = 1,
            selectionMaxAspectRatio = 1,
            imageBackground = Color.clear
        });
    }

    void SaveImage(Texture2D croppedTexture)
    {
        byte[] textureBytes = croppedTexture.EncodeToPNG();

        string path = "resources";
#if UNITY_ANDROID
        path = ".resources";
#endif
        path = Path.Combine(Application.persistentDataPath, path);
        Directory.CreateDirectory(path);
        string playerImagePath = Path.Combine(path, "playerImage.png");
        File.WriteAllBytes(playerImagePath, textureBytes);
        Debug.Log("[DeviceCamera.cs] - Image saved to: " + playerImagePath);

        if (User.instance != null)
            User.instance.UpdateUserImage();
    }

    Texture2D DuplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}
