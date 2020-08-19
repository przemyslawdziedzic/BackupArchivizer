using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupArchivizer
{
    class FileInfoComparer : IEqualityComparer<FileInfo>
    {
        public bool Equals(FileInfo x, FileInfo y)
        {
            var xName = Path.GetFileNameWithoutExtension(x.FullName);
            var yName = Path.GetFileNameWithoutExtension(y.FullName);
            return xName == yName;
        }

        public int GetHashCode(FileInfo obj)
        {
            return Path.GetFileNameWithoutExtension(obj.FullName).GetHashCode();
        }
    }
}
