using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos.Bundles.Core.Entity
{
    public class AbstractEditableEntity : AbstractEntity
    {
        public AbstractEditableEntity() : base()
        {
            DataCadastro = DateTime.Now;
        }
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> DataModificacao { get; set; }
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> DataExclusao { get; set; }
        public override void Finalizar()
        {            
            DataExclusao = DateTime.Now;
            base.Finalizar();
        }
    }
}
