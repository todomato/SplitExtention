using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractExt
{
    class Program
    {
        static void Main(string[] args)
        {

            //取得當前路徑
            string path = System.Environment.CurrentDirectory;
            Console.WriteLine(path);

            //查詢所有檔案
            var fileList = GetAllFileList(path);

            //將所有副檔名分類
            var dics = fileList.GroupBy(c => c.Ext).ToDictionary(c => c.Key, c => c.ToList());

            //建立資料夾
            foreach (var ext in dics.Keys)
            {
                if (!Directory.Exists(ext))
                {
                    Directory.CreateDirectory(ext);
                }

                //移動複製檔案
                foreach (FileInfo f in dics[ext])
                {
                    var destFile = System.IO.Path.Combine(path,ext,f.FileName);
                    System.IO.File.Copy(f.Path, destFile, true);
                }
            }
            Console.WriteLine("分類完成,請自己關閉!");
            Console.ReadKey();
        }

        private static List<FileInfo> GetAllFileList(string path)
        {
            var result = new List<FileInfo>();

            try
            {
                //查目錄
                foreach (var dir in Directory.GetDirectories(path))
                {
                    //查檔案
                    foreach (var file in Directory.GetFiles(dir))
                    {
                        Console.WriteLine(file);
                        var i = new FileInfo()
                        {
                            Path = file,
                            FileName = Path.GetFileName(file),
                            Ext = Path.GetExtension(file)
                        };
                        result.Add(i);
                    }
                    GetAllFileList(dir);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }

    public class FileInfo
    {
        public string FileName { get; set; }
        public string Ext { get; set; }
        public string Path { get; set; }
    }

}
