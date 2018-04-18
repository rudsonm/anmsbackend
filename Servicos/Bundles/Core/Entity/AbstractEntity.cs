using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Servicos.Bundles.Core.Entity
{
    public class AbstractEntity
    {
        public AbstractEntity()
        {
            Ativo = true;            
        }

        public AbstractEntity(int id)
        {
            this.Id = id;
            this.Ativo = true;
        }

        [Key]
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public virtual void Finalizar() {
            Ativo = false;
        }

        public virtual string[] GetIncludeProperties() {
            return new string[0];
        }
    }
}