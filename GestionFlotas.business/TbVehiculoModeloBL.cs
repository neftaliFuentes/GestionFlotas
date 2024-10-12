using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbVehiculoModeloBL
	{
		private readonly FlotasContext _db;
		public TbVehiculoModeloBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbVehiculoModeloModel> Obtener(int _TbVehiculoModeloId)
		{
			var tipo = await (from p in _db.TbVehiculoModelo
							  where p.TbVehiculoModeloId == _TbVehiculoModeloId
							  select (new TbVehiculoModeloModel
							  {
								  TbVehiculoModeloId = p.TbVehiculoModeloId,
								  TbVehiculoMarcaId = p.TbVehiculoMarcaId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbVehiculoModeloModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbVehiculoModelo
						 select (new TbVehiculoModeloModel
						 {
							 TbVehiculoModeloId = p.TbVehiculoModeloId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return tipos;
		}
		public async Task Guardar(TbVehiculoModeloModel _TbVehiculoModelo)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbVehiculoModelo);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbVehiculoModelo oVehiculoModelo = null;
				if (_TbVehiculoModelo.TbVehiculoModeloId == 0)
				{
					oVehiculoModelo = new TbVehiculoModelo
					{
						TbVehiculoMarcaId = _TbVehiculoModelo.TbVehiculoMarcaId,
						Nombre = _TbVehiculoModelo.Nombre,
						Activo = _TbVehiculoModelo.Activo,
					};
					_db.Add(oVehiculoModelo);
					await _db.SaveChangesAsync();
					_TbVehiculoModelo.TbVehiculoModeloId = oVehiculoModelo.TbVehiculoModeloId;
				}
				else
				{
					oVehiculoModelo = await _db.TbVehiculoModelo.Where(x => x.TbVehiculoModeloId == _TbVehiculoModelo.TbVehiculoModeloId).FirstAsync();
					if (oVehiculoModelo == null) throw new Exception($"Modelo vechículo no existe para el ID: {_TbVehiculoModelo.TbVehiculoModeloId}");

					oVehiculoModelo.TbVehiculoMarcaId = _TbVehiculoModelo.TbVehiculoMarcaId;
					oVehiculoModelo.Nombre = _TbVehiculoModelo.Nombre;
					oVehiculoModelo.Activo = _TbVehiculoModelo.Activo;

					_db.Update(oVehiculoModelo);
					await _db.SaveChangesAsync();
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbVehiculoModeloId)
		{
			try
			{
				return await _db.TbVehiculoModelo.Where(x => x.TbVehiculoModeloId == _TbVehiculoModeloId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro tipo porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
		public async Task<List<TbVehiculoModeloModel>> ListarByMarcaId(short _TbVehiculoMarcaId)
		{
			var tipos = await (from p in _db.TbVehiculoModelo
						 where	p.TbVehiculoMarcaId == _TbVehiculoMarcaId
						 select (new TbVehiculoModeloModel
						 {
							 TbVehiculoModeloId = p.TbVehiculoModeloId,
							 TbVehiculoMarcaId = p.TbVehiculoMarcaId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).ToListAsync();

			return tipos;
		}
	}
}
