using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task
{
   public class FolderConvertor
    {
       
        public void Serialize(string folderPath,string saveIn,string nameFile)
        {
            RootContainer rootContainer = new RootContainer();
            

            if (!String.IsNullOrEmpty(folderPath))
            {

                SerializeDirectory(folderPath,"", ref rootContainer);


                FileStream fs = new FileStream(string.Format("{0}\\{1}.dat",saveIn,nameFile), FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, rootContainer);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }

            }
        }
        private void SerializeDirectory(string path,string lastDirectoryPath, ref RootContainer rootContainer)
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(path);

            if (lastDirectoryPath != String.Empty)
            {
                lastDirectoryPath = lastDirectoryPath + "\\" + path.Substring(path.LastIndexOf('\\') + 1);
            }
            else
            {
                lastDirectoryPath = path.Substring(path.LastIndexOf('\\') + 1);
            }
            rootContainer.ListDirectories.Add(lastDirectoryPath);

            foreach (var item in currentDirectory.GetFiles())
            {
                rootContainer.ListFiles.Add(String.Format("{0}\\{1}", lastDirectoryPath, item.Name), File.ReadAllBytes(item.FullName));
            }
            foreach (var item in currentDirectory.GetDirectories())
            {
                SerializeDirectory(item.FullName,lastDirectoryPath,ref rootContainer);
            }

        }

        public void Deserialize(string deserealizationFile,string deserealizationPath)
        {

            RootContainer rootContainer = new RootContainer();
            FileStream fs = new FileStream(deserealizationFile, FileMode.Open);
            DirectoryInfo rootDirectory = new DirectoryInfo(deserealizationPath);


            try
            {
                BinaryFormatter formatter = new BinaryFormatter();


                rootContainer = (RootContainer)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                throw new Exception("Failed to deserialize.Reason: " + e.Message);
            }
            finally
            {
                fs.Close();
            }


            foreach (var item in rootContainer.ListDirectories)
            {
                rootDirectory.CreateSubdirectory(item);
            }
            foreach (var item in rootContainer.ListFiles)
            {
                File.WriteAllBytes(rootDirectory.FullName + "\\" + item.Key, item.Value);
            }
           
           

        }
    }
}
