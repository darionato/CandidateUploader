using System;
using System.Configuration;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CandidateUploader.Infrastructure
{
    public static class CuConfig
    {

        public static long MaxVideoSeconds
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["cu:MaxVideoSeconds"]);
            }
        }

        public static long MaxUploads
        {
            get
            {
                return Convert.ToInt64(ConfigurationManager.AppSettings["cu:MaxUploads"]);
            }
        }

        public static long MaxUploadsPerUser
        {
            get
            {
                return Convert.ToInt64(ConfigurationManager.AppSettings["cu:MaxUploadsPerUser"]);
            }
        }

        public static long MaxImageBytes
        {
            get
            {
                return Convert.ToInt64(ConfigurationManager.AppSettings["cu:MaxImageBytes"]);
            }
        }

        public static Size ImageThumbnailSize
        {
            get
            {
                return GetSize(ConfigurationManager.AppSettings["cu:ImageThumbnailSize"]);
            }
        }

        public static Size ImageListSize
        {
            get
            {
                return GetSize(ConfigurationManager.AppSettings["cu:ImageListSize"]);
            }
        }

        public static Size ImageDetailsSize
        {
            get
            {
                return GetSize(ConfigurationManager.AppSettings["cu:ImageDetailsSize"]);
            }
        }

        private static Size GetSize(string value)
        {

            var result = new Size();

            var matches = Regex.Match(value, "^([0-9]+)x([0-9]+)$");

            if (matches.Success)
            {
                result.Width = Convert.ToInt32(matches.Groups[1].Value);
                result.Height = Convert.ToInt32(matches.Groups[2].Value);
            }

            return result;

        }

    }
}