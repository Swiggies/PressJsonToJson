using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows;
using PressJsonToJson.Objects;

namespace PressJsonToJson
{
    public class FTP
    {
        public FTPDetails ftpDetails = new FTPDetails();
        private FtpWebRequest ftpWebRequest;

        public FTP(string address,  string username, string password, bool passiveMode)
        {
            try
            {
                ftpDetails.address = "ftp://" + address;
                ftpDetails.username = username;
                ftpDetails.password = password;
                ftpDetails.passiveMode = passiveMode;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public List<string> GetFoldersOverFTP(FTPDetails details, string folder)
        {
            try
            { 
                Uri mapsUri = new Uri($"{details.address}/{folder}");
                ftpWebRequest = (FtpWebRequest)WebRequest.Create(mapsUri);
                ftpWebRequest.Credentials = new NetworkCredential(details.username, details.password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpWebRequest.UsePassive = details.passiveMode;
                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                
                List<string> dirs = new List<string>();

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    Console.WriteLine(line);
                    dirs.Add(line.Split('/')[1]);
                    line = streamReader.ReadLine();
                }

                streamReader.Close();

                return dirs;
            }
            catch(WebException e)
            {
                MessageBox.Show(e.Message);
                return new List<string>();
            }
        }
    }
}
