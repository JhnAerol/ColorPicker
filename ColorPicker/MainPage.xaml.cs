

namespace ColorPicker
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        public void sld_ValueChanged(object sender, EventArgs args)
        {
            //Initialize the RGB values
            var red = sldRed.Value;
            var green = sldGreen.Value;
            var blue = sldBlue.Value;

            //Set the RGB colors from the initialize values
            Color color = Color.FromRgb(red, green, blue);

            //Call the method to set the color of the backgrounds
            SetColor(color);
        }

        //Set the backgrounds color 
        private void SetColor(Color color)
        {
            btnRandomizer.BackgroundColor = color;
            Container.BackgroundColor = color;
            lblHex.Text = color.ToHex();
        }

        public void Randomizer (object sender, EventArgs args)
        {
            //Generate a random number
            var rand = new Random();

            //Initializing the RGB value from the random numbers
            var color = Color.FromRgb(
                rand.Next(0, 256),
                rand.Next(0, 256),
                rand.Next(0, 256));

            //Set the slider value
            sldBlue.Value = color.Blue;
            sldGreen.Value = color.Green;
            sldRed.Value = color.Red;

            SetColor(color);
        }

        private void btnEnterHex(object sender, EventArgs args)
        {
            //Get the value from entry as hex value
            var color = Color.FromHex(txtInput.Text);

            sldBlue.Value = color.Blue;
            sldGreen.Value = color.Green;
            sldRed.Value = color.Red;

            SetColor(color);
        }
    }
}
