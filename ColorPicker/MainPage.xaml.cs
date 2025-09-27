namespace ColorPicker
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            SetColor(Color.FromRgb(0, 0, 0));
            UpdateHexField("#000000");
        }

        public void sld_ValueChanged(object sender, EventArgs args)
        {
            // Check if sliders exist before using them
            if (sldRed == null || sldGreen == null || sldBlue == null)
                return;

            // Get RGB values from sliders (0-255 range)
            var red = (int)sldRed.Value;
            var green = (int)sldGreen.Value;
            var blue = (int)sldBlue.Value;

            // Validate RGB values are within proper range (0-255)
            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));

            // Update the value labels
            UpdateValueLabels(red, green, blue);

            // Create color from RGB values
            Color color = Color.FromRgb(red, green, blue);

            // Update background color and hex display
            SetColor(color);
            UpdateHexField(color.ToHex().ToUpper());
        }

        private void UpdateValueLabels(int red, int green, int blue)
        {
            // Update RGB value labels if they exist
            if (lblRedValue != null)
                lblRedValue.Text = red.ToString();
            if (lblGreenValue != null)
                lblGreenValue.Text = green.ToString();
            if (lblBlueValue != null)
                lblBlueValue.Text = blue.ToString();
        }

        // Set the background color of the display panel
        private void SetColor(Color color)
        {
            // Update color preview frame (display panel)
            if (Container != null)
                Container.BackgroundColor = color;

            // Update RGB value labels
            var red = (int)(color.Red * 255);
            var green = (int)(color.Green * 255);
            var blue = (int)(color.Blue * 255);
            UpdateValueLabels(red, green, blue);
        }

        // Update the hex display field
        private void UpdateHexField(string hexValue)
        {
            if (lblHex != null)
                lblHex.Text = hexValue;
        }

        public void Randomizer(object sender, EventArgs args)
        {
            // Check if sliders exist before using them
            if (sldRed == null || sldGreen == null || sldBlue == null)
                return;

            // Generate random RGB values (0-255)
            var rand = new Random();
            var red = rand.Next(0, 256);
            var green = rand.Next(0, 256);
            var blue = rand.Next(0, 256);

            // Create color from random RGB values
            var color = Color.FromRgb(red, green, blue);

            // Move sliders to correct positions that match RGB values
            sldRed.Value = red;
            sldGreen.Value = green;
            sldBlue.Value = blue;

            // Update background color
            SetColor(color);

            // Update HEX field with the random color
            UpdateHexField(color.ToHex().ToUpper());
        }

        private void btnEnterHex(object sender, EventArgs args)
        {
            // Check if input field exists and has valid text
            if (txtInput == null || string.IsNullOrWhiteSpace(txtInput.Text))
                return;

            // Check if sliders exist before using them
            if (sldRed == null || sldGreen == null || sldBlue == null)
                return;

            string hexValue = txtInput.Text.Trim().ToUpper();

            // Add # if missing
            if (!hexValue.StartsWith("#"))
                hexValue = "#" + hexValue;

            // Validate hex format
            if (!IsValidHexColor(hexValue))
                return;

            // Convert 3-digit hex to 6-digit if needed
            if (hexValue.Length == 4)
            {
                hexValue = $"#{hexValue[1]}{hexValue[1]}{hexValue[2]}{hexValue[2]}{hexValue[3]}{hexValue[3]}";
            }

            // Create color from hex
            Color color = Color.FromHex(hexValue);

            // Calculate RGB values (0-255 range)
            var red = (int)(color.Red * 255);
            var green = (int)(color.Green * 255);
            var blue = (int)(color.Blue * 255);

            // Move sliders to correct positions that match RGB values of entered HEX
            sldRed.Value = red;
            sldGreen.Value = green;
            sldBlue.Value = blue;

            // Update background color
            SetColor(color);

            // Update hex display
            UpdateHexField(hexValue);

            // Clear the input field
            txtInput.Text = string.Empty;
        }

        private bool IsValidHexColor(string hex)
        {
            // Must start with # and be either 4 or 7 characters long
            if (!hex.StartsWith("#") || (hex.Length != 4 && hex.Length != 7))
                return false;

            // Check if all characters after # are valid hex digits
            for (int i = 1; i < hex.Length; i++)
            {
                char c = hex[i];
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F')))
                    return false;
            }

            return true;
        }

        private async void btnCopyHex(object sender, EventArgs args)
        {
            if (lblHex == null || string.IsNullOrWhiteSpace(lblHex.Text))
                return;

            // Copy hex value to clipboard
            await Clipboard.SetTextAsync(lblHex.Text);
        }
    }
}
