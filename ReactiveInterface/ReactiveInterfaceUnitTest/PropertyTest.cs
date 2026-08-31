using ReactiveHMI.ReactiveInterface.TemplateRepositories.Model;
using ReactiveHMI.ReactiveInterface.ReactiveInterfaceUnitTest.Mocks;
using Xunit;

namespace ReactiveHMI.ReactiveInterface.ReactiveInterfaceUnitTest
{
    public class PropertyTest
    {
        [Fact]
        public void MapPrintableToDrawableProperty()
        {
            PrintableProperty printable = new(
                new TestSubscription() { Value = "Printable value" },
                new PropertyTemplate("Name", null, "red")
            );
            DrawableProperty drawable = printable.MapToDrawable(new Point(10, 100));
            Assert.True(drawable is DrawableProperty);
            Assert.Equal(printable.Template.Name, drawable.Template.Name);
            Assert.Equal(printable.Subscription, drawable.Subscription);
            Assert.Equal(printable.Template.HexColor, drawable.Template.HexColor);
            Assert.Equal(10, drawable.Template.Location.X);
            Assert.Equal(100, drawable.Template.Location.Y);
        }

        [Fact]
        public void MapDrawableToPrintableProperty()
        {
            DrawableProperty drawable = new(
                new TestSubscription() { Value = "Drawable value" },
                new PropertyTemplate("Name", new Point(0, 0), "red")
            );
            PrintableProperty printable = drawable.MapToPrintable();
            Assert.True(printable is PrintableProperty);
            Assert.Equal(drawable.Template.Name, printable.Template.Name);
            Assert.Equal(drawable.Subscription, printable.Subscription);
            Assert.Equal(drawable.Template.HexColor, printable.Template.HexColor);
            Assert.Null(printable.Template.Location);
        }
    }
}
