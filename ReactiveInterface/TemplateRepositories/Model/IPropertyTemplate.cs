namespace ReactiveHMI.TemplateRepositories.Model
{
    public interface IPropertyTemplate
    {
        string Name { get; set; }
        Point Location { get; set; }
        string HexColor { get; set; }
    }
}
