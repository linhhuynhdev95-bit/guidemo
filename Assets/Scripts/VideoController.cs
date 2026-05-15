using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Cần include namespace này

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Kéo Video Player component vào đây từ Inspector
    void Start()
    {
        // Gán sự kiện khi video kết thúc
        videoPlayer.loopPointReached += OnVideoEnd;

        // Bắt đầu phát video khi game bắt đầu (nếu Play On Awake đã bỏ chọn)
        // videoPlayer.Play(); 
    }

    // Phương thức phát video
    public void PlayVideo()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    // Phương thức tạm dừng video
    public void PauseVideo()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    // Phương thức dừng video
    public void StopVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }

    // Phương thức tua lại video
    public void RestartVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop(); // Dừng để reset thời gian
            videoPlayer.Play();
        }
    }

    // Sự kiện khi video kết thúc (nếu không lặp)
    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video đã kết thúc!");
        StartGameClick();
        // Bạn có thể làm gì đó khi video kết thúc, ví dụ chuyển cảnh
        // UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public int DemoSceneIdx = 1;
    public void StartGameClick()
    {
        SceneManager.LoadScene(DemoSceneIdx);
    }
}