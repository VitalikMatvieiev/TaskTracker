using System;
using System.Collections.Generic;
using System.Text;

namespace TreloDAL.Models
{
    public class UserCredentialFile
    {
        public int FileId { get; set; }
        public CredetialFileType AppFileType { get; set; }
        public byte[] FileData { get; set; }
        public string FileType { get; set; }
        public string ContentType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
    public enum CredetialFileType
    {
        Avatar,
    }
}
