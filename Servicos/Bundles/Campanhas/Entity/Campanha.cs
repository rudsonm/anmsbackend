﻿using Servicos.Bundles.Core.Entity;
using Servicos.Bundles.Pessoas.Entity;
using Servicos.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Servicos.Bundles.Campanhas.Entity
{
    public enum CampanhaStatus
    {
        [Description("FINALIZADO")]
        FINALIZADO,
        [Description("CANCELADO")]
        CANCELADO,
        [Description("EM_ANDAMENTO")]
        EM_ANDAMENTO,
        [Description("PAUSADA")]
        PAUSADA
    }
    public class Campanha : AbstractEditableEntity
    {
        public Campanha()
        {
            this.Status = "EM_ANDAMENTO";
            this.Contribuicao = 0;            
        }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public float Meta { get; set; }
        public float Contribuicao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual float Andamento {
            get
            {
                return (Contribuicao * 100) / Meta;
            }
        }

        public virtual List<int> Fotos
        {
            get
            {
                return new ServicosContext()
                    .CampanhaFotos
                    .Where(foto => foto.Campanha == this.Id)
                    .Select(foto => foto.Id)
                    .ToList();
            }
        }

        public override string[] GetIncludeProperties()
        {
            return new string[] { "Usuario" };
        }
    }
}