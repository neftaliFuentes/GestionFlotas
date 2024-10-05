using GestionFlotas.model;
using GestionFlotas.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotas.business
{
	public class TbPersonaCargoBL
	{
		private readonly FlotasContext _db;
		public TbPersonaCargoBL(FlotasContext db)
		{
			_db = db;
		}
		public async Task<TbPersonaCargoModel> Obtener(int _TbPersonaCargoId)
		{
			var tipo = await (from p in _db.TbPersonaCargo
							  where p.TbPersonaCargoId == _TbPersonaCargoId
							  select (new TbPersonaCargoModel
							  {
								  TbPersonaCargoId = p.TbPersonaCargoId,
								  Nombre = p.Nombre.Trim(),
								  Activo = p.Activo,
								  ActivoString = p.Activo ? "SI" : "NO",
							  })).FirstOrDefaultAsync();


			return tipo;
		}
		public IQueryable<TbPersonaCargoModel> ListarAsQuerable()
		{
			var tipos = (from p in _db.TbPersonaCargo
						 select (new TbPersonaCargoModel
						 {
							 TbPersonaCargoId = p.TbPersonaCargoId,
							 Nombre = p.Nombre.Trim(),
							 Activo = p.Activo,
							 ActivoString = p.Activo ? "SI" : "NO",
						 })).AsQueryable();

			return tipos;
		}
		public async Task Guardar(TbPersonaCargoModel _TbPersonaCargo)
		{
			try
			{
				List<ErrorValidacionModel> validacionModelo = ValidadorModelBL.valida(_TbPersonaCargo);
				if (validacionModelo.Count > 0) throw new Exception(string.Join("<br/>", validacionModelo.Select(x => x.Mensaje)));

				TbPersonaCargo oCargoPersona = null;
				if (_TbPersonaCargo.TbPersonaCargoId == 0)
				{
					oCargoPersona = new TbPersonaCargo
					{
						TbPersonaCargoId = _TbPersonaCargo.TbPersonaCargoId,
						Nombre = _TbPersonaCargo.Nombre,
						Activo = _TbPersonaCargo.Activo,
					};
					_db.Add(oCargoPersona);
				}
				else
				{
					oCargoPersona = await _db.TbPersonaCargo.Where(x => x.TbPersonaCargoId == _TbPersonaCargo.TbPersonaCargoId).FirstAsync();
					if (oCargoPersona == null) throw new Exception($"Cargo de persona no existe para el ID: {_TbPersonaCargo.TbPersonaCargoId}");

					oCargoPersona.TbPersonaCargoId = _TbPersonaCargo.TbPersonaCargoId;
					oCargoPersona.Nombre = _TbPersonaCargo.Nombre;
					oCargoPersona.Activo = _TbPersonaCargo.Activo;

					_db.Update(oCargoPersona);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<int> Eliminar(int _TbPersonaCargoId)
		{
			try
			{
				return await _db.TbPersonaCargo.Where(x => x.TbPersonaCargoId == _TbPersonaCargoId).ExecuteDeleteAsync();

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
