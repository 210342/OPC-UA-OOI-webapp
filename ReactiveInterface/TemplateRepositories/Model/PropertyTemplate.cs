namespace TemplateRepositories.Model
{
    public class PropertyTemplate : IPropertyTemplate
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public string HexColor { get; set; }

        public PropertyTemplate() { }

        public PropertyTemplate(string name, Point location, string hexColor)
        {
            Name = name;
            Location = location;
            HexColor = hexColor;
        }
    }
}
