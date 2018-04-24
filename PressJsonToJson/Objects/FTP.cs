using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows;

namespace PressJsonToJson
{
    public class FTP
    {
        public List<string> GetMapFoldersOverFTP()
        {
            try
            {
                Uri mapsUri = new Uri("");
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(mapsUri);
                ftpWebRequest.Credentials = new NetworkCredential("user", "password");
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpWebRequest.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                
                List<string> dirs = new List<string>();

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    Console.WriteLine(line.Split('/')[1]);
                    dirs.Add(line);
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

        public List<string> GetTypeFoldersOverFTP()
        {
            try
            {
                Uri mapsUri = new Uri("");
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(mapsUri);
                ftpWebRequest.Credentials = new NetworkCredential("user", "password");
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpWebRequest.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());


                List<string> dirs = new List<string>();

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    Console.WriteLine(line.Split('/')[1]);
                    dirs.Add(line);
                    line = streamReader.ReadLine();
                }

                streamReader.Close();

                return dirs;
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
                return new List<string>();
            }
        }
    }
}
