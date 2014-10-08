using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public class ContentManager
    {

        public Dictionary<string, IResource> imgDict = new Dictionary<string, IResource>();


        internal ContentManager()
        {
        }

        public IResource LoadResource(string path)
        {
            return null;
        }

        public IResource LoadResource(string path, ResourceType resourceType)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            IResource resource = null;
            if (resourceType == ResourceType.Spriter)
            {
                using (var fs = File.OpenRead(path))
                {
                    resource = new SpriterResource(fs);
                }
            }
            else
            {

            }
            return resource;
        }

        public void UnloadResource(string key)
        {

        }


    }
}
