using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolomonDesktop.Model
{
    /// <summary>
    /// Сущность. Оъект планировщика
    /// </summary>
    public class SoloEntity
    {
        public string Caption { get; set; }

        public List<SoloLink> MasterLinks { get; set; }
        public List<SoloLink> SlaveLinks { get; set; }
               

    }
}
