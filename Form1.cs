using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlyffFileMover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class FilePath
        {
            public string source { get; set; }
            public string destination { get; set; }
        }
        public class FilePathConfig
        {
            public List<FilePath> files { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var config = LoadFilePaths("FlyffPath.json");

                foreach (var file in config.files)
                {
                    CopyFile(file.source, file.destination);
                }

                toolStripStatusLabel1.Text = "Status: Done.";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Status: {ex.Message}";
            }
        }
        private FilePathConfig LoadFilePaths(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<FilePathConfig>(json);
        }
        private void CopyFile(string source, string destination)
        {
            string destinationDirectory = Path.GetDirectoryName(destination);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            File.Copy(source, destination, true);
        }
    }
}
