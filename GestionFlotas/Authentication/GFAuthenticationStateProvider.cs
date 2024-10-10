using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using GestionFlotas.model;
using System.Security.Claims;

namespace GestionFlotas.Authentication
{
    public class GFAuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public GFAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    try
        //    {
        //        var userSessionStorageResult = await _sessionStorage.GetAsync<TbUsuarioModel>("SentinelUserSession");
        //        var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
        //        if (userSession == null)
        //            return await Task.FromResult(new AuthenticationState(_anonymous));
        //        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
        //        new Claim("UserId", userSession.TbUsuarioId.ToString()),
        //                new Claim("PerfilId", userSession.TbPerfilId.ToString()),
        //                new Claim("UserLogin", userSession.Login),
        //                new Claim("UserData", JsonConvert.SerializeObject(userSession)),
        //                new Claim(ClaimTypes.Name, $"{userSession.DataPersona.Nombre.Trim()} {userSession.DataPersona.Apellido.Trim()}")
        //    }, "SentinelAuth"));
        //        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        //    }
        //    catch
        //    {
        //        return await Task.FromResult(new AuthenticationState(_anonymous));
        //    }

        //}
        //public async Task UpdateAuthenticationState(TbUsuarioModel userSession)
        //{
        //    ClaimsPrincipal claimsPrincipal;
        //    if (userSession != null)
        //    {
        //        await _sessionStorage.SetAsync("SentinelUserSession", userSession);
        //        claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
        //        new Claim("UserId", userSession.TbUsuarioId.ToString()),
        //                new Claim("PerfilId", userSession.TbPerfilId.ToString()),
        //                new Claim("UserLogin", userSession.Login),
        //                new Claim("UserData", JsonConvert.SerializeObject(userSession)),
        //                new Claim(ClaimTypes.Name, $"{userSession.DataPersona.Nombre.Trim()} {userSession.DataPersona.Apellido.Trim()}" )
        //    }, "SentinelAuth"));
        //    }
        //    else
        //    {
        //        await _sessionStorage.DeleteAsync("SentinelUserSession");
        //        claimsPrincipal = _anonymous;
        //    }
        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        //}

    }
}