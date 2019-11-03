using System.Drawing;

namespace MessageParsing.Model
{
    public interface IPropertyTemplate
    {
        string Name { get; set; }
        Point? Location { get; set; }
        Color? FontColor { get; set; }
        Color? BackgroundColor { get; set; }
    }
}
