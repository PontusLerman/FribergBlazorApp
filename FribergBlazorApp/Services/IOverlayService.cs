//author: Johan Krångh
namespace FribergBlazorApp.Services
{
    public interface IOverlayService
    {
        bool IsVisible { get; }
        string ImageUrl { get; }
        void ShowImage(string imageUrl);
        void HideImage();
        event Action OnChange;
    }
}
