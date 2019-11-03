using System.Drawing;

namespace MessageParsing.Model
{
    public class PropertyTemplate : IPropertyTemplate
    {
        public string Name { get; set; }
        public Point? Location { get; set; }
        public Color? FontColor { get; set; }
        public Color? BackgroundColor { get; set; }

        public PropertyTemplate(string name, Point? location, Color? fontColor, Color? backgroundColor)
        {
            Name = name;
            Location = location;
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }
    }
}
