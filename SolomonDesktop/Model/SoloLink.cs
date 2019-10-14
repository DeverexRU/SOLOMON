using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolomonDesktop.Model
{
    /// <summary>
    /// Описание связи двух сущностей
    /// </summary>
    public class SoloLink
    {
        public EnumLinkKind LinkKind { get; set; }

        public List<SoloEntity> MasterEtenities { get; set; }
        public List<SoloEntity> SlaveEtenities { get; set; }

    }
}
