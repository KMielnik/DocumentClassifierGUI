using System.Windows.Media;

namespace DocumentClassifierGUI
{
    public interface IDocumentMarkingView
    {
        public void SaveActualElement();
        public void SetActualDocumentClass((string Name, Brush Color) newItemClass);
    }
}