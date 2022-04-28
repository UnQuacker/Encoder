using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Encoder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 | *.mp3; (.mp3) | WAV | *.wav; (.wav) | JPG | *.jpg; *.jpeg";
            if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Custom | *.cstm";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) { return; }

            byte[] content = File.ReadAllBytes(openFileDialog.FileName);
            byte[] test = Protect(content);

            File.WriteAllBytes(saveFileDialog.FileName, test);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Custom | *.cstm";
            if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "MP3 | *.mp3 | WAV | *.wav | JPG | *.jpg; *.jpeg";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) { return; }

            byte[] content = File.ReadAllBytes(openFileDialog.FileName);
            byte[] test = Unprotect(content);

            File.WriteAllBytes(saveFileDialog.FileName, test);

        }

        public static byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
