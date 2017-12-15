using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;

namespace ElvUIUpdate
{
    public partial class frmMain : Form
    {
        const string CONFIGKEYPATH = "wowpath";
        const string TUKUIURL = "https://www.tukui.org/welcome.php";
        string wowPath;
        string addonsFolder;
        string localFilePath;
        System.Collections.Specialized.NameValueCollection appSettings;

        public frmMain()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadSettings();
            downloadZip();
        }

        private void downloadZip()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(TUKUIURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                if (!string.IsNullOrEmpty(data))
                {
                    int startPos = -1, endPos = -1;
                    startPos = data.IndexOf("<a href=\"/downloads/elvui");
                    if (startPos != -1)
                    {
                        endPos = data.IndexOf('"', startPos + 10);
                        if (endPos != -1)
                        {
                            string zipUrl = @"https://www.tukui.org/" + data.Substring(startPos + 10, endPos - startPos - 10);
                            string zipFileName = zipUrl.Split('/').Last();
                            localFilePath = System.IO.Path.GetTempPath() + zipFileName;
                            using (WebClient wc = new WebClient())
                            {
                                wc.DownloadProgressChanged += (s, e) =>
                                {
                                    pgBar.Value = e.ProgressPercentage;
                                };
                                wc.DownloadFileCompleted += (s, e) =>
                                {
                                    DownloadFileCompleted();
                                };
                                lbProgress.Text = "Downloading ElvUI";
                                wc.DownloadFileAsync(new Uri(zipUrl), localFilePath);
                            }
                        }
                    }
                }

                response.Close();
                readStream.Close();
            }

        }

        private async void DownloadFileCompleted()
        {
            pgBar.Value = 0;
            lbProgress.Text = "Extracting Files";
            if (!string.IsNullOrEmpty(localFilePath))
            {
                using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(localFilePath)))
                {
                    ZipEntry zipEntry;
                    while ((zipEntry = zipStream.GetNextEntry()) != null)
                    {
                        pgBar.Value = Decimal.ToInt32(204800 / (new System.IO.FileInfo(localFilePath).Length));
                        string directoryName = Path.GetDirectoryName(zipEntry.Name);
                        string fileName = Path.GetFileName(zipEntry.Name);

                        if (directoryName.Length > 0 && !Directory.Exists(addonsFolder + "\\" + directoryName))
                        {
                            Directory.CreateDirectory(addonsFolder + "\\" + directoryName);
                        }

                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(addonsFolder + "\\" + zipEntry.Name.Replace("/", @"\")))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = zipStream.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            pgBar.Value = 100;
            lbProgress.Text = "Finished";
            await Task.Delay(5000);
            this.Close();
        }

        private void loadSettings()
        {
            appSettings = ConfigurationManager.AppSettings;
            wowPath = appSettings[CONFIGKEYPATH] ?? string.Empty;
            if (string.IsNullOrEmpty(wowPath))
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Please inform your WoW installation folder:";
                    DialogResult fbdReturn = fbd.ShowDialog();

                    if (fbdReturn == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        wowPath = fbd.SelectedPath;
                        Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                        config.AppSettings.Settings.Add(CONFIGKEYPATH, wowPath);
                        config.Save(ConfigurationSaveMode.Minimal);
                    }
                }
            }

            addonsFolder = wowPath + "\\Interface\\Addons";
        }
    }
}
