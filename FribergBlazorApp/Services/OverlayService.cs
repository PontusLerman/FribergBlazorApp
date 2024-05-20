//author: Johan Krångh
namespace FribergBlazorApp.Services
{
    public class OverlayService : IOverlayService
    {
        private bool isVisible;

        private string imageUrl;

        public bool IsVisible => isVisible;
        public string ImageUrl => imageUrl;

        public event Action OnChange;

        public void HideImage()
        {
            isVisible = false;
            NotifyStateChange();
        }

        public void ShowImage(string imageUrl)
        {
            this.imageUrl = imageUrl;
            isVisible = true;
            NotifyStateChange();
        }
        private void NotifyStateChange() => OnChange?.Invoke();
    }
}
