using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSOFT.Common
{
    public class EnumUtility
    {
        public string GetResourceNameByValue(string value)
        {
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("MISA.ImportExport.Core.Properties.Resources", this.GetType().Assembly);
            var entry =
                resourceManager.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentCulture, true, true)
                  .OfType<DictionaryEntry>()
                  .FirstOrDefault(e => e.Value.ToString() == value);
            if (entry.Key == null)
                return null;
            return entry.Key.ToString();
        }
        public string GetResourceNameByValue(string value, string enumNameStringContains)
        {
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("MISA.ImportExport.Core.Properties.Resources", this.GetType().Assembly);
            var entry =
                resourceManager.GetResourceSet(System.Threading.Thread.CurrentThread.CurrentCulture, true, true)
                  .OfType<DictionaryEntry>()
                  .FirstOrDefault(e => e.Value.ToString() == value && e.Key.ToString().Contains(enumNameStringContains));
            if (entry.Key == null)
                return null;
            return entry.Key.ToString();
        }
    }
}
