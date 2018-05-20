using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Komorebi
{
    static class MeshManager
    {
        private static Dictionary<string, Mesh> meshes = new Dictionary<string, Mesh>();

        public static Mesh getMesh(String filename)
        {
            var myFileName = Path.GetFileName(filename);
            if (meshes.ContainsKey(myFileName))
            {
                return meshes[myFileName];
            }
            else
            {
                Mesh newMesh = new Mesh(myFileName);
                meshes.Add(myFileName, newMesh);
                return newMesh;
            }
        }
    }
}
