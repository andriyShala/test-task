using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task
{
    [Serializable]
   public class RootContainer
    {
        public RootContainer()
        {
            this.ListDirectories = new List<string>();
            this.ListFiles = new Dictionary<string, byte[]>();
        }
        public List<string> ListDirectories { get; }
        public Dictionary<string,byte[]> ListFiles { get;}
    }
}
