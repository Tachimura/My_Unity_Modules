namespace MapsSystem.MiniMap
{
    public interface IMiniMap
    {
        void Show();
        void Hide();
        void ZoomIn(int percentage);
        void ZoomOut(int percentage);
    }
}