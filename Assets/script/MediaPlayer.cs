using System.IO;
using UnityEngine;
using SimpleFileBrowser;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Runtime.ConstrainedExecution;

public class MediaPlayer : MonoBehaviour
{
    public RawImage imageDisplay;
    public RawImage videoDisplay;
    public VideoPlayer videoPlayer;


    // Start is called before the first frame update
    void Start()
    {
        // Initializ File browser
		FileBrowser.SetFilters( true, new FileBrowser.Filter( "Images", ".jpg", ".png" ), new FileBrowser.Filter( "Text Files", ".txt", ".pdf" ) );
		FileBrowser.SetDefaultFilter( ".jpg" );
		FileBrowser.SetExcludedExtensions( ".lnk", ".tmp", ".zip", ".rar", ".exe" );
		FileBrowser.AddQuickLink( "Users", "C:\\Users", null );
    }

    // Update is called once per frame
    void Update(){

    }

    // Open File selection page
    public void OpenFileDialog(){
        FileBrowser.ShowLoadDialog(
            paths => {
                if (paths.Length > 0){
                    string filePath = paths[0];
                    // Handle and play the selected file
                    FileHandler(filePath);                   
                }
            },
            null,
            FileBrowser.PickMode.Files
        );
    }

    // Handle and play the selected file based on it's type
    private void FileHandler(string filePath){
        // Get the file extension
        string extension = Path.GetExtension(filePath).ToLower();

        // Displayer image
        if (IsImageFile(extension)){
            StartCoroutine(LoadImage(filePath)); 
        }
        // Display video
        else if (IsVideoFile(extension)){
            PlayVideo(filePath);
        }
        else{
            Debug.LogWarning("Unsupported file type: " + extension);
        }
    }

    // Load and display images
    private System.Collections.IEnumerator LoadImage(string filePath){
        // Load the image file as a byte array
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);

        // Create a Texture2D
        Texture2D texture = new Texture2D(2, 2);

        // Load the image data into the texture
        if (texture.LoadImage(fileData))
        {
            // Assign the texture to the RawImage
            imageDisplay.texture = texture;
            imageDisplay.SetNativeSize();
        }
        else{
            Debug.LogError("Failed to load image.");
        }
        yield return null;
    }

    // Load and display video
    private void PlayVideo(string filePath){
        // Ensure the file path is valid
        if (!string.IsNullOrEmpty(filePath)){
            // Set the video player's source to the selected file
            videoPlayer.url = filePath;

            // If you're displaying the video in a UI RawImage

            videoPlayer.renderMode = VideoRenderMode.RenderTexture; // Disable direct rendering
            videoPlayer.targetTexture = new RenderTexture(800, 600, 16); // Adjust resolution
            videoDisplay.texture = videoPlayer.targetTexture;
            
            // Play the video
            videoPlayer.Play();
        }
        else{
            Debug.LogError("Video file path is invalid.");
        }
    }

    // Check if the file is an image
    private bool IsImageFile(string extension){
        // Check if the file is an image based on its extension
        return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp";
    }

    // Check if the file is a video
    private bool IsVideoFile(string extension){
        // Check if the file is a video based on its extension
        return extension == ".mp4" || extension == ".avi" || extension == ".mov" || extension == ".mkv" || extension == ".wmv";
    }
}
