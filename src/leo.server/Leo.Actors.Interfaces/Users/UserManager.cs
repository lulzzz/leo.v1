using Leo.Core.Id;
using Leo.Core.Security;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Leo.Actors.Interfaces.Users
{
    internal class UserManager : IUserManager
    {
        private readonly IClusterClient _actors;
        private readonly IdProvider _ids;

        public UserManager(IClusterClient actors, IdProvider ids)
        {
            _actors = actors;
            _ids = ids;
        }

        public async Task<string> CreateOrReplaceUserAsync(string authenticationType, string userId, string userName, string email)
        {
            if (!_actors.IsInitialized)
                await _actors.Connect();

            var commandId = _ids.Create();
            try
            {

                var user = _actors.GetGrain<IUserAggregate>(userId);
                await user.LoginUser(new LoginUser(commandId, userId, authenticationType, userId, userName, email));
            }
            catch(Exception ex)
            {
                throw;
            }
            return userId;
        }
    }
}