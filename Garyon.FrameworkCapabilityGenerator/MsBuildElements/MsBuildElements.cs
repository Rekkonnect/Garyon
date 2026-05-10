using System.Xml.Linq;

namespace Garyon.FrameworkCapabilityGenerator.MsBuildElements;

internal abstract class MsBuildElement
{
    public abstract XElement ToXElement();

    protected static IReadOnlyList<XElement> XElements(IEnumerable<MsBuildElement> elements)
    {
        return elements.Select(e => e.ToXElement()).ToList();
    }

    protected static void WriteNonEmptyAttribute(XElement element, string attributeName, string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            element.SetAttributeValue(attributeName, value);
        }
    }
}

internal class Project : MsBuildElement
{
    public List<string> InitialTargets { get; set; } = [];

    public List<MsBuildElement> Elements { get; } = [];

    public override XElement ToXElement()
    {
        var element = new XElement("Project");
        WriteNonEmptyAttribute(element, "InitialTargets", string.Join(";", InitialTargets));
        element.Add(XElements(Elements));
        return element;
    }
}

internal class Choose : MsBuildElement
{
    public required WhenElement When { get; set; }
    public required OtherwiseElement Otherwise { get; set; }

    public override XElement ToXElement()
    {
        var element = new XElement("Choose");
        element.Add(When.ToXElement());
        element.Add(Otherwise.ToXElement());
        return element;
    }

    public class WhenElement : MsBuildElement
    {
        public required string Condition { get; set; }
        public List<MsBuildElement> Actions { get; set; } = [];

        public override XElement ToXElement()
        {
            var element = new XElement("When");
            element.SetAttributeValue("Condition", Condition);
            element.Add(XElements(Actions));
            return element;
        }
    }

    public class OtherwiseElement : MsBuildElement
    {
        public List<MsBuildElement> Actions { get; set; } = [];

        public override XElement ToXElement()
        {
            var element = new XElement("Otherwise");
            element.Add(XElements(Actions));
            return element;
        }
    }
}

internal class PropertyGroup : MsBuildElement
{
    public string? Condition { get; set; }
    public List<PropertyElement> Properties { get; } = [];

    public override XElement ToXElement()
    {
        var element = new XElement("PropertyGroup");
        WriteNonEmptyAttribute(element, "Condition", Condition);
        element.Add(XElements(Properties));
        return element;
    }

    public class PropertyElement : MsBuildElement
    {
        public required string Name { get; set; }
        public required string Value { get; set; }

        public override XElement ToXElement()
        {
            return new XElement(Name, Value);
        }
    }
}

internal class Target : MsBuildElement
{
    public required string Name { get; set; }
    public List<MsBuildElement> Children { get; } = [];

    public override XElement ToXElement()
    {
        var element = new XElement("Target");
        WriteNonEmptyAttribute(element, "Name", Name);
        element.Add(XElements(Children));
        return element;
    }
}
