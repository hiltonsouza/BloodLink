using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Core.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity() { }


        //pensando em colocar o Active aqui e updated
        public int Id { get; private set; }

    }


}
