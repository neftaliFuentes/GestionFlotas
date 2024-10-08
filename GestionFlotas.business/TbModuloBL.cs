using GestionFlotas.dataaccess;
using GestionFlotas.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlotas.business
{
    public class TbModuloBL
    {
        private readonly FlotasContext _db;
        public TbModuloBL(FlotasContext db)
        {
            _db = db;
        }
        public async Task<TbmoduloModel> Obtener(int _TbModuloId)
        {
            var tipo = await (from M in _db.TbModulo
                              where M.TbModuloId == _TbModuloId
                              select (new TbmoduloModel
                              {
                                  TbModuloId = M.TbModuloId,
                                  Nombre = M.Nombre.Trim(),
                                  Icono = M.Icono,
                                  Activo = M.Activo,
                                  ActivoString = M.Activo ? "SI" : "NO",
                                  EsPadre = M.EsPadre,
                                  PadreId = M.PadreId,
                                  Orden= M.Orden,
                              })).FirstOrDefaultAsync();


            return tipo;
        }
        public IQueryable<TbmoduloModel> ListarAsQuerable()
        {
            var Modulos = (from M in _db.TbModulo
                         select (new TbmoduloModel
                         {
                             TbModuloId = M.TbModuloId,
                             Nombre = M.Nombre.Trim(),
                             Icono = M.Icono,
                             Activo = M.Activo,
                             ActivoString = M.Activo ? "SI" : "NO",
                             EsPadre = M.EsPadre,
                             PadreId = M.PadreId,
                             Orden = M.Orden,
                         })).AsQueryable();

            return Modulos;
        }
    }
}
