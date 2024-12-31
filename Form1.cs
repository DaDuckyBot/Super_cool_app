namespace Super_cool_app
{
    public partial class Form1 : Form
    {
        private bool isCorrupted = false;

        public Form1()
        {
            InitializeComponent();
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!isCorrupted)
            {
                checkBox1.Enabled = false;
                checkBox1.CheckedChanged -= checkBox1_CheckedChanged;
                isCorrupted = true;
                this.Text = "Checkbox Corrupted!";
                CorruptExecutable();
            }
        }

        private void CorruptExecutable()
        {
            try
            {
                string filePath = Application.ExecutablePath;
                string tempFilePath = Path.Combine(Path.GetTempPath(), "corruptor.exe");

                // Save a copy of the executable before it terminates
                File.Copy(filePath, tempFilePath, true);

                // Close the application gracefully
                Application.Exit();

                // Corrupt the executable file
                byte[] fileBytes = File.ReadAllBytes(tempFilePath);

                Random rand = new Random();
                for (int i = 0; i < fileBytes.Length; i++)
                {
                    if (rand.NextDouble() < 0.1)
                    {
                        fileBytes[i] = (byte)rand.Next(0, 256);
                    }
                }

                // Overwrite the original executable with corrupted data
                File.WriteAllBytes(filePath, fileBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during corruption: {ex.Message}");
            }
        }
    }
}
