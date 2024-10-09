using GestionFlotas.dataaccess;
using GestionFlotas.model;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
    public class TbModuloAccionBL
    {
        private readonly FlotasContext _db;
        public TbModuloAccionBL(FlotasContext db)
        {
            _db = db;
        }
        public async Task<TbModuloAccionModel> Obtener(int _TbModuloAccionId)
        {
            var ModuloAccion = await (from M in _db.TbModuloAccion
                                      where M.TbModuloAccionId == _TbModuloAccionId
                                      select (new TbModuloAccionModel
                                      {
                                          TbModuloAccionId = M.TbModuloAccionId,
                                          TbModuloId = M.TbModuloId,
                                          Nombre = M.Nombre,
                                          Icono = M.Icono,
                                          Url = M.Url,
                                          EsVisibleMenu = M.EsVisibleMenu,
                                          Activo = M.Activo,
                                      })).FirstOrDefaultAsync();


            return ModuloAccion;
        }
        public async Task<List<TbModuloAccionModel>> ObtenerListByTbModuloId(int _TbModuloId)
        {
            List<TbModuloAccionModel> ModulosAccion = await (from M in _db.TbModuloAccion
                                                             where M.TbModuloId == _TbModuloId && M.EsVisibleMenu == true && M.Activo == true
                                                             select (new TbModuloAccionModel
                                                             {
                                                                 TbModuloAccionId = M.TbModuloAccionId,
                                                                 TbModuloId = M.TbModuloId,
                                                                 Nombre = M.Nombre,
                                                                 Icono = M.Icono,
                                                                 Url = M.Url,
                                                                 EsVisibleMenu = M.EsVisibleMenu,
                                                                 Activo = M.Activo,
                                                             })).ToListAsync();


            return ModulosAccion;
        }
    }
}
