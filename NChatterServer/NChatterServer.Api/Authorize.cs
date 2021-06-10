using System.Collections.Generic;
using NChatterServer.Core.Models;

namespace NChatterServer.Api
{
    public class Authorize
    {
        public static bool UserInGroup(IEnumerable<User> members, int id)
        {
            foreach (var user in members)
            {
                if (user.Id == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}