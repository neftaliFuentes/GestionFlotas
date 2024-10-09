using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbPaisBL
	{
		private readonly FlotasContext _db;
		public TbPaisBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbPaisModel> Obtener(int _TbPaisId)
		{
			var pais = await (from p in _db.TbPais
							  where p.TbPaisId == _TbPaisId
							  select (new TbPaisModel
							  {
								  TbPaisId = p.TbPaisId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return pais;
		}
		public IQueryable<TbPaisModel> ListarAsQuerable()
		{
			var paises = (from p in _db.TbPais
						  select (new TbPaisModel
						  {
							  TbPaisId = p.TbPaisId,
							  Nombre = p.Nombre.Trim(),
							  Activo = p.Activo,
							  ActivoString = p.Activo ? "SI" : "NO",
						  })).AsQueryable();

			return paises;
		}
		public async Task<List<TbPaisModel>> ListarByDDL()
		{
			var paises = await (from p in _db.TbPais
								where p.Activo
								select (new TbPaisModel
								{
									TbPaisId = p.TbPaisId,
									Nombre = p.Nombre.Trim()
								})).ToListAsync();

			return paises;
		}
		public async Task Guardar(TbPaisModel _TbPais)
		{
			try
			{
				//List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbPais);
				//if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbPais oPais = null;
				if (_TbPais.TbPaisId == 0)
				{
					oPais = new TbPais
					{
						TbPaisId = _TbPais.TbPaisId,
						Nombre = _TbPais.Nombre,
						Activo = _TbPais.Activo,
					};
					_db.Add(oPais);
				}
				else
				{
					oPais = await _db.TbPais.Where(x => x.TbPaisId == _TbPais.TbPaisId).FirstAsync();
					if (oPais == null) throw new Exception($"País no existe para el ID: {_TbPais.TbPaisId}");

					oPais.TbPaisId = _TbPais.TbPaisId;
					oPais.Nombre = _TbPais.Nombre;
					oPais.Activo = _TbPais.Activo;

					_db.Update(oPais);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbPaisId)
		{
			try
			{
				return await _db.TbPais.Where(x => x.TbPaisId == _TbPaisId).ExecuteDeleteAsync();

			}
			catch (Exception ex)
			{
				if (ex.Message.ToUpper().Contains("CONSTRAI"))
					throw new Exception("No se puede eliminar el registro pais porque esta siendo utilizado en el sistema");
				else
					throw;
			}
		}
	}
}
