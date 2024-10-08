using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbTipoGastoBL
	{
		private readonly FlotasContext _db;
		public TbTipoGastoBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbTipoGastoModel> Obtener(int _TbTipoGastoId)
		{
			var tipo = await (from p in _db.TbTipoGasto
							  where p.TbTipoGastoId == _TbTipoGastoId
							  select (new TbTipoGastoModel
							  {
								  TbTipoGastoId = p.TbTipoGastoId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbTipoGastoModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbTipoGasto
						 select (new TbTipoGastoModel
						 {
							 TbTipoGastoId = p.TbTipoGastoId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return tipos;
		}
		public async Task Guardar(TbTipoGastoModel _TbTipoGasto)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbTipoGasto);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbTipoGasto oTipoGasto = null;
				if (_TbTipoGasto.TbTipoGastoId == 0)
				{
					oTipoGasto = new TbTipoGasto
					{
						TbTipoGastoId = _TbTipoGasto.TbTipoGastoId,
						Nombre = _TbTipoGasto.Nombre,
						Activo = _TbTipoGasto.Activo,
					};
					_db.Add(oTipoGasto);
				}
				else
				{
					oTipoGasto = await _db.TbTipoGasto.Where(x => x.TbTipoGastoId == _TbTipoGasto.TbTipoGastoId).FirstAsync();
					if (oTipoGasto == null) throw new Exception($"Tipo de gasto no existe para el ID: {_TbTipoGasto.TbTipoGastoId}");

					oTipoGasto.TbTipoGastoId = _TbTipoGasto.TbTipoGastoId;
					oTipoGasto.Nombre = _TbTipoGasto.Nombre;
					oTipoGasto.Activo = _TbTipoGasto.Activo;

					_db.Update(oTipoGasto);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbTipoGastoId)
		{
			try
			{
				return await _db.TbTipoGasto.Where(x => x.TbTipoGastoId == _TbTipoGastoId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro tipo porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
